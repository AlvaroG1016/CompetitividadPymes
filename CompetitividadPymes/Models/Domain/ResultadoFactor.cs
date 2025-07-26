using System;
using System.Collections.Generic;

namespace CompetitividadPymes.Models.Domain;

public partial class ResultadoFactor
{
    public int Id { get; set; }

    public int IdEncuesta { get; set; }

    public int IdFactor { get; set; }

    public decimal PuntajeObtenido { get; set; }

    public decimal PuntajeMaximo { get; set; }

    public decimal PorcentajeFactor { get; set; }

    public decimal PesoFactor { get; set; }

    public decimal ContribucionFinal { get; set; }

    public int CantidadVariables { get; set; }

    public DateTime? FechaCalculo { get; set; }

    public virtual Encuestum IdEncuestaNavigation { get; set; } = null!;

    public virtual Factor IdFactorNavigation { get; set; } = null!;
}
