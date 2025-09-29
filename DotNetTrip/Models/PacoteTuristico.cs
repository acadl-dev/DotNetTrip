namespace DotNetTrip.Models
{
    public class PacoteTuristico
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public DateTime DataInicio { get; set; }
        public int CapacidadeMaxima { get; set; }
        public decimal Preco { get; set; }
        public List<Destino>? Destinos { get; set; }

        // Relacionamento automático
        public virtual ICollection<Reserva> ReservasFeitas { get; set; } = new List<Reserva>();
    }
}
