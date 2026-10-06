using System.ComponentModel.DataAnnotations;

namespace ListaExercicio01App.Entities
{
    public class Categoria
    {
        public Guid IdCategoria { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "A descrição é obrigatória")]
        [RegularExpression("^[\\p{L}\\s]+$", ErrorMessage = "O campo {0} aceita somente letras e espaços")]
        [MaxLength(150, ErrorMessage = "A Descrição deve ter no máximo {1} caracteres")]
        [MinLength(6, ErrorMessage = "A Descrição deve ter no mínimo {1} caracteres")]
        public string Descricao { get; set; } = string.Empty;
    }
}
