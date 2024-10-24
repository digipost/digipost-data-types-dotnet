namespace Digipost.Api.Client.DataTypes.Core
{
    public class Omsetningshistorikk
    {
        public string Dato { get; }
        public string Beskrivelse { get; set; }
        public string Selger { get; set; }
        public string Kjoeper { get; set; }
        public long Beloep { get; set; }

        public Omsetningshistorikk(string dato)
        {
            Dato = dato;
        }
    }
    
    internal static class OmsetningshistorikkToDto
    {
        internal static Internal.Omsetningshistorikk ToDto(this Omsetningshistorikk omsetningshistorikk)
        {
            return new Internal.Omsetningshistorikk
            {
                Dato = omsetningshistorikk.Dato,
                Beskrivelse = omsetningshistorikk.Beskrivelse,
                Selger = omsetningshistorikk.Selger,
                Kjoeper = omsetningshistorikk.Kjoeper,
                Beloep = omsetningshistorikk.Beloep
            };
        }
    }
}
