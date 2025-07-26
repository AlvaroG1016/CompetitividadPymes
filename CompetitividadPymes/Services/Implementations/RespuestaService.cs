using CompetitividadPymes.Models.Domain;
using CompetitividadPymes.Models.DTO.Request;
using CompetitividadPymes.Models.DTO.Response;
using CompetitividadPymes.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace CompetitividadPymes.Services.Implementations
{
    public class RespuestaService : IRespuestaService
    {
        private readonly PymesCompetitividadContext _context;

        public RespuestaService(PymesCompetitividadContext context)
        {
            _context = context;
        }

        public async Task<ResultadoEncuestaDTO> ActualizarRespuestas(List<RespuestaRequestDTO> respuestas)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var idEncuesta = respuestas.First().IdEncuesta;

                // 1. Guardar/actualizar las respuestas individuales (esto sí se puede actualizar completo)
                await UpsertRespuestasIndividuales(respuestas);

                // 2. Calcular y guardar/actualizar SOLO las variables que cambiaron
                var resultadosVariables = await UpsertResultadosVariables(idEncuesta, respuestas);

                // 3. Calcular y guardar/actualizar SOLO los factores que cambiaron
                var resultadosFactores = await UpsertResultadosFactores(idEncuesta, resultadosVariables);

                // 4. Crear DTO completo con toda la información
                var resultadoEncuesta = await CrearResultadoCompleto(idEncuesta);

                await transaction.CommitAsync();
                return resultadoEncuesta;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                Console.WriteLine($"Error en ActualizarRespuestas: {ex.Message}");
                throw;
            }
        }

        private async Task UpsertRespuestasIndividuales(List<RespuestaRequestDTO> respuestas)
        {
            var idEncuesta = respuestas.First().IdEncuesta;

            foreach (var respuesta in respuestas)
            {
                // Buscar si ya existe la respuesta
                var respuestaExistente = await _context.Respuesta
                    .FirstOrDefaultAsync(r => r.IdEncuesta == respuesta.IdEncuesta &&
                                             r.IdPregunta == respuesta.IdPregunta);

                if (respuestaExistente != null)
                {
                    // Actualizar respuesta existente
                    respuestaExistente.ValorRespuesta = respuesta.ValorRespuesta;
                }
                else
                {
                    // Crear nueva respuesta
                    var nuevaRespuesta = new Respuestum
                    {
                        IdEncuesta = respuesta.IdEncuesta,
                        IdPregunta = respuesta.IdPregunta,
                        ValorRespuesta = respuesta.ValorRespuesta
                    };
                    _context.Respuesta.Add(nuevaRespuesta);
                }
            }

            await _context.SaveChangesAsync();
        }

        private async Task<List<ResultadoVariableDTO>> UpsertResultadosVariables(int idEncuesta, List<RespuestaRequestDTO> respuestas)
        {
            // Obtener solo las variables que están afectadas por las respuestas enviadas
            var preguntasAfectadas = respuestas.Select(r => r.IdPregunta).ToList();

            var variablesAfectadas = await _context.Variables
                .Include(v => v.Pregunta)
                .Include(v => v.IdFactorNavigation)
                .Where(v => v.Pregunta.Any(p => preguntasAfectadas.Contains(p.Id)))
                .ToListAsync();

            var resultadosCalculados = new List<ResultadoVariableDTO>();

            foreach (var variable in variablesAfectadas)
            {
                // Obtener todas las respuestas para esta variable en esta encuesta
                var respuestasVariable = await _context.Respuesta
                    .Where(r => r.IdEncuesta == idEncuesta &&
                               variable.Pregunta.Select(p => p.Id).Contains(r.IdPregunta))
                    .ToListAsync();

                if (respuestasVariable.Any())
                {
                    // Calcular totales
                    var puntajeObtenido = respuestasVariable
                            .Where(r => r.ValorRespuesta != null)
                            .Sum(r => Convert.ToDecimal(r.ValorRespuesta));

                    var cantidadPreguntas = variable.Pregunta.Count;
                    var puntajeMaximo = cantidadPreguntas * 100;

                    // Calcular porcentajes
                    var porcentajeVariable = puntajeMaximo > 0 ? (puntajeObtenido / puntajeMaximo) * 100 : 0;
                    var pesoVariable = variable.Peso;
                    var contribucionFinal = (porcentajeVariable * pesoVariable) / 100;

                    // Buscar si ya existe el resultado para esta variable en esta encuesta
                    var resultadoExistente = await _context.ResultadoVariables
                        .FirstOrDefaultAsync(rv => rv.IdEncuesta == idEncuesta &&
                                                  rv.IdVariable == variable.IdVariable);

                    if (resultadoExistente != null)
                    {
                        // Actualizar resultado existente
                        resultadoExistente.PuntajeObtenido = puntajeObtenido;
                        resultadoExistente.PuntajeMaximo = puntajeMaximo;
                        resultadoExistente.PorcentajeVariable = Math.Round(porcentajeVariable, 2);
                        resultadoExistente.PesoVariable = pesoVariable;
                        resultadoExistente.ContribucionFinal = Math.Round(contribucionFinal, 2);
                        resultadoExistente.FechaCalculo = DateTime.Now;
                    }
                    else
                    {
                        // Crear nuevo resultado
                        var nuevoResultado = new ResultadoVariable
                        {
                            IdEncuesta = idEncuesta,
                            IdVariable = variable.IdVariable,
                            PuntajeObtenido = puntajeObtenido,
                            PuntajeMaximo = puntajeMaximo,
                            PorcentajeVariable = Math.Round(porcentajeVariable, 2),
                            PesoVariable = pesoVariable,
                            ContribucionFinal = Math.Round(contribucionFinal, 2),
                            FechaCalculo = DateTime.Now
                        };
                        _context.ResultadoVariables.Add(nuevoResultado);
                    }

                    // Agregar al DTO
                    resultadosCalculados.Add(new ResultadoVariableDTO
                    {
                        IdVariable = variable.IdVariable,
                        NombreVariable = variable.Nombre ?? $"Variable {variable.IdVariable}",
                        IdFactor = variable.IdFactor,
                        PuntajeObtenido = puntajeObtenido,
                        PuntajeMaximo = puntajeMaximo,
                        PorcentajeVariable = Math.Round(porcentajeVariable, 2),
                        PesoVariable = pesoVariable,
                        ContribucionFinal = Math.Round(contribucionFinal, 2),
                        CantidadPreguntas = cantidadPreguntas
                    });
                }
            }

            await _context.SaveChangesAsync();
            return resultadosCalculados;
        }

        private async Task<List<ResultadoFactorDTO>> UpsertResultadosFactores(int idEncuesta, List<ResultadoVariableDTO> variablesActualizadas)
        {
            // Obtener solo los factores que tienen variables que fueron actualizadas
            var factoresAfectados = variablesActualizadas.Select(v => v.IdFactor).Distinct().ToList();

            var factoresConDatos = await _context.Factors
                .Include(f => f.Variables)
                .Where(f => factoresAfectados.Contains(f.IdFactor))
                .ToListAsync();

            var resultadosFactores = new List<ResultadoFactorDTO>();

            foreach (var factor in factoresConDatos)
            {
                // Obtener TODOS los resultados de variables para este factor en esta encuesta
                var resultadosVariablesFactor = await _context.ResultadoVariables
                    .Include(rv => rv.IdVariableNavigation)
                    .Where(rv => rv.IdEncuesta == idEncuesta &&
                                factor.Variables.Select(v => v.IdVariable).Contains(rv.IdVariable))
                    .ToListAsync();

                if (resultadosVariablesFactor.Any())
                {
                    // Calcular totales del factor
                    var contribucionesVariables = resultadosVariablesFactor.Sum(rv => rv.ContribucionFinal);
                    var pesoTotalVariables = factor.Variables.Sum(v => v.Peso);
                    var cantidadVariables = factor.Variables.Count;

                    var puntajeObtenido = contribucionesVariables;
                    var puntajeMaximo = pesoTotalVariables;

                    // Porcentaje del factor (0-100%)
                    var porcentajeFactor = puntajeMaximo > 0 ? (puntajeObtenido / puntajeMaximo) * 100 : 0;

                    // Peso del factor en el total general
                    var pesoFactor = factor.Peso;

                    // Contribución final del factor al puntaje total
                    var contribucionFinalFactor = (porcentajeFactor * pesoFactor) / 100;

                    // Buscar si ya existe el resultado para este factor en esta encuesta
                    var resultadoFactorExistente = await _context.ResultadoFactors
                        .FirstOrDefaultAsync(rf => rf.IdEncuesta == idEncuesta &&
                                                  rf.IdFactor == factor.IdFactor);

                    if (resultadoFactorExistente != null)
                    {
                        // Actualizar resultado existente
                        resultadoFactorExistente.PuntajeObtenido = Math.Round(puntajeObtenido, 2);
                        resultadoFactorExistente.PuntajeMaximo = Math.Round(puntajeMaximo, 2);
                        resultadoFactorExistente.PorcentajeFactor = Math.Round(porcentajeFactor, 2);
                        resultadoFactorExistente.PesoFactor = pesoFactor;
                        resultadoFactorExistente.ContribucionFinal = Math.Round(contribucionFinalFactor, 2);
                        resultadoFactorExistente.CantidadVariables = cantidadVariables;
                        resultadoFactorExistente.FechaCalculo = DateTime.Now;
                    }
                    else
                    {
                        // Crear nuevo resultado
                        var nuevoResultadoFactor = new ResultadoFactor
                        {
                            IdEncuesta = idEncuesta,
                            IdFactor = factor.IdFactor,
                            PuntajeObtenido = Math.Round(puntajeObtenido, 2),
                            PuntajeMaximo = Math.Round(puntajeMaximo, 2),
                            PorcentajeFactor = Math.Round(porcentajeFactor, 2),
                            PesoFactor = pesoFactor,
                            ContribucionFinal = Math.Round(contribucionFinalFactor, 2),
                            CantidadVariables = cantidadVariables,
                            FechaCalculo = DateTime.Now
                        };
                        _context.ResultadoFactors.Add(nuevoResultadoFactor);
                    }

                    // Crear DTO con variables asociadas
                    var variablesDTO = resultadosVariablesFactor.Select(rv => new ResultadoVariableDTO
                    {
                        IdVariable = rv.IdVariable,
                        NombreVariable = rv.IdVariableNavigation.Nombre ?? $"Variable {rv.IdVariable}",
                        IdFactor = factor.IdFactor,
                        PuntajeObtenido = rv.PuntajeObtenido,
                        PuntajeMaximo = rv.PuntajeMaximo,
                        PorcentajeVariable = rv.PorcentajeVariable,
                        PesoVariable = rv.PesoVariable,
                        ContribucionFinal = rv.ContribucionFinal,
                        CantidadPreguntas = factor.Variables.First(v => v.IdVariable == rv.IdVariable).Pregunta.Count
                    }).ToList();

                    resultadosFactores.Add(new ResultadoFactorDTO
                    {
                        IdFactor = factor.IdFactor,
                        NombreFactor = factor.Nombre ?? $"Factor {factor.IdFactor}",
                        PuntajeObtenido = Math.Round(puntajeObtenido, 2),
                        PuntajeMaximo = Math.Round(puntajeMaximo, 2),
                        PorcentajeFactor = Math.Round(porcentajeFactor, 2),
                        PesoFactor = pesoFactor,
                        ContribucionFinal = Math.Round(contribucionFinalFactor, 2),
                        CantidadVariables = cantidadVariables,
                        Variables = variablesDTO
                    });
                }
            }

            await _context.SaveChangesAsync();
            return resultadosFactores;
        }

        private async Task<ResultadoEncuestaDTO> CrearResultadoCompleto(int idEncuesta)
        {
            // Obtener TODOS los resultados para esta encuesta (todos los factores)
            var resultadosFactores = await _context.ResultadoFactors
                .Include(rf => rf.IdFactorNavigation)
                .Where(rf => rf.IdEncuesta == idEncuesta)
                .ToListAsync();

            // Obtener TODOS los resultados de variables para esta encuesta
            var resultadosVariables = await _context.ResultadoVariables
                .Include(rv => rv.IdVariableNavigation)
                .Where(rv => rv.IdEncuesta == idEncuesta)
                .ToListAsync();

            // Crear DTOs
            var factoresDTO = resultadosFactores.Select(rf => new ResultadoFactorDTO
            {
                IdFactor = rf.IdFactor,
                NombreFactor = rf.IdFactorNavigation.Nombre ?? $"Factor {rf.IdFactor}",
                PuntajeObtenido = rf.PuntajeObtenido,
                PuntajeMaximo = rf.PuntajeMaximo,
                PorcentajeFactor = rf.PorcentajeFactor,
                PesoFactor = rf.PesoFactor,
                ContribucionFinal = rf.ContribucionFinal,
                CantidadVariables = rf.CantidadVariables,
                Variables = resultadosVariables
                    .Where(rv => rv.IdVariableNavigation.IdFactor == rf.IdFactor)
                    .Select(rv => new ResultadoVariableDTO
                    {
                        IdVariable = rv.IdVariable,
                        NombreVariable = rv.IdVariableNavigation.Nombre ?? $"Variable {rv.IdVariable}",
                        IdFactor = rf.IdFactor,
                        PuntajeObtenido = rv.PuntajeObtenido,
                        PuntajeMaximo = rv.PuntajeMaximo,
                        PorcentajeVariable = rv.PorcentajeVariable,
                        PesoVariable = rv.PesoVariable,
                        ContribucionFinal = rv.ContribucionFinal
                    }).ToList()
            }).ToList();

            var variablesDTO = resultadosVariables.Select(rv => new ResultadoVariableDTO
            {
                IdVariable = rv.IdVariable,
                NombreVariable = rv.IdVariableNavigation.Nombre ?? $"Variable {rv.IdVariable}",
                IdFactor = rv.IdVariableNavigation.IdFactor,
                PuntajeObtenido = rv.PuntajeObtenido,
                PuntajeMaximo = rv.PuntajeMaximo,
                PorcentajeVariable = rv.PorcentajeVariable,
                PesoVariable = rv.PesoVariable,
                ContribucionFinal = rv.ContribucionFinal
            }).ToList();

            // Calcular puntaje final total (suma de contribuciones de factores)
            var puntajeFinalTotal = resultadosFactores.Sum(rf => rf.ContribucionFinal);

            return new ResultadoEncuestaDTO
            {
                IdEncuesta = idEncuesta,
                ResultadosPorFactor = factoresDTO,
                ResultadosPorVariable = variablesDTO,
                PuntajeFinalTotal = Math.Round(puntajeFinalTotal, 2),
                PorcentajeFinalTotal = Math.Round(puntajeFinalTotal, 2),
                FechaCalculo = DateTime.Now,
                TotalFactores = factoresDTO.Count,
                TotalVariables = variablesDTO.Count,
                TotalPreguntas = variablesDTO.Sum(v => v.CantidadPreguntas)
            };
        }

        public async Task<ResultadoEncuestaDTO> ObtenerResultadosEncuesta(int idEncuesta)
        {
            return await CrearResultadoCompleto(idEncuesta);
        }

        public async Task<List<RespuestaPorFactorDTO>> ObtenerRespuestasPorFactor(int encuestaId, int factorId)
        {
            try
            {
                var respuestas = await _context.Respuesta
                    .Include(r => r.IdPreguntaNavigation)
                        .ThenInclude(p => p.IdVariableNavigation)
                    .Where(r => r.IdEncuesta == encuestaId &&
                               r.IdPreguntaNavigation.IdVariableNavigation.IdFactor == factorId)
                    .Select(r => new RespuestaPorFactorDTO
                    {
                        IdPregunta = r.IdPregunta,
                        ValorRespuesta = r.ValorRespuesta,
                        DescripcionPregunta = r.IdPreguntaNavigation.Enunciado,
                        NombreVariable = r.IdPreguntaNavigation.IdVariableNavigation.Nombre,
                        IdVariable = r.IdPreguntaNavigation.IdVariableNavigation.IdVariable,
                        IdFactor = r.IdPreguntaNavigation.IdVariableNavigation.IdFactor
                    })
                    .OrderBy(r => r.IdPregunta)
                    .ToListAsync();

                return respuestas;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en ObtenerRespuestasPorFactor: {ex.Message}");
                throw new Exception($"Error al obtener respuestas para el factor {factorId} en la encuesta {encuestaId}: {ex.Message}");
            }
        }
    }
}