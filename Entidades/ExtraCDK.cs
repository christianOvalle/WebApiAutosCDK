using System.ComponentModel.DataAnnotations;

namespace WebApiAutosCDK.Entidades
{
    public class ExtraCDK
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [StringLength(maximumLength: 100, ErrorMessage = "La {0} debe tener menos de {1} Caracteres")]
        public string nombre { get; set; }
        [StringLength(maximumLength: 1000)]
        public string descripcion { get; set; }
       
        public List<VersionCDK_ExtraCDK> versionCDK_ExtraCDK { get; set; }
    }
}
