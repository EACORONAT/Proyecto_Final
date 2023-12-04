namespace MvcCongresoTIC.Models
{
    public class RegistroConferenciaVM
    {
        public int ConferenciaId { get; set; }
        public string? ConferenciaTitulo { get; set; }
        public int ParticipanteId { get; set; }
        public List<Participante>? Participantes { get; set; }
        public bool ConfirmacionAsistencia { get; set; }
    }
}