using System;
using System.Collections.Generic;
using XmlSchemaClassGenerator;

namespace Digipost.Api.Client.DataTypes.Utils
{
    public class GenerationUtil
    {
        static void Main(string[] args)
        {
            Generator generator = new Generator
            {
                OutputFolder = "../Digipost.Api.Client.DataTypes.Core/",
                Log = s => Console.Out.WriteLine(s),
                GenerateNullables = true,
                DisableComments = true,
                GenerateInterfaces = true,
                OutputWriter = new SingleFileOutputWriter("../Digipost.Api.Client.DataTypes.Core/") { OutputFile = "DataTypes.cs"},
                GenerateComplexTypesForCollections = true,
                NamespaceProvider = new Dictionary<NamespaceKey, string>
                {
                    { new NamespaceKey("http://api.digipost.no/schema/datatypes"), "Digipost.Api.Client.DataTypes.Core" }
                }.ToNamespaceProvider(new GeneratorConfiguration{ NamespacePrefix = "DataTypes" }.NamespaceProvider.GenerateNamespace)
            };
            
            generator.Generate(new List<string>{ "../Digipost.Api.Client.DataTypes.Core/Resources/XSD/datatypes.xsd" });
        }
    }
}
