namespace Digipost.Api.Client.DataTypes.Core
{
    public class Heftelse
    {
        public string Panthaver { get; set; }
        public string TypePant { get; set; }
        public string Beloep { get; set; }
    }

    internal static class HeftelserToDto
    {
        internal static Internal.Heftelse ToDto(this Heftelse heftelse)
        {
            return new Internal.Heftelse
            {
                Panthaver = heftelse.Panthaver,
                TypePant = heftelse.TypePant,
                Beloep = heftelse.Beloep
            };
        }
    }
}
