namespace Digipost.Api.Client.DataTypes.Core
{
    public class ShareDocumentsRequest : DataType<Internal.ShareDocumentsRequest>
    {
        public ShareDocumentsRequest(long maxShareDurationSeconds, string purpose)
        {
            MaxShareDurationSeconds = maxShareDurationSeconds;
            Purpose = purpose;
        }

        public long MaxShareDurationSeconds { get; }
        public string Purpose { get; }

        protected override Internal.ShareDocumentsRequest ToDto()
        {
            return new Internal.ShareDocumentsRequest()
            {
                Max_Share_Duration_Seconds = MaxShareDurationSeconds,
                Purpose = Purpose
            };
        }
    }
}
