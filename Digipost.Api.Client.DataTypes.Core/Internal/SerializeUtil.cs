using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Digipost.Api.Client.DataTypes.Core.Internal
{
    internal static class SerializeUtil
    {
        public static string SerializeToString<T>(T value)
        {
            if (value == null)
            {
                return null;
            }

            return doSerialize(value, writer => writer.ToString());
        }

        public static XmlDocument Serialize<T>(T value)
        {
            if (value == null)
            {
                return null;
            }

            return doSerialize(value, writer =>
            {
                var doc = new XmlDocument();
                doc.Load(new StringReader(writer.ToString()));
                return doc;
            });
        }

        private static OUT doSerialize<T, OUT>(T value, Func<StringWriter, OUT> returnFunc)
        {
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "http://api.digipost.no/schema/datatypes");

            var serializer = new XmlSerializer(value.GetType());

            var settings = new XmlWriterSettings
            {
                Encoding = new UTF8Encoding(false),
                ConformanceLevel = ConformanceLevel.Document,
                Indent = false,
                OmitXmlDeclaration = true
            };

            using (var textWriter = new Utf8StringWriter())
            {
                using (var xmlWriter = XmlWriter.Create(textWriter, settings))
                {
                    serializer.Serialize(xmlWriter, value, ns);
                }

                return returnFunc.Invoke(textWriter);
            }
        }

        private sealed class Utf8StringWriter : StringWriter
        {
            public override Encoding Encoding => Encoding.UTF8;
        }
    }
}
