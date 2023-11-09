using System;

namespace Digipost.Api.Client.DataTypes.Core
{
    public class SignedDocument : BaseDataType<Internal.SignedDocument>
    {
        public string DocumentIssuer { get; }
        public string DocumentSubject { get; }
        public DateTime SigningTime { get; }

        public SignedDocument(string documentIssuer, string documentSubject, DateTime signingTime)
        {
            DocumentIssuer = documentIssuer;
            DocumentSubject = documentSubject;
            SigningTime = signingTime;
        }

        internal override Internal.SignedDocument ToDto()
        {
            return new Internal.SignedDocument()
            {
                DocumentIssuer = DocumentIssuer,
                DocumentSubject = DocumentSubject,
                SigningTime = DatetimeFormatter(SigningTime)
            };
        }
    }
}
