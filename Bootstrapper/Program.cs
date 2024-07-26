using System;
using WixSharp;
using WixSharp.Bootstrapper;

internal class Program
{
    static void Main()
    {
        var bootstrapper =
            new Bundle(
                "MyApp",
                new ExePackage(@"C:\Users\user\source\repos\InstallTest\InstallerExe\InstallerExe.exe")
                {
                    Name = "Installer",
                    InstallCommand = "/i",
                    UninstallCommand = "/x",
                    RepairCommand = "/fa",
                    DetectCondition = "MyAppInstalled",
                    Compressed = true
                }
            );

        bootstrapper.Version = new Version("1.1.0");
        bootstrapper.UpgradeCode = new Guid("6fe30b47-2577-43ad-9095-1861ba25889b");

        // Use file search to detect if MyApp is already present on the target system
        // Alternatively you can use UtilRegistrySearch to detect the MyApp by testing registry

        bootstrapper.Include(WixExtension.Util);
        bootstrapper.AddWixFragment(
            "Wix/Bundle",
            new UtilFileSearch
            {
                Path = $@"[WindowsVolume]Users\user\source\repos\InstallTest\Ian\MyApp\MyApp.exe",
                Result = SearchResult.exists,
                Variable = "MyAppInstalled"
            });

        bootstrapper.PreserveTempFiles = false;
        bootstrapper.Build("InstallerBootstrap.exe");
    }
}