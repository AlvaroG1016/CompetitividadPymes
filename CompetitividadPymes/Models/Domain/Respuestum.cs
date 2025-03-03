using System;
using System.Collections.Generic;

namespace CompetitividadPymes.Models.Domain;

public partial class Respuestum
{
    public int IdRespuesta { get; set; }

    public int? IdEncuesta { get; set; }

    public int? IdPregunta { get; set; }

    public int ValorRespuesta { get; set; }

    public virtual Encuestum? IdEncuestaNavigation { get; set; }

    public virtual Preguntum? IdPreguntaNavigation { get; set; }
}
