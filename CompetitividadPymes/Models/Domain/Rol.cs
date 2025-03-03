using System;
using System.Collections.Generic;

namespace CompetitividadPymes.Models.Domain;

public partial class Rol
{
    public int IdRol { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();

    public virtual ICollection<Factor> IdFactors { get; set; } = new List<Factor>();

    public virtual ICollection<Permiso> IdPermisos { get; set; } = new List<Permiso>();
}
