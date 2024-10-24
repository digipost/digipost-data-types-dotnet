namespace Digipost.Api.Client.DataTypes.Core
{
    public class Residence
    {
        public ResidenceAddress ResidenceAddress { get; }
        public Matrikkel Matrikkel { get; set; }
        public string Source { get; set; }
        public string ExternalId { get; set; }

        public Residence(ResidenceAddress residenceAddress)
        {
            ResidenceAddress = residenceAddress;
        }
    }
    
    internal static class ResidenceToDto
    {
        internal static Internal.Residence ToDto(this Residence residence)
        {
            var residenceAddress = new Internal.ResidenceAddress()
            {
                UnitNumber = residence.ResidenceAddress.UnitNumber,
                HouseNumber = residence.ResidenceAddress.HouseNumber,
                StreetName = residence.ResidenceAddress.StreetName,
                PostalCode = residence.ResidenceAddress.PostalCode,
                City = residence.ResidenceAddress.City
            };

            var res = new Internal.Residence
            {
                Address = residenceAddress,
                Source = residence.Source,
                ExternalId = residence.ExternalId
            };

            if (residence.Matrikkel != null)
            {
                res.Matrikkel = residence.Matrikkel.ToDto();
            }            
            return res;
        }
    }
}
