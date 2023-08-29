using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Newtonsoft.Json;

namespace DemaSoft.CodeGenerator.Languages.CSharp;

public class AssemblyGenerator: IAssemblyGenerator
{
    public MemoryStream? GenerateAndCompileAssemblyToMemoryStream(string className, string classCode)
    {
        SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(classCode);

        // Define necessary references
        MetadataReference[] references =
        {
        MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
        MetadataReference.CreateFromFile(typeof(JsonConvert).Assembly.Location), // Reference to Newtonsoft.Json
        MetadataReference.CreateFromFile(typeof(JsonPropertyAttribute).Assembly.Location), // Reference to Newtonsoft.Json.JsonProperty
        MetadataReference.CreateFromFile(typeof(Attribute).Assembly.Location), // Reference to System.Runtime
        MetadataReference.CreateFromFile(typeof(HttpClient).Assembly.Location), // Reference to System.Net.Http    
        MetadataReference.CreateFromFile(Assembly.Load("System.Runtime").Location), // Reference to System.Runtime
        MetadataReference.CreateFromFile(Assembly.Load("System.ComponentModel").Location), // Reference to System.ComponentModel
        MetadataReference.CreateFromFile(Assembly.Load("System.Net.Primitives").Location), // Reference to System.Net.Primitives
        MetadataReference.CreateFromFile(Assembly.Load("System.Private.Uri").Location), // Reference to System.Private.Uri
        MetadataReference.CreateFromFile(Assembly.Load("System.Linq").Location), // Reference to System.Linq
        MetadataReference.CreateFromFile(Assembly.Load("System.Collections").Location), // Reference to System.Collections
        MetadataReference.CreateFromFile(Assembly.Load("System.Runtime.Serialization.Primitives").Location), // Reference to System.Runtime.Primitives
        MetadataReference.CreateFromFile(Assembly.Load("System.ComponentModel.Annotations").Location), // Reference to System.ComponentModel.Annotations
    };

        CSharpCompilation compilation = CSharpCompilation.Create(
            Guid.NewGuid().ToString(),
            new[] { syntaxTree },
            references,
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        var ms = new MemoryStream();
        var result = compilation.Emit(ms);

        if (result.Success)
        {
            ms.Seek(0, SeekOrigin.Begin); // Reset the stream position to the beginning
            return ms;
        }
        else
        {
            // Handle compilation errors
            foreach (var diagnostic in result.Diagnostics)
            {
                Console.WriteLine(diagnostic.ToString());
            }

            ms.Dispose(); // Dispose the memory stream in case of failure
            return null;
        }

    }
}