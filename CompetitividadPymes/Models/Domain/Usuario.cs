using System;
using System.Collections.Generic;

namespace CompetitividadPymes.Models.Domain;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public int? IdEmpresa { get; set; }

    public int IdRol { get; set; }

    public string Nombre { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Estado { get; set; } = null!;

    public DateTime? FechaRegistro { get; set; }

    public DateTime? UltimoAcceso { get; set; }

    public virtual Empresa? IdEmpresaNavigation { get; set; }

    public virtual Rol IdRolNavigation { get; set; } = null!;
}
