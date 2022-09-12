using WebApiAutosCDK.Entidades;

namespace WebApiAutosCDK.DTOs
{
    public class ModeloDTOs
    {
        public int Id { get; set; }

        public string modelo { get; set; }

        public DateTime FechaCreacion { get; set; }

        public MarcaDTOs MarcaDTOs { get; set; }

    }
}
