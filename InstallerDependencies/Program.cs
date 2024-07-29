using InstallerDependencies.CustomActions;
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

            // var installDir = @"Users\user\source\repos\InstallTest\Ian\MyApp\";

            var project = new Project(
                "InstallerDependencies",
                new Dir("INSTALLDIR"));

            var srcDir = $@"{sln}\Files\Bin\deps\";
            foreach (var str in System.IO.Directory.GetFiles(srcDir))
            {
                var file = str.Substring(srcDir.Length);

                // action to download files on installation
                var idInstall = $"MyCustomAction.Download_{file}.Install";
                project.AddProperty(new Property(name: idInstall, value: $"Src={str};Dest={file};Opt=/i"));
                project.AddAction(
                    new ManagedAction(DownloadActions.Download)
                    {
                        Id = idInstall,
                        Return = Return.check,
                        When = When.After,
                        Step = Step.InstallFiles,
                        Condition = Condition.NOT_Installed,
                        Execute = Execute.deferred,
                    }
                );

                // action to remove files on uninstallation
                var idUninstall = $"MyCustomAction.Download_{file}.Uninstall";
                project.AddProperty(new Property(name: idUninstall, value: $"Src={str};Dest={file};Opt=/u"));
                project.AddAction(
                    new ManagedAction(DownloadActions.Download)
                    {
                        Id = idUninstall,
                        Return = Return.check,
                        When = When.After,
                        Step = Step.InstallFiles,
                        Condition = Condition.Installed,
                        Execute = Execute.deferred,
                    }
                );
            }

            project.BuildMsi();
        }
    }
}
