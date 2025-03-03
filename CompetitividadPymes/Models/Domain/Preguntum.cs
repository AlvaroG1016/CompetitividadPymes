using System;
using System.Collections.Generic;

namespace CompetitividadPymes.Models.Domain;

public partial class Preguntum
{
    public int IdPregunta { get; set; }

    public int? IdVariable { get; set; }

    public string Enunciado { get; set; } = null!;

    public decimal Peso { get; set; }

    public virtual Variable? IdVariableNavigation { get; set; }

    public virtual ICollection<Respuestum> Respuesta { get; set; } = new List<Respuestum>();
}
