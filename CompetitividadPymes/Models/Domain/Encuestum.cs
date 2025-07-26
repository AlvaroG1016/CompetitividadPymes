using System;
using System.Collections.Generic;

namespace CompetitividadPymes.Models.Domain;

public partial class Encuestum
{
    public int IdEncuesta { get; set; }

    public int? IdEmpresa { get; set; }

    public DateTime? FechaAplicacion { get; set; }

    public string Estado { get; set; } = null!;

    public decimal? PuntajeTotal { get; set; }

    public virtual ICollection<DocumentoEvidencium> DocumentoEvidencia { get; set; } = new List<DocumentoEvidencium>();

    public virtual Empresa? IdEmpresaNavigation { get; set; }

    public virtual ICollection<Respuestum> Respuesta { get; set; } = new List<Respuestum>();

    public virtual ICollection<ResultadoFactor> ResultadoFactors { get; set; } = new List<ResultadoFactor>();

    public virtual ICollection<ResultadoVariable> ResultadoVariables { get; set; } = new List<ResultadoVariable>();
}
