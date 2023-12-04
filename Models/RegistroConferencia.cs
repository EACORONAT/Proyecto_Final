using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MvcCongresoTIC.Models
{
    public class RegistroConferencia
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ConferenciaId { get; set; }                
        public int ParticipanteId { get; set; }
        //public Participante? Participante { get; set; }
        public bool ConfirmacionAsistencia { get; set; }
        public Conferencia Conferencia { get; set; }
        public Participante Participante { get; set; }
    }
}