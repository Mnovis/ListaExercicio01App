using System.ComponentModel.DataAnnotations;

namespace ListaExercicio01App.Validators
{
    public class ObjetoValidator
    {
        public static bool ValidarObjeto(object objeto)
        {
            var validation = new ValidationContext(objeto);
            var errors = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(
                objeto,
                validation,
                errors,
                validateAllProperties: true
            );

            if (!isValid)
            {
                foreach (var item in errors)
                {
                    Console.WriteLine("\tERRO: " + item.ErrorMessage);
                }
            }

            return isValid;
        }
    }
}
