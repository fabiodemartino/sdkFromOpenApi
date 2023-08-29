using System.Diagnostics;

namespace DemaSoft.Packager
{
    public class ProcessExecutor : IProcessExecutor
    {
        public void ExecuteProcess(string fileName, string arguments, string workingDirectory)
        {
            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentException("File name cannot be null or empty.", nameof(fileName));

            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = fileName,
                Arguments = arguments,
                WorkingDirectory = workingDirectory,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using var process = new Process();
            process.StartInfo = startInfo;
#if DEBUG
            process.OutputDataReceived += (sender, e) => Console.WriteLine(e.Data);
            process.ErrorDataReceived += (sender, e) => Console.WriteLine("Error: " + e.Data);
#endif
            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            process.WaitForExit();
        }
    }
}

