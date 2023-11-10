namespace Digipost.Api.Client.DataTypes.Core
{
    public class ShareDocumentsRequestSharingStopped : BaseDataType<Internal.ShareDocumentsRequestSharingStopped>
    {
        internal override Internal.ShareDocumentsRequestSharingStopped ToDto()
        {
            return new Internal.ShareDocumentsRequestSharingStopped();
        }
    }
}
