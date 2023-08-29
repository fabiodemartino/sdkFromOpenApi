using DemaSoft.Packager.Publishers;
using static DemaSoft.Packager.PackageManagerFactory;

namespace DemaSoft.Packager
{
    public interface IPackageManager
    {
        //bool Package(Tuple<int, string> artifactList, Shared.PackageManagerType packagerType);
       
        IPublisher SelectPublisher(Shared.PublisherType publisher);
    }
}
