using System;

namespace Digipost.Api.Client.DataTypes.Core
{
    public class Inkasso : BaseDataType<Internal.Inkasso>
    {
        public DateTime DueDate { get; }
        public decimal? Sum { get; set; }
        public string Account { get; set; }
        public string Kid { get; set; }

        public ExternalLink Link { get; set; }

        public Inkasso(DateTime dueDate)
        {
            DueDate = dueDate;
        }

        internal override Internal.Inkasso ToDto()
        {
            var inkasso = new Internal.Inkasso
            {
                DueDate = DatetimeFormatter(DueDate),
                Account = Account,
                Kid = Kid,
            };

            if (Sum.HasValue)
            {
                inkasso.Sum = Sum.Value;
                inkasso.SumValueSpecified = true;
            }

            if (Link != null)
            {
                inkasso.Link = Link.ToDto();
            }

            return inkasso;
        }

    }


}
