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
