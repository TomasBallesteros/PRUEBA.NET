using System;
using System.Collections.Generic;

namespace API_PRUEBA.Models
{
    public partial class Persona
    {
        public int PeId { get; set; }
        public string? PeNombres { get; set; }
        public string? PeApellidos { get; set; }
        public string? PeIdentificacion { get; set; }
        public string? PeEmail { get; set; }
        public string? PeTipoIdentificacion { get; set; }
        public DateTime? PeFechaCreacion { get; set; }
        public string? PeCodigoPersona { get; set; }
        public string? PeNombreCompleto { get; set; }
    }
}
