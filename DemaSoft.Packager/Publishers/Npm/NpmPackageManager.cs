using System.Text;

namespace DemaSoft.Packager.Publishers.Npm
{
    public class NpmPackageManager : IPublisher
    {
        public Task<bool> CreatePackage(MemoryStream fileToPackage, string packageName, string version)
        {
            // created only for extensibility purposes
            throw new NotImplementedException();
        }
    }

    
}
