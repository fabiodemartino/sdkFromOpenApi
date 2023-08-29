
using DemaSoft.Packager.Publishers;
using DemaSoft.Packager.Publishers.Npm;
using DemaSoft.Packager.Publishers.Nuget;

namespace DemaSoft.Packager
{
    public class PackageManagerFactory : IPackageManager
    {
        public IPublisher SelectPublisher(Shared.PublisherType publisher)
        {
            return publisher switch
            {
                Shared.PublisherType.NuGet => new NuGetPackageManager(),
                Shared.PublisherType.Npm => new NpmPackageManager(),
                _ => throw new ArgumentException("Invalid package manager type")
            };
        }
    }
}