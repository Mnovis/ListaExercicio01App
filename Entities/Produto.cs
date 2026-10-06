using System.ComponentModel.DataAnnotations;

namespace ListaExercicio01App.Entities
{
    public class Produto
    {
        public Guid IdProduto { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "O {0} é obrigatório")]
        [MaxLength(150, ErrorMessage = "O Nome deve ter no máximo {1}")]
        [MinLength(6, ErrorMessage = "O Nome deve ter no mínimo {1}")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O {0} é obrigatório")]
        [Range(0, double.MaxValue, MinimumIsExclusive = true, ErrorMessage = "O preço deve ser maior que zero.")]
        public double Preco { get; set; } = 0d;

        [Required(ErrorMessage = "A {0} é obrigatória")]
        [Range(1, int.MaxValue, ErrorMessage = "A Quantidade deve ser maior que zero.")]
        public int Quantidade { get; set; }

        public DateTime DataCompra { get; set; } = DateTime.Now;
    }
}
