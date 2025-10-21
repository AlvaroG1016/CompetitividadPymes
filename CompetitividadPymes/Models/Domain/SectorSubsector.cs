using System;
using System.Collections.Generic;

namespace CompetitividadPymes.Models.Domain;

public partial class SectorSubsector
{
    public int IdSectorsubsector { get; set; }

    public int IdSector { get; set; }

    public int IdSubsector { get; set; }

    public virtual ICollection<CaracterizacionEmpresa> CaracterizacionEmpresas { get; set; } = new List<CaracterizacionEmpresa>();

    public virtual Sector IdSectorNavigation { get; set; } = null!;

    public virtual SubSector IdSubsectorNavigation { get; set; } = null!;
}
