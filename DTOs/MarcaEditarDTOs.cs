using System.ComponentModel.DataAnnotations;
using WebApiAutosCDK.Validaciones;

namespace WebApiAutosCDK.DTOs
{
    public class MarcaEditarDTOs
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 150, ErrorMessage = "El campo {0} no debe tener menos de {1} Caracteres")]
        [PrimeraLetraMayusculaAttribute]
        public string marca { get; set; }
    }
}
