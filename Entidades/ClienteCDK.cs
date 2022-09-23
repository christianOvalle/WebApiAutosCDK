namespace WebApiAutosCDK.Entidades
{
    public class ClienteCDK
    {
        public int Id { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string cedula { get; set; }
        public DateTime fechaRegistro { get; set; } = DateTime.Now;
        public string correo { get; set; }
        public List<UbicacionDireccionCDK> ubicacionDireccionCDKs { get; set; }
        public List<DireccionClienteCDK> direccionClienteCDKs { get; set; }
    }
}
