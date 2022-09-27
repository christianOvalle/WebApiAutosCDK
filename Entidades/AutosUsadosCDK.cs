using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiAutosCDK.Entidades
{
    public class AutosUsadosCDK
    {
       
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string marca { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string modelo { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string matricula { get; set; }
        public string condicion{ get; set; }
        [Required]
        public decimal precio_tasacion { get; set; }
        [Required]
        public string years { get; set; }
        public DateTime fechaRecibido { get; set; } = DateTime.Now;
        public ClienteCDK ClienteCDK { get; set; }
        public int ClienteCDKId { get; set; }


    }
}
