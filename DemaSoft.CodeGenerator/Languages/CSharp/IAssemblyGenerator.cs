namespace DemaSoft.CodeGenerator.Languages.CSharp
{
    public interface IAssemblyGenerator
    {
        MemoryStream? GenerateAndCompileAssemblyToMemoryStream(string className, string classCode);
    }
}
