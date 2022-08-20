namespace WebApiAutosCDK.Entidades
{
    public class VersionCDK_ExtraCDK
    {
       
        public int VersionCDKId { get; set; }

        public int ExtrasCDKId  { get; set; }

        public ExtraCDK extraCDK { get; set; }

        public VersionCDK versionCDK { get; set; }
    }
}
