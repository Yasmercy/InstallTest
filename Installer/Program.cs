using System;
using WixSharp;
using WixSharp.Custom_Dialogs;
using WixSharp.Forms;

namespace Installer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var root = @"C:\Users\user\source\repos\InstallTest";
            var sln = $@"{root}\Installer";

            var feature1 = new Feature("Featuer1");
            var feature2 = new Feature("Featuer2");

            var project = new ManagedProject(
                "MyApp",
                new Dir(
                    "INSTALLDIR",
                    new File($@"{sln}\Files\Docs\readme.md"),
                    new File($@"{sln}\Files\Bin\MyApp.exe"),
                    new File($@"{sln}\Files\Bin\MyApp.dll"),
                    new File($@"{sln}\Files\Bin\MyApp.pdb"),
                    new File($@"{sln}\Files\Bin\MyApp.runtimeconfig.json"),
                    new File($@"{sln}\Files\Bin\nlog.config")
                ),
                new Dir(@"%Desktop%",
                    new ExeFileShortcut("MyApp", System.IO.Path.Combine("[INSTALLDIR]", "MyApp.exe"), arguments: "") 
                    { WorkingDirectory = "[INSTALLDIR]" }
                ),
                new Dir(@"%ProgramMenu%",
                    new ExeFileShortcut("MyApp", System.IO.Path.Combine("[INSTALLDIR]", "MyApp.exe"), arguments: "") 
                    { WorkingDirectory = "[INSTALLDIR]" }
                ),
                new IniFile(feature1, "data.ini", "INSTALLDIR", IniFileAction.createLine, "Form1", "Include", "true"),
                new IniFile(feature2, "data.ini", "INSTALLDIR", IniFileAction.createLine, "Form2", "Include", "true")
            );


            // project.Media.Clear();
            // project.AddXmlElement("Wix/Package", "MediaTemplate", "CompressionLevel=high; EmbedCab=yes");

            project.GUID = new Guid("6fe30b47-2577-43ad-9095-1861ba25889b");
            project.Version = new Version("1.1.0");

            project.MajorUpgradeStrategy = MajorUpgradeStrategy.Default;
            project.MajorUpgradeStrategy.RemoveExistingProductAfter = Step.InstallInitialize;
            project.MajorUpgradeStrategy.PreventDowngradingVersions.OnlyDetect = false;
            project.MajorUpgradeStrategy.PreventDowngradingVersions.MigrateFeatures = true;

            // project.EmbeddedUI = new EmbeddedAssembly(System.Reflection.Assembly.GetExecutingAssembly().Location);

            project.ManagedUI = new ManagedUI();

            project.ManagedUI.InstallDialogs.Add(typeof(MyWelcome))
                                            .Add(Dialogs.Licence)
                                            .Add(Dialogs.SetupType)
                                            .Add(Dialogs.Features)
                                            .Add(Dialogs.InstallDir)
                                            .Add(Dialogs.Progress)
                                            .Add(Dialogs.Exit);

            project.ManagedUI.ModifyDialogs.Add(Dialogs.MaintenanceType)
                                           .Add(Dialogs.Features)
                                           .Add(Dialogs.Progress)
                                           .Add(Dialogs.Exit);

            project.PreserveTempFiles = false;
            var msi = project.BuildMsi();
        }
    }
}