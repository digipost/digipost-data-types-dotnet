using System.Collections.ObjectModel;

namespace Digipost.Api.Client.DataTypes.Core
{
    public class ShareDocumentsRequest : BaseDataType<Internal.ShareDocumentsRequest>
    {
        public ShareDocumentsRequest(long maxShareDurationSeconds, string purpose)
        {
            MaxShareDurationSeconds = maxShareDurationSeconds;
            Purpose = purpose;
            AllowedOriginOrganisationNumbers = new Collection<string>();
        }

        public ShareDocumentsRequest(long maxShareDurationSeconds, string purpose, Collection<string> allowedOriginOrganisationNumbers)
        {
            MaxShareDurationSeconds = maxShareDurationSeconds;
            Purpose = purpose;
            AllowedOriginOrganisationNumbers = allowedOriginOrganisationNumbers;
        }

        public long MaxShareDurationSeconds { get; }
        public string Purpose { get; }
        public Collection<string> AllowedOriginOrganisationNumbers { get;  }

        internal override Internal.ShareDocumentsRequest ToDto()
        {
            var shareDocumentsRequest = new Internal.ShareDocumentsRequest()
            {
                MaxShareDurationSeconds = MaxShareDurationSeconds,
                Purpose = Purpose
            };
            
            foreach (var allowedOriginOrganisationNumber in AllowedOriginOrganisationNumbers)
            {
                shareDocumentsRequest.AllowedOriginOrganisationNumbers.Add(allowedOriginOrganisationNumber);    
            }
            
            return shareDocumentsRequest;
        }
    }
}
