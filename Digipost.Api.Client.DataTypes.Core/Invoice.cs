using System;

namespace Digipost.Api.Client.DataTypes.Core
{
    public class Invoice : BaseDataType<Internal.Invoice>
    {
        public DateTime DueDate { get; }
        public decimal Sum { get; }
        public string CreditorAccount { get; }
        public string Kid { get; set; }

        public ExternalLink Link { get; set; }

        public Invoice(DateTime dueDate, decimal sum, string creditorAccount)
        {
            DueDate = dueDate;
            Sum = sum;
            CreditorAccount = creditorAccount;
        }

        internal override Internal.Invoice ToDto()
        {
            var invoice = new Internal.Invoice()
            {
                DueDate = DatetimeFormatter(DueDate),
                Sum = Sum,
                CreditorAccount = CreditorAccount,
                Kid = Kid,
            };

            if (Link != null)
            {
                invoice.Link = Link.ToDto();
            }

            return invoice;
        }

    }


}
