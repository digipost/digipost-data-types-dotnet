namespace Digipost.Api.Client.DataTypes.Core
{
    public class Payslip : BaseDataType<Internal.Payslip>
    {
        internal override Internal.Payslip ToDto()
        {
            return new Internal.Payslip();
        }
    }
}
