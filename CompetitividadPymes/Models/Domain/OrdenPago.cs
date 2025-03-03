using System;
using System.Collections.Generic;

namespace CompetitividadPymes.Models.Domain;

public partial class OrdenPago
{
    public int IdOrdenPago { get; set; }

    public int? IdEmpresa { get; set; }

    public int IdPlan { get; set; }

    public decimal MontoTotal { get; set; }

    public string MetodoPago { get; set; } = null!;

    public string Estado { get; set; } = null!;

    public DateTime? FechaCreacion { get; set; }

    public DateTime? FechaPago { get; set; }

    public virtual Empresa? IdEmpresaNavigation { get; set; }

    public virtual Plane IdPlanNavigation { get; set; } = null!;

    public virtual ICollection<Suscripcion> Suscripcions { get; set; } = new List<Suscripcion>();
}
