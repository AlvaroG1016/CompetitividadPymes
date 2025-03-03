using System;
using System.Collections.Generic;

namespace CompetitividadPymes.Models.Domain;

public partial class DocumentoEvidencium
{
    public int IdDocumento { get; set; }

    public int? IdEncuesta { get; set; }

    public int? IdFactor { get; set; }

    public string TipoDocumento { get; set; } = null!;

    public byte[] Archivo { get; set; } = null!;

    public virtual Encuestum? IdEncuestaNavigation { get; set; }

    public virtual Factor? IdFactorNavigation { get; set; }
}
