using WixSharp;
using WixSharp.CommonTasks;

namespace InstallerDependencies
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var srcDir = $@"\\192.168.10.240\設備部門\設備軟體\gitServer\deps\";

            var project = new Project(
                "InstallerDependencies",
                new Dir("INSTALLDIR"))
            {
                Binaries = new[]
                {
                    new Binary(new Id("DownloadBin"), @"actions\download.exe")
                }
            };

            foreach (var str in System.IO.Directory.GetFiles(srcDir))
            {
                var file = str.Substring(srcDir.Length);
                project.AddAction(
                    new BinaryFileAction("DownloadBin", $@"/i [INSTALLDIR] {file}", Return.check, When.After, Step.InstallFiles, Condition.NOT_Installed)
                    {
                        Execute = Execute.deferred
                    }
                );
                project.AddAction(
                    new BinaryFileAction("DownloadBin", $@"/u [INSTALLDIR] {file}", Return.check, When.After, Step.InstallFiles, Condition.Installed)
                    {
                        Execute = Execute.deferred
                    }
                );
            }

            project.BuildMsi();
        }
    }
}
