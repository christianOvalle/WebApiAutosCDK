using System.ComponentModel.DataAnnotations;
using WebApiAutosCDK.Validaciones;

namespace WebApiAutosCDK.Entidades
{
    public class MarcaCDK
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 150, ErrorMessage = "El campo {0} no debe tener menos de {1} Caracteres")]
        [PrimeraLetraMayusculaAttribute]
        public string  marca { get; set; }    

        public List<ModeloCDK> Modelos { get; set; } 

        public List<Comentario> comentarios { get; set; }
    }
}
