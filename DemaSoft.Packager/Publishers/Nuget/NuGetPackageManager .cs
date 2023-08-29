using System.Diagnostics;

namespace DemaSoft.Packager.Publishers.Nuget
{
    public class NuGetPackageManager : IPublisher
    {
        
        public async Task<bool> CreatePackage(MemoryStream fileToPackage, string packageName, string version)
        {

            var packageCreated = false;

            // Step 1: Save the DLL content from the memory stream to a temporary file
            var tempDllPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".dll");
            await using (var fileStream = File.Create(tempDllPath))
            {
                fileToPackage.Seek(0, SeekOrigin.Begin);
                await fileToPackage.CopyToAsync(fileStream);
            }

            try
            {
                // Step 2: Create a .nuspec file with the necessary metadata
                var nuspecContent = $@"<?xml version=""1.0"" encoding=""utf-8""?>
                <package>
                    <metadata>
                        <id>YourPackageName</id>
                        <version>{version}</version>
                        <authors>DemaSoft</authors>
                        <owners>DemaSoft</owners>
                        <description>DemaSoft Code Generator</description>
                    </metadata>
                </package>";

                var outputDirectory = AppDomain.CurrentDomain.BaseDirectory + @"output";
                var nuspecPath = Path.Combine(outputDirectory, $"{packageName}.nuspec");
                await File.WriteAllTextAsync(nuspecPath, nuspecContent);

                // Step 3: Run nuget.exe to create the NuGet package
                var nugetExePath = AppDomain.CurrentDomain.BaseDirectory + @"Publishers\Nuget\nuget.exe";
                var arguments = $"pack \"{nuspecPath}\" -OutputDirectory \"{outputDirectory}\"";

                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = nugetExePath,
                    Arguments = arguments,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using Process process = new Process { StartInfo = startInfo };
                process.Start();
                await process.WaitForExitAsync();
                
                packageCreated = process.ExitCode == 0;
                
            }
            catch
            {
                return packageCreated;
            }
            finally
            {
                // Clean up the temporary DLL file and .nuspec file
                if (File.Exists(tempDllPath))
                {
                    File.Delete(tempDllPath);
                }
            }

            return packageCreated;
        }

    
        public NuGetPackageManager(){}
    }
}
