namespace DemaSoft.Packager
{
    public interface IProcessExecutor
    {
        void ExecuteProcess(string fileName, string arguments, string workingDirectory);
    }

}
