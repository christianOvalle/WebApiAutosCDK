using System.ComponentModel.DataAnnotations;
using WebApiAutosCDK.Validaciones;

namespace WebApiAutosCDK.Entidades
{
    public class UbicacionDireccionCDK
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string lugar { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int ClienteCDKId { get; set; }
        public ClienteCDK clienteCDK { get; set; }
        
    }
}
