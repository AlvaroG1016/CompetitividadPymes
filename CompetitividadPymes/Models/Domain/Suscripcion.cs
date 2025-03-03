using System;
using System.Collections.Generic;

namespace CompetitividadPymes.Models.Domain;

public partial class Suscripcion
{
    public int IdSuscripcion { get; set; }

    public int? IdEmpresa { get; set; }

    public int? IdPlan { get; set; }

    public DateTime FechaInicio { get; set; }

    public DateTime FechaFin { get; set; }

    public string Estado { get; set; } = null!;

    public int? IdOrdenPago { get; set; }

    public virtual Empresa? IdEmpresaNavigation { get; set; }

    public virtual OrdenPago? IdOrdenPagoNavigation { get; set; }

    public virtual Plane? IdPlanNavigation { get; set; }
}
