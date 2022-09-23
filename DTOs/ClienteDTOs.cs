using WebApiAutosCDK.Entidades;

namespace WebApiAutosCDK.DTOs
{
    public class ClienteDTOs
    {
        public int Id { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string cedula { get; set; }
        public string correo { get; set; }
        public AutosUsadosCDK AutosUsadosCDK { get; set; }
        public List<UbicacionDireccionCDK> ubicacionDireccionCDKs { get; set; }
        public List<DireccionClienteCDK> direccionClienteCDKs { get; set; }
    }
}
