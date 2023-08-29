 

namespace DemaSoft.CodeGenerator.Languages
{
    public interface ILanguageCodeGenerator
    {
        Task<MemoryStream?> GenerateAsync(string codeToGenerateFrom);
    }
}
