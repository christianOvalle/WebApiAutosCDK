using System.ComponentModel.DataAnnotations;

namespace WebApiAutosCDK.DTOs
{
    public class ExtraCreacionDTOs
    {
        
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [StringLength(maximumLength: 100, ErrorMessage = "La {0} debe tener menos de {1} Caracteres")]
        public string nombre { get; set; }
        [StringLength(maximumLength: 1000)]
        public string descripcion { get; set; }

    }
}
