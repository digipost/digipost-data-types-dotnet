using Digipost.Api.Client.DataTypes.Core.Common;

namespace Digipost.Api.Client.DataTypes.Core
{
    public class Residence : BaseDataType<Internal.Residence>
    {
        public ResidenceAddress ResidenceAddress { get; }
        public Matrikkel Matrikkel { get; set; }
        public string Source { get; set; }
        public string ExternalId { get; set; }

        public Residence(ResidenceAddress residenceAddress)
        {
            ResidenceAddress = residenceAddress;
        }

        internal override Internal.Residence ToDto()
        {
            var residenceAddress = new Internal.ResidenceAddress()
            {
                UnitNumber = ResidenceAddress.UnitNumber,
                HouseNumber = ResidenceAddress.HouseNumber,
                StreetName = ResidenceAddress.StreetName,
                PostalCode = ResidenceAddress.PostalCode,
                City = ResidenceAddress.City
            };

            var res = new Internal.Residence
            {
                Address = residenceAddress,
                Source = Source,
                ExternalId = ExternalId
            };

            if (Matrikkel != null)
            {
                res.Matrikkel = Matrikkel.ToDto();
            }
            return res;
        }
    }
}
