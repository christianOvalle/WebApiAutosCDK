using System.ComponentModel.DataAnnotations;
using WebApiAutosCDK.Validaciones;

namespace WebApiAutosCDK.Entidades
{
    public class VendedorCDK
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="El campo {0} es obligatorio")]
        [StringLength(maximumLength:100, ErrorMessage ="El nombre esta muy largo")]
        [PrimeraLetraMayusculaAttribute]
        public string nombre { get; set; }
        [Required(ErrorMessage = "Esta {0} campo es obligatorio")]
        [StringLength(maximumLength: 100, ErrorMessage = "El nombre esta muy largo")]
        [PrimeraLetraMayusculaAttribute]
        public string apellido { get; set; }
        [Required(ErrorMessage = "Esta {0} campo es obligatorio")]
        public string direccion { get; set; }
        [Required(ErrorMessage = "Esta {0} campo es obligatorio")]
        public string cedula{ get; set; }
        [Phone]
        public string telefono { get; set; }
        [EmailAddress]
        public string correo { get; set; }
    }
}
