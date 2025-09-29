using System.ComponentModel.DataAnnotations;

namespace DotNetTrip.Models
{
    public class PacoteTuristico
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O título é obrigatório.")]
        [MinLength(3, ErrorMessage = "O título deve ter pelo menos 3 caracteres.")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "A data de início é obrigatória.")]
        public DateTime DataInicio { get; set; }

        [Required(ErrorMessage = "A capacidade máxima é obrigatória.")]
        [Range(1, int.MaxValue, ErrorMessage = "A capacidade deve ser maior que zero.")]
        public int CapacidadeMaxima { get; set; }

        [Required(ErrorMessage = "O preço é obrigatório.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser maior que zero.")]
        public decimal Preco { get; set; }

        // MANTER APENAS ESTA (escolha o nome que preferir):
        public ICollection<Destino> Destinos { get; set; } = new List<Destino>();

        // Relacionamento um-para-muitos com Reserva
        public ICollection<Reserva> ReservasFeitas { get; set; } = new List<Reserva>();
    }
}
