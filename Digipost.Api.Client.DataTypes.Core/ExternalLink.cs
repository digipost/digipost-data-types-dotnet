using System;

namespace Digipost.Api.Client.DataTypes.Core
{
    public class ExternalLink : DataType<Internal.ExternalLink>
    {
        public ExternalLink(Uri absoluteUri)
        {
            AbsoluteUri = absoluteUri.IsAbsoluteUri ? absoluteUri : throw new ArgumentException("Uri must be absolute");
        }

        public Uri AbsoluteUri { get; }

        public DateTime Deadline { get; set; }

        public String Description { get; set; }

        public String ButtonText { get; set; }

        protected override Internal.ExternalLink ToDto()
        {
            return new Internal.ExternalLink
            {
                Url = AbsoluteUri.ToString(),
                Deadline = Deadline,
                Description = Description,
                Button_Text = ButtonText
            };
        }
    }
}
