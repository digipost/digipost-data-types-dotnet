using System.CodeDom;
using System.IO;
using XmlSchemaClassGenerator;

namespace Digipost.Api.Client.DataTypes.Utils
{
    public class SingleFileOutputWriter : FileOutputWriter
    {
        public SingleFileOutputWriter(string directory, bool createIfNotExists = true)
            : base(directory, createIfNotExists)
        {
        }

        public string OutputFile { get; set; }

        public override void Write(CodeNamespace cn)
        {
            var cu = new CodeCompileUnit();
            cu.Namespaces.Add(cn);

            if (string.IsNullOrWhiteSpace(OutputFile))
            {
                OutputFile = $"{cn.Name}.cs";
            }

            WriteFile(Path.Combine(OutputDirectory, OutputFile), cu);
        }
    }
}
