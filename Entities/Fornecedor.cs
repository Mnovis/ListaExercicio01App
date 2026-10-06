using System.ComponentModel.DataAnnotations;

namespace ListaExercicio01App.Entities
{
    public class Fornecedor
    {
        public Guid IdFornecedor { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "O {0} é obrigatório")]
        [MaxLength(150, ErrorMessage = "O Nome deve ter no máximo {1}")]
        [MinLength(6, ErrorMessage = "O Nome deve ter no mínimo {1}")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O {0} é obrigatório")]
        [RegularExpression("^[0-9]{14}$", ErrorMessage = "O Cnpj deve ter exatamente 14 números")]
        public string Cnpj { get; set; } = string.Empty;

    }
}
