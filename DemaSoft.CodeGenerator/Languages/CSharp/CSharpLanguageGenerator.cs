using NSwag.CodeGeneration.CSharp;
using NSwag;

namespace DemaSoft.CodeGenerator.Languages.CSharp
{

    public class CSharpLanguageGenerator : ICSharpLanguageGenerator
    {
        private readonly IAssemblyGenerator _assemblyGenerator;

        public CSharpLanguageGenerator(IAssemblyGenerator assemblyGenerator)
        {
            _assemblyGenerator = assemblyGenerator;
        }

        public async Task<MemoryStream?> GenerateAsync(string codeToGenerateFrom)
        {
            const string className = "TestClass";
            const string nameSpace = "TestNameSpace";
            var settings = new CSharpClientGeneratorSettings
            {
                ClassName = className,
                CSharpGeneratorSettings =
                {
                    Namespace = nameSpace
                }
            };

            var document = await OpenApiDocument.FromJsonAsync(codeToGenerateFrom);
            var generator = new CSharpClientGenerator(document, settings);
            var generateCode = generator.GenerateFile();

            var stream = _assemblyGenerator.GenerateAndCompileAssemblyToMemoryStream(settings.ClassName.ToString(),
                generateCode);

            stream?.Seek(0, SeekOrigin.Begin); // Reset the stream position to the beginning

            return stream;
        }
    }
}
