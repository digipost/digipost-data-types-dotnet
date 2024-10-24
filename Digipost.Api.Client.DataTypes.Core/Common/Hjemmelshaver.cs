namespace Digipost.Api.Client.DataTypes.Core
{
    public class Hjemmelshaver
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }
    
    internal static class HjemmelshaverToDto
    {
        internal static Internal.Hjemmelshaver ToDto(this Hjemmelshaver hjemmelshaver)
        {
            return new Internal.Hjemmelshaver
            {
                Name = hjemmelshaver.Name,
                Email = hjemmelshaver.Email
            };
        }
    }
}
