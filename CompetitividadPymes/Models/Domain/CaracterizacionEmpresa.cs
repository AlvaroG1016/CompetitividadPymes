using System;
using System.Collections.Generic;

namespace CompetitividadPymes.Models.Domain;

public partial class CaracterizacionEmpresa
{
    public int Id { get; set; }

    public string NombreEmpresa { get; set; } = null!;

    public string Ciudad { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string TiempoMercado { get; set; } = null!;

    public int IdSector { get; set; }

    public int IdEmpresa { get; set; }

    public int IdSubsector { get; set; }

    public virtual ICollection<Encuestum> Encuesta { get; set; } = new List<Encuestum>();

    public virtual SectorSubsector SectorSubsector { get; set; } = null!;
}
