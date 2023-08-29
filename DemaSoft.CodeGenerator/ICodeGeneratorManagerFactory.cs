using DemaSoft.CodeGenerator.Languages;

namespace DemaSoft.CodeGenerator
{
    public interface ICodeGeneratorManagerFactory
    {
        ILanguageCodeGenerator SelectLanguageGenerator(Shared.LanguageType language);
    }
}
