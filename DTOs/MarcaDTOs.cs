using WebApiAutosCDK.Entidades;

namespace WebApiAutosCDK.DTOs
{
    public class MarcaDTOs
    {
        public int Id { get; set; }

        public string marca { get; set; }

        public List<ComentarioDTOs> comentarios { get; set; }
     
    }
}
