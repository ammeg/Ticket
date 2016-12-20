namespace Ticket.Models
{
    public abstract class Base
    {
        public Guid UsuarioCad { get; set; }

        public Guid? UsuarioAlteracao { get; set; }

        public DateTime DataHoraCad { get; set; }

        public DateTime? DataHoraAlteracao { get; set; }
    }
}
