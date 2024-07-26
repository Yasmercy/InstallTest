using WixSharp;
using WixSharp.CommonTasks;

namespace InstallerDependencies
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var root = @"\Users\user\source\repos\InstallTest";
            var sln = $@"{root}\Installer";

            var installDir = @"[WindowsVolume]Users\user\source\repos\InstallTest\Ian\MyApp";

            var project = new Project(
                "InstallerDependencies",
                new Dir(installDir))
            {
                Binaries = new[]
                {
                    new Binary(new Id("DownloadBin"), @"actions\download.exe")
                }
            };

            var srcDir = $@"{sln}\Files\Bin\deps\";
            foreach (var str in System.IO.Directory.GetFiles(srcDir))
            {
                var file = str.Substring(srcDir.Length);
                project.AddAction(
                    new BinaryFileAction("DownloadBin", $@"{str} {installDir}\{file}", Return.check, When.After, Step.InstallFiles, Condition.NOT_Installed)
                    {
                        Execute = Execute.deferred
                    }
                );
                project.AddAction(
                    new BinaryFileAction("DownloadBin", $@"/u {str} {installDir}\{file}", Return.check, When.After, Step.InstallFiles, Condition.Installed)
                    {
                        Execute = Execute.deferred
                    }
                );
            }

            project.BuildMsi();
        }
    }
}
