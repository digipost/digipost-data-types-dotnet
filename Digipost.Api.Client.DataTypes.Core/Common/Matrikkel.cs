namespace Digipost.Api.Client.DataTypes.Core
{
    public class Matrikkel
    {
        public string Kommunenummer { get; }
        public string Gaardsnummer { get; }
        public string Bruksnummer { get; }
        public string Festenummer { get; set; }
        public string Seksjonsnummer { get; set; }

        public Matrikkel(string kommunenummer, string gaardsnummer, string bruksnummer)
        {
            Kommunenummer = kommunenummer;
            Gaardsnummer = gaardsnummer;
            Bruksnummer = bruksnummer;
        }
    }
    
    internal static class MatrikkelToDto
    {
        internal static Internal.Matrikkel ToDto(this Matrikkel matrikkel)
        {
            return new Internal.Matrikkel
            {
                Kommunenummer = matrikkel.Kommunenummer,
                Gaardsnummer = matrikkel.Gaardsnummer,
                Bruksnummer = matrikkel.Bruksnummer,
                Festenummer = matrikkel.Festenummer,
                Seksjonsnummer = matrikkel.Seksjonsnummer
            };
        }
    }
    
}
