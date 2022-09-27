using System.ComponentModel.DataAnnotations;
using WebApiAutosCDK.Validaciones;

namespace WebApiAutosCDK.Entidades
{
    public class ClienteCDK
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="El campo {0} es obligatorio")]
        [PrimeraLetraMayusculaAttribute(ErrorMessage ="La primera letra debe ser mayuscula")]
        [StringLength(maximumLength:50, ErrorMessage ="El {0} no puede tener mas de {1} caracteres")]
        public string nombre { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [PrimeraLetraMayusculaAttribute(ErrorMessage = "La primera letra debe ser mayuscula")]
        [StringLength(maximumLength: 50, ErrorMessage = "El {0} no puede tener mas de {1} caracteres")]
        public string apellido { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Range(0, 11, ErrorMessage ="El numero excede el rango de una cedula")]
        public string cedula { get; set; }
        public DateTime fechaRegistro { get; set; } = DateTime.Now;
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [EmailAddress(ErrorMessage ="Debe ingresar un correo valido")]
        public string correo { get; set; }
        public List<UbicacionDireccionCDK> ubicacionDireccionCDKs { get; set; }
        public List<DireccionClienteCDK> direccionClienteCDKs { get; set; }
    }
}
