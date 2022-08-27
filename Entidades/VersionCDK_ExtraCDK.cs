namespace WebApiAutosCDK.Entidades
{
    public class VersionCDK_ExtraCDK
    {
       
        public int VersionCDKId { get; set; }

        public int ExtraCDKId  { get; set; }

        public ExtraCDK Extra { get; set; }

        public VersionCDK version { get; set; }
    }
}
