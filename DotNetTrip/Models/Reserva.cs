using System.ComponentModel.DataAnnotations;

namespace DotNetTrip.Models
{
    public class Reserva
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public Cliente? Cliente { get; set; }
        public int PacoteTuristicoId { get; set; }
        public PacoteTuristico? PacoteTuristico { get; set; }
        public DateTime DataReserva { get; set; }

        [Required(ErrorMessage = "As observações são obrigatórias.")]
        [MinLength(10, ErrorMessage = "As observações devem ter pelo menos 10 caracteres.")]
        public string Observacoes { get; set; } = string.Empty;


        // Delegate para o evento de capacidade atingida
        public delegate void CapacityReachedHandler(string message);

        // Evento estático para ser acessível de qualquer lugar
        public static event CapacityReachedHandler? CapacityReached;

        // Método para disparar o evento
        public static void OnCapacityReached(string message)
        {
            CapacityReached?.Invoke(message);
        }
    }
}
