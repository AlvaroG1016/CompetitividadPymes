﻿using AutoMapper;
using CompetitividadPymes.Models.Domain;
using CompetitividadPymes.Models.DTO.Request;
using CompetitividadPymes.Models.DTO.Response;
using CompetitividadPymes.Services.Interfaces;
using CompetitividadPymes.Utilities;

namespace CompetitividadPymes.Services.Implementations
{
    public class CaracterizacionEmpresaService : ICaracterizacionEmpresaService
    {
        private readonly PymesCompetitividadContext _context;
        private readonly IMapper _mapper;
        private readonly GeneralUtilities _utilities;

        public CaracterizacionEmpresaService(PymesCompetitividadContext context, IMapper mapper, GeneralUtilities utilities)
        {
            _context = context;
            _mapper = mapper;
            _utilities = utilities;
        }

        public async Task CreateCaracterizacionEmpresa(CaracterizacionEmpresaRequestDTO req)
        {
            if (req == null)
            {
                throw new ArgumentNullException(nameof(req), "Los datos son requeridos");
            }
            await using var transaction = await _context.Database.BeginTransactionAsync();

            var caracterizacion = _mapper.Map<CaracterizacionEmpresa>(req);

            caracterizacion.IdEmpresa = _utilities.ObtenerIdEmpresaToken();

            _context.CaracterizacionEmpresas.Add(caracterizacion);
            await _context.SaveChangesAsync();

            await transaction.CommitAsync();

        }

        public async Task<CaracterizacionEmpresaResponseDTO> GetCaracterizacionEmpresaByIdEmpresa()
        {
            var idEmpresa = _utilities.ObtenerIdEmpresaToken();
            var caracterizacion = _context.CaracterizacionEmpresas.FirstOrDefault(c => c.IdEmpresa == idEmpresa);

            var result = caracterizacion != null
                ? _mapper.Map<CaracterizacionEmpresaResponseDTO>(caracterizacion)
                : new CaracterizacionEmpresaResponseDTO();

            result.Caracterizado = caracterizacion != null;

            return result;
        }
    }
}
