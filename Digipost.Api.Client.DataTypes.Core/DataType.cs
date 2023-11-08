using Digipost.Api.Client.DataTypes.Core.Internal;

namespace Digipost.Api.Client.DataTypes.Core
{
    /// <summary>
    /// A DataType is a type that can add metadata to messages
    /// sent in Digipost. All classes implementing this interface must
    /// be mappable to the xml type to be serializable to xml.
    /// </summary>
    public abstract class DataType<T>
    {
        internal abstract T ToDto();

        internal string ToXmlString()
        {
            return SerializeUtil.Serialize(ToDto());
        }
    }
}
