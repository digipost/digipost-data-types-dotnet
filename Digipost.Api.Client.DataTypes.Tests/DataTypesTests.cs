using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;
using Digipost.Api.Client.DataTypes.Core;
using Digipost.Api.Client.Shared.Resources.Resource;
using Digipost.Api.Client.Shared.Resources.Xml;
using Xunit;

namespace Digipost.Api.Client.DataTypes.Tests
{
    public class DataTypesTests
    {
        private static XmlDocument xmlDocument;
        private static List<Type> dataTypes = new List<Type>();
        private string _xmlExamples;

        public DataTypesTests()
        {
            ResourceUtility resourceUtility = new ResourceUtility(
                typeof(ExternalLink).Assembly, "Digipost.Api.Client.DataTypes.Core.Resources.XSD");
            var bytes = resourceUtility.ReadAllBytes("datatypes.xsd");

            ResourceUtility resourceUtilityXml = new ResourceUtility(
                typeof(ExternalLink).Assembly, "Digipost.Api.Client.DataTypes.Core.Resources.XML");
            var bytesXml = resourceUtilityXml.ReadAllBytes("datatypes-examples.xml");

            var xsdDocument = XmlUtility.ToXmlDocument(Encoding.UTF8.GetString(bytes));
            xmlDocument = XmlUtility.ToXmlDocument(Encoding.UTF8.GetString(bytesXml));
            _xmlExamples = ToNormalizedString(xmlDocument);

            var asm = Assembly.Load("Digipost.Api.Client.DataTypes.Core");

            var allObjectTypes = asm.GetTypes().Where(p =>
                p.Namespace == "Digipost.Api.Client.DataTypes.Core.Internal"
            ).ToList();

            foreach (XmlNode child in xsdDocument.DocumentElement)
            {
                if (child.Name == "xs:element")
                {
                    string typeName = child.Attributes["name"].Value.Replace("-", "").ToUpper();
                    int index = allObjectTypes.FindIndex(t => t.Name.ToUpper() == typeName);

                    if (!dataTypes.Contains(allObjectTypes[index]))
                    {
                        dataTypes.Add(allObjectTypes[index]);
                    }
                }
            }
        }

        [Fact]
        public void ExternalLink()
        {
            var externalLink = new ExternalLink(new Uri("https://www.oslo.kommune.no/barnehage/svar-pa-tilbud-om-plass/"))
            {
                Deadline = DateTime.Parse("2017-09-30T13:37:00+02:00"),
                ButtonText = "Svar på barnehageplass",
                Description = "Oslo Kommune ber deg akseptere eller avslå tilbudet om barnehageplass."
            };
            Assert.Contains(externalLink.ToXmlString(), _xmlExamples);
        }

        [Fact]
        public void HasGeneratedAllDataTypes()
        {
            XmlNodeList childNodes = xmlDocument.DocumentElement.ChildNodes;

            foreach (XmlNode child in childNodes)
            {
                string typeName = child.Name.Replace("-", "").ToUpper();
                int index = dataTypes.FindIndex(t => t.Name.ToUpper() == typeName);

                Assert.True(index >= 0);
            }
        }

        [Fact]
        public void ValidateDataTypes()
        {
            XmlNodeList childNodes = xmlDocument.DocumentElement.ChildNodes;

            foreach (XmlNode child in childNodes)
            {
                string typeName = child.Name.Replace("-", "").ToUpper();
                int index = dataTypes.FindIndex(t => t.Name.ToUpper() == typeName);

                XmlRootAttribute rootAtt = Attribute.GetCustomAttribute(dataTypes[index], typeof (XmlRootAttribute)) as XmlRootAttribute;

                XmlSerializer serializer = new XmlSerializer(dataTypes[index], rootAtt);
                XmlReader xmlReader = new XmlNodeReader(child);

                object dataType = serializer.Deserialize(xmlReader);

                XmlDocument xmldoc = new XmlDocument();

                xmldoc.LoadXml(ToXml(serializer, dataType));

                ValidateXml(xmldoc);
            }
        }

        private void ValidateXml(XmlDocument document)
        {
            if (document.InnerXml.Length == 0)
            {
                return;
            }

            var xmlValidator = new DataTypesXmlValidator();

            var isValidXml = xmlValidator.Validate(document.InnerXml, out string _);

            Assert.True(isValidXml);
        }

        private string ToXml<T>(XmlSerializer serializer, T value)
        {
            var settings = new XmlWriterSettings
            {
                Encoding = new UTF8Encoding(false),
                ConformanceLevel = ConformanceLevel.Document,
                Indent = false,
                OmitXmlDeclaration = false
            };

            using (var textWriter = new Utf8StringWriter())
            {
                using (var xmlWriter = XmlWriter.Create(textWriter, settings))
                {
                    serializer.Serialize(xmlWriter, value);
                }
                return textWriter.ToString();
            }
        }

        private sealed class Utf8StringWriter : StringWriter
        {
            public override Encoding Encoding => Encoding.UTF8;
        }

        public static string ToNormalizedString(XmlDocument doc )
        {
            var stringWriter = new StringWriter(new StringBuilder());
            var xmlTextWriter = new XmlTextWriter(stringWriter) {Formatting = Formatting.None};
            doc.Save( xmlTextWriter );
            return stringWriter.ToString();
        }
    }
}
