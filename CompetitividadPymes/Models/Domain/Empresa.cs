using System;
using System.Collections.Generic;

namespace CompetitividadPymes.Models.Domain;

public partial class Empresa
{
    public int IdEmpresa { get; set; }

    public string Nombre { get; set; } = null!;

    public string Sector { get; set; } = null!;

    public string Clasificacion { get; set; } = null!;

    public string Estado { get; set; } = null!;

    public DateTime? FechaRegistro { get; set; }

    public virtual ICollection<Encuestum> Encuesta { get; set; } = new List<Encuestum>();

    public virtual ICollection<OrdenPago> OrdenPagos { get; set; } = new List<OrdenPago>();

    public virtual ICollection<Suscripcion> Suscripcions { get; set; } = new List<Suscripcion>();

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
