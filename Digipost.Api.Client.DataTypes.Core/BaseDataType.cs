using System;
using System.Xml;
using Digipost.Api.Client.DataTypes.Core.Internal;

namespace Digipost.Api.Client.DataTypes.Core
{

    /// <summary>
    /// A DataType is a type that can add metadata to messages
    /// sent in Digipost. All classes implementing this interface must
    /// be mappable to the xml type to be serializable to xml.
    /// </summary>
    public interface IDigipostDataType
    {
        XmlDocument ToXmlDocument();
        string ToXmlString();
    }

    public abstract class BaseDataType<T> : IDigipostDataType
    {
        internal abstract T ToDto();

        protected string DatetimeFormatter(DateTime? dt)
        {
            return dt?.ToString("yyyy-MM-dd'T'HH:mm:ssK");
        }

        public XmlDocument ToXmlDocument()
        {
            return SerializeUtil.Serialize(ToDto());
        }

        public string ToXmlString()
        {
            return SerializeUtil.SerializeToString(ToDto());
        }
    }
}
