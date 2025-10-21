using System;
using System.Collections.Generic;

namespace CompetitividadPymes.Models.Domain;

public partial class SubSector
{
    public int IdSubsector { get; set; }

    public string NombreSubsector { get; set; } = null!;

    public virtual ICollection<SectorSubsector> SectorSubsectors { get; set; } = new List<SectorSubsector>();
}
