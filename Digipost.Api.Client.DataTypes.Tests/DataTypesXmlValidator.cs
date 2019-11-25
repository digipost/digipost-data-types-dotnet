using System.IO;
using System.Reflection;
using System.Xml;
using Digipost.Api.Client.Shared.Resources.Resource;
using Digipost.Api.Client.Shared.Xml;

namespace Digipost.Api.Client.DataTypes.Tests
{
    public class DataTypesXmlValidator : XmlValidator
    {
        private static readonly ResourceUtility DataTypesResourceUtility = new ResourceUtility(Assembly.Load("Digipost.Api.Client.DataTypes.Core"), "Digipost.Api.Client.DataTypes.Core.Resources.XSD");
        
        public DataTypesXmlValidator()
        {
            AddXsd("http://api.digipost.no/schema/datatypes", GetDataTypesXsd());
        }
        
        public static XmlReader GetDataTypesXsd()
        {
            return XmlReader.Create(new MemoryStream(DataTypesResourceUtility.ReadAllBytes("datatypes.xsd")));
        }
    }
}
