using System.ComponentModel.DataAnnotations;

namespace WebApiAutosCDK.Entidades
{
    public class VehiculoCDK
    {
        public int Id { get; set; }
        public int cantidadDisponible { get; set; } = 0;
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name ="Precio de entrada")]
        public decimal precioEntrada { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public decimal precioSalida { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string color { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string cantidad_puertas { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string velocidad { get; set; }
        public string cantidadGomas  { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string matricula { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string año { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int MarcaCDKId { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int ModeloCDKId { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int VersionCDKId { get; set; }
        public ClienteCDK ClienteCDK { get; set; }
        public ModeloCDK ModeloCDK { get; set; }
        public VersionCDK VersionCDK { get; set; }


    }
}
