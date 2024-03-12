using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Digipost.Api.Client.DataTypes.Core;
using Digipost.Api.Client.DataTypes.Core.Common;
using Digipost.Api.Client.Shared.Resources.Resource;
using Digipost.Api.Client.Shared.Resources.Xml;
using Xunit;

namespace Digipost.Api.Client.DataTypes.Tests
{
    public class DataTypesTests
    {
        private static XmlDocument _xmlDocument;
        private static readonly List<Type> DataTypes = new List<Type>();
        private readonly string _xmlExamples;

        public DataTypesTests()
        {
            ResourceUtility resourceUtility = new ResourceUtility(
                typeof(ExternalLink).Assembly, "Digipost.Api.Client.DataTypes.Core.Resources.XSD");
            var bytes = resourceUtility.ReadAllBytes("datatypes.xsd");

            ResourceUtility resourceUtilityXml = new ResourceUtility(
                typeof(ExternalLink).Assembly, "Digipost.Api.Client.DataTypes.Core.Resources.XML");
            var bytesXml = resourceUtilityXml.ReadAllBytes("datatypes-examples.xml");

            var xsdDocument = XmlUtility.ToXmlDocument(Encoding.UTF8.GetString(bytes));
            _xmlDocument = XmlUtility.ToXmlDocument(Encoding.UTF8.GetString(bytesXml));
            _xmlExamples = ToNormalizedString(_xmlDocument);

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

                    if (!DataTypes.Contains(allObjectTypes[index]))
                    {
                        DataTypes.Add(allObjectTypes[index]);
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
        public void ShareDocumentRequest()
        {
            var shareDocumentsRequest = new ShareDocumentsRequest(1209600L, "We require to see your latest pay slip in order to grant you a loan.", ["984661185"]);
            Assert.Contains(shareDocumentsRequest.ToXmlString(), _xmlExamples);
        }

        [Fact]
        public void ShareDocumentRequestEvent()
        {
            var shareDocumentsRequest = new ShareDocumentsRequestSharingStopped();
            Assert.Contains(shareDocumentsRequest.ToXmlString(), _xmlExamples);
        }

        [Fact]
        public void Inkasso()
        {
            var inkasso = new Inkasso(DateTime.Parse("2019-12-10T00:00:00+01:00"))
            {
                Sum = new decimal(42),
                Account = "01235424320",
                Kid = "1435025439583420243982723",
                Link = new ExternalLink(new Uri("https://www.example.com"))
                {
                    Description = "Gå til avsenders side for å gjøre en handling",
                    ButtonText = "Ta meg til handling!"
                }
            };
            Assert.Contains(inkasso.ToXmlString(), _xmlExamples);
        }

        [Fact]
        public void Invoice()
        {
            var invoice = new Invoice(DateTime.Parse("2020-09-10T00:00:00+02:00"), new decimal(42), "01235424320")
            {
                Kid = "1435025439583420243982723",
                Link = new ExternalLink(new Uri("https://www.example.com"))
                {
                    Description = "Gå til avsenders side for å gjøre en handling",
                    ButtonText = "Ta meg til handling!"
                }
            };
            Assert.Contains(invoice.ToXmlString(), _xmlExamples);
        }

        [Fact]
        public void Appointment()
        {
            var appointment = new Appointment(DateTime.Parse("2017-06-27T10:00:00+02:00"))
            {
                EndTime = DateTime.Parse("2017-06-27T11:00:00+02:00"),
                ArrivalTime = "Oppmøte senest 15 minutter før timen",
                Place = "Oslo City Røntgen",
                Address = new Address
                {
                    StreetAddress = "Storgata 23",
                    PostalCode = "0011",
                    City = "Oslo",
                    Country = "Norge"
                },
                Language = Language.NN,
                SubTitle = "Undersøke smerter i ryggen",
                Infos = { new Info("Informasjon om Oslo City Røntgen", "Oslo City Røntgen er et spesialistsenter for avansert bildediagnostikk.") },
                Link = new ExternalLink(new Uri("https://www.oslo.kommune.no/barnehage/svar-pa-tilbud-om-plass/"))
                {
                    Deadline = DateTime.Parse("2017-09-30T13:37:00+02:00"),
                    Description = "Oslo Kommune ber deg akseptere eller avslå tilbudet om barnehageplass.",
                    ButtonText = "Svar på barnehageplass"
                }
            };

            Assert.Contains(appointment.ToXmlString(), _xmlExamples);
            Assert.NotNull(appointment.ToXmlDocument());
        }

        [Fact]
        public void Payslip()
        {
            var payslip = new Payslip();
            Assert.Contains(payslip.ToXmlString(), _xmlExamples);
        }

        [Fact]
        public void SignedDocument()
        {
            var signedDocument = new SignedDocument("Bedrift AS", "Ansettelseskontrakt", DateTime.Parse("2018-07-11T10:00:00+02:00"));

            Assert.Contains(signedDocument.ToXmlString(), _xmlExamples);
        }

        [Fact]
        public void HasGeneratedAllDataTypes()
        {
            XmlNodeList childNodes = _xmlDocument.DocumentElement.ChildNodes;

            foreach (XmlNode child in childNodes)
            {
                string typeName = child.Name.Replace("-", "").ToUpper();
                int index = DataTypes.FindIndex(t => t.Name.ToUpper() == typeName);

                Assert.True(index >= 0);
            }
        }

        [Fact]
        public void ValidateDataTypes()
        {
            XmlNodeList childNodes = _xmlDocument.DocumentElement.ChildNodes;

            foreach (XmlNode child in childNodes)
            {
                string typeName = child.Name.Replace("-", "").ToUpper();
                int index = DataTypes.FindIndex(t => t.Name.ToUpper() == typeName);

                XmlRootAttribute rootAtt = Attribute.GetCustomAttribute(DataTypes[index], typeof (XmlRootAttribute)) as XmlRootAttribute;

                XmlSerializer serializer = new XmlSerializer(DataTypes[index], rootAtt);
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
