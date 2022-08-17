using System.ComponentModel.DataAnnotations;
using WebApiAutosCDK.Entidades;

namespace WebApiAutosCDK.DTOs
{
    public class ModeloCreacionDTOs : IValidatableObject
    {
        [Required(ErrorMessage = "El campo {0} Es obligatorio")]
        [StringLength(maximumLength: 100, ErrorMessage = "El campo{0} no puede tener mas de {1} caracteres")]
        public string modelo { get; set; }

        public int MarcaCDKId { get; set; }

        //public MarcaCDK MarcaCDK { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!string.IsNullOrEmpty(modelo))
            {
                var primeraLetra = modelo[0].ToString();

                if (primeraLetra != primeraLetra.ToUpper())
                {
                    yield return new ValidationResult("La primera letra debe ser mayuscula", new string[] { nameof(modelo) });
                }
            }
        }
    }
}
