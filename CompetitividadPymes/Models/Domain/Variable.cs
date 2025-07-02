using System;
using System.Collections.Generic;

namespace CompetitividadPymes.Models.Domain;

public partial class Variable
{
    public int IdVariable { get; set; }

    public int IdFactor { get; set; }

    public string Nombre { get; set; } = null!;

    public decimal Peso { get; set; }

    public string? Descripcion { get; set; }

    public int? Cantidadpreguntas { get; set; }

    public virtual Factor IdFactorNavigation { get; set; } = null!;

    public virtual ICollection<Preguntum> Pregunta { get; set; } = new List<Preguntum>();
}
