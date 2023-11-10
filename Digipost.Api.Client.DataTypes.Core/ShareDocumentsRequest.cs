namespace Digipost.Api.Client.DataTypes.Core
{
    public class ShareDocumentsRequest : BaseDataType<Internal.ShareDocumentsRequest>
    {
        public ShareDocumentsRequest(long maxShareDurationSeconds, string purpose)
        {
            MaxShareDurationSeconds = maxShareDurationSeconds;
            Purpose = purpose;
        }

        public long MaxShareDurationSeconds { get; }
        public string Purpose { get; }

        internal override Internal.ShareDocumentsRequest ToDto()
        {
            return new Internal.ShareDocumentsRequest()
            {
                MaxShareDurationSeconds = MaxShareDurationSeconds,
                Purpose = Purpose
            };
        }
    }
}
