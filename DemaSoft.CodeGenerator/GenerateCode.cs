using DemaSoft.Packager;

namespace DemaSoft.CodeGenerator
{
    public class GenerateCode: ICodeGenerator
    {
        private readonly CodeGeneratorManagerFactory _codeGeneratorManagerFactory;
        private readonly PackageManagerFactory _packageManagerFactory;
        public GenerateCode(CodeGeneratorManagerFactory codeGeneratorManagerFactory,
            PackageManagerFactory packageManagerFactory )
        {
            _codeGeneratorManagerFactory= codeGeneratorManagerFactory;
            _packageManagerFactory = packageManagerFactory;

        }
        public async Task<bool> GenerateAsync(string fileContentToGenerateCodeFrom)
        {
            // to do need to make this calls async
            var csharpGenerator = _codeGeneratorManagerFactory.SelectLanguageGenerator(Shared.LanguageType.CSharp);
          
            var csharpGeneratedCode = await csharpGenerator.GenerateAsync(fileContentToGenerateCodeFrom);

            var csharpPackageManagerFactory =
                _packageManagerFactory.SelectPublisher(Packager.Shared.PublisherType.NuGet);

            const string packageName = "TestPackage";
            const string version = "1.0.0";
        
            var outputPath = AppDomain.CurrentDomain.BaseDirectory + @"GeneratedFiles";

            if (csharpGeneratedCode != null)
                await csharpPackageManagerFactory.CreatePackage(csharpGeneratedCode, packageName, version);

            return await Task.FromResult(true); 
        }
    }
}
