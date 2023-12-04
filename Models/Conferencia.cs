using System.ComponentModel.DataAnnotations;

namespace MvcCongresoTIC.Models
{
    public class Conferencia
    {        
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Horario { get; set; }
        public string NombreConferencista { get; set; }
        public bool ConfirmacionAsistencia { get; set; }
        // Propiedad para almacenar los registros de asistencia
        public List<RegistroConferencia> Asistentes { get; set; }
        public ICollection<RegistroConferencia> AsistentesCollection { get; set; } = new List<RegistroConferencia>();
    }
}