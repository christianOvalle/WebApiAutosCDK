using System.ComponentModel.DataAnnotations;
using WebApiAutosCDK.Validaciones;

namespace WebApiAutosCDK.Entidades
{
    public class VersionCDK
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="El campo {0} es obligatorio")]
        [PrimeraLetraMayusculaAttribute]
        [StringLength(maximumLength:200, ErrorMessage ="La {0} debe tener menos de {1} Caracteres")]
        public string versionNombre { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [StringLength(maximumLength: 100, ErrorMessage = "La {0} debe tener menos de {1} Caracteres")]
        public string potencia { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public float precioBase { get; set; }
        [StringLength(maximumLength: 50, ErrorMessage = "La {0} debe tener menos de {1} Caracteres")]
        public string combustible { get; set; } 

        public int ModeloCDKId { get; set; }

        public List<VersionCDK_ExtraCDK> versionCDK_ExtraCDKs { get; set; }


       
        
    }
}
