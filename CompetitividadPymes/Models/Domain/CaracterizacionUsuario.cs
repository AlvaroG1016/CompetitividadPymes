using System;
using System.Collections.Generic;

namespace CompetitividadPymes.Models.Domain;

public partial class CaracterizacionUsuario
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public int Edad { get; set; }

    public string Genero { get; set; } = null!;

    public string Cargo { get; set; } = null!;

    public int Antiguedad { get; set; }

    public string EmailInstitucional { get; set; } = null!;

    public string EmailPersonal { get; set; } = null!;

    public int IdEmpresa { get; set; }

    public virtual ICollection<Encuestum> Encuesta { get; set; } = new List<Encuestum>();

    public virtual Empresa IdEmpresaNavigation { get; set; } = null!;
}
