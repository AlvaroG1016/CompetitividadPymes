using System;
using System.Collections.Generic;

namespace CompetitividadPymes.Models.Domain;

public partial class Permiso
{
    public int IdPermiso { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Rol> IdRols { get; set; } = new List<Rol>();
}
