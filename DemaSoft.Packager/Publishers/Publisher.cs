
namespace DemaSoft.Packager.Publishers
{
    public class Publisher: IPublisher
    {
        public Task<bool> CreatePackage(MemoryStream fileToPackage, string packageName, string version)
        {
            // created only for extensibility purposes
            throw new NotImplementedException();
        }
    }
}
