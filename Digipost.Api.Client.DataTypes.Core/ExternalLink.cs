using System;

namespace Digipost.Api.Client.DataTypes.Core
{
    public class ExternalLink : BaseDataType<Internal.ExternalLink>
    {
        public ExternalLink(Uri absoluteUri)
        {
            AbsoluteUri = absoluteUri.IsAbsoluteUri ? absoluteUri : throw new ArgumentException("Uri must be absolute");
        }

        public Uri AbsoluteUri { get; }

        public DateTime? Deadline { get; set; }

        public String Description { get; set; }

        public String ButtonText { get; set; }

        internal override Internal.ExternalLink ToDto()
        {
            var externalLink = new Internal.ExternalLink
            {
                Url = AbsoluteUri.ToString(),
                Description = Description,
                ButtonText = ButtonText
            };

            if (Deadline.HasValue)
            {
                externalLink.Deadline = Deadline.Value;
                externalLink.DeadlineValueSpecified = true;
            }

            return externalLink;
        }
    }
}
