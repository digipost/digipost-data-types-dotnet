namespace Digipost.Api.Client.DataTypes.Core.Common
{
    public class Address
    {
        public string StreetAddress { get; set; }
        public string StreetAddress2 { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }

    internal static class AddressToDto
    {
        internal static Internal.Address ToDto(this Address address)
        {
            return new Internal.Address
            {
                StreetAddress = address.StreetAddress,
                StreetAddress2 = address.StreetAddress2,
                PostalCode = address.PostalCode,
                City = address.City,
                Country = address.Country
            };
        }
    }
}
