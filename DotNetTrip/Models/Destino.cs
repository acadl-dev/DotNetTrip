namespace DotNetTrip.Models
{
    public class Destino
    {
        public int Id { get; set; }
        public string Cidade { get; set; }
        public string Pais { get; set; }

        // Relacionamento muitos-para-muitos com PacoteTuristico
        public ICollection<PacoteTuristico> PacotesTuristicos { get; set; } = new List<PacoteTuristico>();
    }
}