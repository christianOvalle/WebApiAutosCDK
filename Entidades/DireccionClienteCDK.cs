using System.ComponentModel.DataAnnotations;

namespace WebApiAutosCDK.Entidades
{
    public class DireccionClienteCDK
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="El campo {0} es obligatorio")]
        [StringLength(maximumLength: 150, ErrorMessage = "El campo {0} no debe tener menos de {1} Caracteres")]
        public string ciudad { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string municipio { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string sector { get; set; }
        public string codigo_postal { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string calle { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string numero_casa { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int ClienteCDKId { get; set; }
        public int? UbicacionDireccionCDKId { get; set; }
        public ClienteCDK clienteCDK { get; set; }
        public UbicacionDireccionCDK direccionCDK { get; set; }

    }
}
