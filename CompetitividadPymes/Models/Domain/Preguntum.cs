using System;
using System.Collections.Generic;

namespace CompetitividadPymes.Models.Domain;

public partial class Preguntum
{
    public int? IdVariable { get; set; }

    public string Enunciado { get; set; } = null!;

    public decimal Peso { get; set; }

    public string Id { get; set; } = null!;

    public virtual Variable? IdVariableNavigation { get; set; }

    public virtual ICollection<Respuestum> Respuesta { get; set; } = new List<Respuestum>();
}
