using System;
using System.Collections.Generic;

namespace CompetitividadPymes.Models.Domain;

public partial class Sector
{
    public int IdSector { get; set; }

    public string NombreSector { get; set; } = null!;

    public virtual ICollection<SectorSubsector> SectorSubsectors { get; set; } = new List<SectorSubsector>();
}
