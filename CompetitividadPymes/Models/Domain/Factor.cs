using System;
using System.Collections.Generic;

namespace CompetitividadPymes.Models.Domain;

public partial class Factor
{
    public int IdFactor { get; set; }

    public string Nombre { get; set; } = null!;

    public decimal Peso { get; set; }

    public string DisponibleEnPlanes { get; set; } = null!;

    public string? Descripcion { get; set; }

    public virtual ICollection<DocumentoEvidencium> DocumentoEvidencia { get; set; } = new List<DocumentoEvidencium>();

    public virtual ICollection<Variable> Variables { get; set; } = new List<Variable>();

    public virtual ICollection<Rol> IdRols { get; set; } = new List<Rol>();
}
