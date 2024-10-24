using System.Collections.Generic;
using System.Linq;
using Digipost.Api.Client.DataTypes.Core.Common;
using Digipost.Api.Client.DataTypes.Core;

namespace Digipost.Api.Client.DataTypes.Core
{
    public class Boligdetaljer : BaseDataType<Internal.Boligdetaljer>
    {
        public Residence Residence { get; }
        public List<Hjemmelshaver> Hjemmelshavers { get; }
        public List<Omsetningshistorikk> Omsetningshistorikks { get; }
        public List<Heftelse> Heftelses { get; }
        public Language Language { get; set; }
        public int Bruksareal { get; set; }
        public int AntallOppholdsrom { get; set; }
        public int AntallBaderom { get; set; }
        public string Organisasjonsnummer { get; set; }
        public string Bruksenhet { get; set; }
        public string Andelsnummer { get; set; }
        public ExternalLink CallToAction { get; set; }

        public Boligdetaljer(Residence residence)
        {
            Language = Language.NB;
            Hjemmelshavers = new List<Hjemmelshaver>();
            Omsetningshistorikks = new List<Omsetningshistorikk>();
            Heftelses = new List<Heftelse>();
            Residence = residence;
        }

        internal override Internal.Boligdetaljer ToDto()
        {
            var boligdetaljer = new Internal.Boligdetaljer
            {
                Residence = Residence.ToDto(),
                Bruksareal = Bruksareal,
                AntallOppholdsrom = AntallOppholdsrom,
                AntallBaderom = AntallBaderom,
                Organisasjonsnummer = Organisasjonsnummer,
                Bruksenhet = Bruksenhet,
                Andelsnummer = Andelsnummer
            };

            boligdetaljer.Language = Language.ToDto();
            
            if (CallToAction != null)
            {
                boligdetaljer.CallToAction = CallToAction.ToDto();
            }

            foreach (var hh in Hjemmelshavers.Select(hjemmelshaver => hjemmelshaver.ToDto()))
            {
                boligdetaljer.Hjemmelshavere.Add(hh);
            }

            foreach (var oh in Omsetningshistorikks.Select(omsetningshistorikk => omsetningshistorikk.ToDto()))
            {
                boligdetaljer.Omsetningshistorikk.Add(oh);
            }

            foreach (var h in Heftelses.Select(heftelse => heftelse.ToDto()))
            {
                boligdetaljer.Heftelser.Add(h);
            }

            return boligdetaljer;
        }
    }
}
