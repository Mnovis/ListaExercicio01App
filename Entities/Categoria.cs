using System.ComponentModel.DataAnnotations;

namespace ListaExercicio01App.Entities
{
    public class Categoria
    {
        public Guid IdCategoria { get; set; } = Guid.NewGuid();

        [MaxLength(150, ErrorMessage = "A Descrição deve ter no máximo {1}")]
        [MinLength(6, ErrorMessage = "A Descrição deve ter no mínimo {1}")]
        public string Descricao { get; set; } = string.Empty;
    }
}
