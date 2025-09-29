using System.ComponentModel.DataAnnotations;

namespace DotNetTrip.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        //DATA ANNOTATIONS UTILIZADAS AQUI:
        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "O nome deve ter entre 2 e 100 caracteres")]
        [Display(Name = "Nome Completo")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email deve ter um formato válido")]
        [StringLength(150, ErrorMessage = "O email deve ter no máximo 150 caracteres")]
        [Display(Name = "E-mail")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "O telefone é obrigatório")]
        [Phone(ErrorMessage = "Telefone deve ter um formato válido")]
        [StringLength(20, MinimumLength = 10, ErrorMessage = "O telefone deve ter entre 10 e 20 caracteres")]
        [Display(Name = "Telefone")]
        public string Telefone { get; set; } = string.Empty;

        [Required(ErrorMessage = "O CPF é obrigatório")]
        [StringLength(14, MinimumLength = 11, ErrorMessage = "O CPF deve ter entre 11 e 14 caracteres")]
        [RegularExpression(@"^\d{3}\.?\d{3}\.?\d{3}-?\d{2}$", ErrorMessage = "CPF deve ter um formato válido (000.000.000-00)")]
        [Display(Name = "CPF")]
        public string Cpf { get; set; } = string.Empty;

        [Required(ErrorMessage = "A data de nascimento é obrigatória")]
        [DataType(DataType.Date)]
        [Display(Name = "Data de Nascimento")]
        public DateTime DataNascimento { get; set; }

        [StringLength(200, ErrorMessage = "O endereço deve ter no máximo 200 caracteres")]
        [Display(Name = "Endereço")]
        public string? Endereco { get; set; }

        [StringLength(10, MinimumLength = 8, ErrorMessage = "O CEP deve ter entre 8 e 10 caracteres")]
        [RegularExpression(@"^\d{5}-?\d{3}$", ErrorMessage = "CEP deve ter um formato válido (00000-000)")]
        [Display(Name = "CEP")]
        public string? Cep { get; set; }

        [StringLength(100, ErrorMessage = "A cidade deve ter no máximo 100 caracteres")]
        [Display(Name = "Cidade")]
        public string? Cidade { get; set; }

        [StringLength(50, MinimumLength = 1, ErrorMessage = "O estado deve ter, no minimo 1 caractere e no máximo 50")]
        [Display(Name = "Estado (UF)")]
        public string? Estado { get; set; }

        // Propriedade calculada para exibir idade
        [Display(Name = "Idade")]
        public int Idade => DateTime.Now.Year - DataNascimento.Year -
                           (DateTime.Now.DayOfYear < DataNascimento.DayOfYear ? 1 : 0);

        // Data de cadastro automática
        [Display(Name = "Data de Cadastro")]
        public DateTime DataCadastro { get; set; } = DateTime.Now;
        public List<Reserva>? Reservas { get; set; }
    }
}
