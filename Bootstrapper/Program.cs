using System;
using WixSharp;
using WixSharp.Bootstrapper;

internal class Program
{
    static void Main()
    {
        var installDir = @"[WindowsVolume]Users\user\source\repos\InstallTest\Ian\MyApp";

        var bootstrapper =
            new Bundle(
                "MyApp",
                // new MsiPackage(@"C:\Users\user\source\repos\InstallTest\InstallerDependencies\InstallerDependencies.msi")
                // {
                //     Name = "InstallerDeps",
                //     MsiProperties = $"INSTALLDIR={installDir}",
                //     Compressed = true
                // },
                new ExePackage(@"C:\Users\user\source\repos\InstallTest\InstallerExe\InstallerExe.exe")
                {
                    Name = "Installer",
                    InstallCommand = $"/i INSTALLDIR={installDir}",
                    UninstallCommand = $"/x INSTALLDIR={installDir}",
                    RepairCommand = $"/fa INSTALLDIR={installDir}",
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
                Path = $@"{installDir}\MyApp.exe",
                Result = SearchResult.exists,
                Variable = "MyAppInstalled"
            });

        bootstrapper.PreserveTempFiles = false;
        bootstrapper.Build("InstallerBootstrap.exe");
    }
}