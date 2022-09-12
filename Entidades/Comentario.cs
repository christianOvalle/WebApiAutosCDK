using Microsoft.AspNetCore.Identity;

namespace WebApiAutosCDK.Entidades
{
    public class Comentario
    {
        public int Id { get; set; }

        public string Contenido { get; set; }

        public int MarcaCDKId { get; set; } 

        public string UsuarioId { get; set; }

        public MarcaCDK marcaCDK { get; set; }

        public IdentityUser Usuario { get; set; }
    }
}
