namespace WebApiAutosCDK.Entidades
{
    public class DireccionClienteCDK
    {
        public int Id { get; set; }
        public string ciudad { get; set; }
        public string municipio { get; set; }
        public string sector { get; set; }
        public string codigo_postal { get; set; }
        public string calle { get; set; }
        public string numero_casa { get; set; }
        public int ClienteCDKId { get; set; }
        public int? UbicacionDireccionCDKId { get; set; }
        public ClienteCDK clienteCDK { get; set; }
        public UbicacionDireccionCDK direccionCDK { get; set; }

    }
}
