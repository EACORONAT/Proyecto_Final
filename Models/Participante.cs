using System.ComponentModel.DataAnnotations;

namespace MvcCongresoTIC.Models
{
    public class Participante
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo Apellidos es obligatorio.")]
        public string Apellidos { get; set; }
        public string NombreCompleto => $"{Nombre} {Apellidos}";

        [Required(ErrorMessage = "El campo Usuario de Twitter es obligatorio.")]
        public string UTwitter { get; set; }

        [Required(ErrorMessage = "El campo Ocupacion es obligatorio.")]
        public string Ocupacion { get; set; }

        [Range(1, 6, ErrorMessage = "Selecciona un Avatar válido.")]
        public int AvatarId { get; set; }        
        public bool TYC { get; set; }
        public ICollection<RegistroConferencia> RegistrosConferencias { get; set; } = new List<RegistroConferencia>();
    }
}