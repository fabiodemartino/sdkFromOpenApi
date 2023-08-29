using DemaSoft.CodeGenerator.Languages.CSharp;
using DemaSoft.CodeGenerator.Languages.TypeScript;
using DemaSoft.CodeGenerator.Languages;

namespace DemaSoft.CodeGenerator
{
    public class CodeGeneratorManagerFactory: ICodeGeneratorManagerFactory

    {
        private readonly IAssemblyGenerator _assemblyGenerator;
        public CodeGeneratorManagerFactory(IAssemblyGenerator assemblyGenerator)
        {
            _assemblyGenerator = assemblyGenerator;
        }
        public ILanguageCodeGenerator SelectLanguageGenerator(Shared.LanguageType language)
        {
            return language switch
            {
                Shared.LanguageType.CSharp => new CSharpLanguageGenerator(_assemblyGenerator),
                Shared.LanguageType.TypeScript => new TypeScriptLanguageGenerator(),
                _ => throw new ArgumentException("Code Generator for language passed is not supported")
            };
        }
    }
}
