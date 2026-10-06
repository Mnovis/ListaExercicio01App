using System.ComponentModel.DataAnnotations;

namespace ListaExercicio01App.Entities
{
    public class Produto
    {
        public Guid IdProduto { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "O {0} é obrigatório")]
        [RegularExpression("^[\\p{L}\\s]+$", ErrorMessage = "O campo {0} aceita somente letras e espaços")]
        [MaxLength(150, ErrorMessage = "O Nome deve ter no máximo {1} caracteres")]
        [MinLength(6, ErrorMessage = "O Nome deve ter no mínimo {1} caracteres")]
        public string Nome { get; set; } = string.Empty;

        [Range(0, double.MaxValue, MinimumIsExclusive = true, ErrorMessage = "O preço deve ser maior que zero.")]
        public double Preco { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "A Quantidade deve ser maior que zero.")]
        public int Quantidade { get; set; }

        [Required(ErrorMessage = "A Data de Compra deve ser Preenchida.")]
        public DateTime? DataCompra { get; set; }

        public Fornecedor? Fornecedor { get; set; }
        public Categoria? Categoria { get; set; }
    }
}
