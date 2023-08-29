namespace DemaSoft.Packager.Publishers
{
    public interface IPublisher
    {
        Task<bool> CreatePackage(MemoryStream fileToPackage,string packageName, string version);

    }
}
