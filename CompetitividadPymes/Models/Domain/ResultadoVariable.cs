using System;
using System.Collections.Generic;

namespace CompetitividadPymes.Models.Domain;

public partial class ResultadoVariable
{
    public int Id { get; set; }

    public int IdEncuesta { get; set; }

    public int IdVariable { get; set; }

    public decimal PuntajeObtenido { get; set; }

    public decimal PuntajeMaximo { get; set; }

    public decimal PorcentajeVariable { get; set; }

    public decimal PesoVariable { get; set; }

    public decimal ContribucionFinal { get; set; }

    public DateTime? FechaCalculo { get; set; }

    public virtual Encuestum IdEncuestaNavigation { get; set; } = null!;

    public virtual Variable IdVariableNavigation { get; set; } = null!;
}
