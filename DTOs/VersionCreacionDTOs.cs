using System.ComponentModel.DataAnnotations;
using WebApiAutosCDK.Entidades;
using WebApiAutosCDK.Validaciones;

namespace WebApiAutosCDK.DTOs
{
    public class VersionCreacionDTOs
    {
     
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [PrimeraLetraMayusculaAttribute]
        [StringLength(maximumLength: 200, ErrorMessage = "La {0} debe tener menos de {1} Caracteres")]
        public string versionNombre { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [StringLength(maximumLength: 100, ErrorMessage = "La {0} debe tener menos de {1} Caracteres")]
        public string potencia { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public float precioBase { get; set; }
        [StringLength(maximumLength: 50, ErrorMessage = "La {0} debe tener menos de {1} Caracteres")]
        public string combustible { get; set; }

        public int ModeloCDKId { get; set; }

        public List<int> ExtrasIds { get; set; }


    }
}
