using System;
using System.Collections.Generic;

namespace CompetitividadPymes.Models.Domain;

public partial class Plane
{
    public int IdPlan { get; set; }

    public string Nombre { get; set; } = null!;

    public decimal Precio { get; set; }

    public int DuracionMeses { get; set; }

    public string Caracteristicas { get; set; } = null!;

    public virtual ICollection<OrdenPago> OrdenPagos { get; set; } = new List<OrdenPago>();

    public virtual ICollection<Suscripcion> Suscripcions { get; set; } = new List<Suscripcion>();
}
