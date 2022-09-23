namespace WebApiAutosCDK.Entidades
{
    public class UbicacionDireccionCDK
    {
        public int Id { get; set; }
        public string lugar { get; set; }
        public int ClienteCDKId { get; set; }
        public ClienteCDK clienteCDK { get; set; }
        
    }
}
