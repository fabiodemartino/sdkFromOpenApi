namespace DemaSoft.CodeGenerator
{
    public interface ICodeGenerator
    {
        Task<bool> GenerateAsync(string fileContentToGenerateCodeFrom);

    }
}
