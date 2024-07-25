using System;
using System.Windows.Forms;
using WixSharp;
using WixSharp.CommonTasks;
using WixSharp.Forms;

namespace Installer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var root = @"C:\Users\user\source\repos\InstallTest";
            var sln = $@"{root}\Installer";

            var project = new ManagedProject(
                "MyApp",
                new Dir(
                    "INSTALLDIR",
                    new File($@"{sln}\Files\Docs\readme.md"),
                    new File($@"{sln}\Files\Bin\MyApp.exe"),
                    new File($@"{sln}\Files\Bin\MyApp.dll"),
                    new File($@"{sln}\Files\Bin\MyApp.pdb"),
                    new File($@"{sln}\Files\Bin\MyApp.runtimeconfig.json"),
                    new File($@"{sln}\Files\Bin\nlog.config"),
                    new Dir(@"deps", new Files($@"{sln}\Files\Bin\deps\*.*"))
                ),
                new IniFile("data.ini", "INSTALLDIR", IniFileAction.createLine, "user", "text", "default")
            );
            project.Media.Clear();
            project.AddXmlElement("Wix/Package", "MediaTemplate", "CompressionLevel=high; EmbedCab=yes");

            project.GUID = new Guid("6fe30b47-2577-43ad-9095-1861ba25889b");
            project.Version = new Version("1.1.0");

            project.MajorUpgradeStrategy = MajorUpgradeStrategy.Default;
            project.MajorUpgradeStrategy.RemoveExistingProductAfter = Step.InstallInitialize;
            project.MajorUpgradeStrategy.PreventDowngradingVersions.OnlyDetect = false;
            project.MajorUpgradeStrategy.PreventDowngradingVersions.MigrateFeatures = true;

            //custom set of standard UI dialogs
            project.ManagedUI = new ManagedUI();

            project.ManagedUI.InstallDialogs.Add(Dialogs.Welcome)
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

            project.Load += Msi_Load;
            project.BeforeInstall += Msi_BeforeInstall;
            project.BeforeInstall += project_BeforeInstall;
            project.AfterInstall += Msi_AfterInstall;

            // project.SourceBaseDir = "<input dir path>";
            // project.OutDir = "<output dir path>";

            project.PreserveTempFiles = false;
            var msi = project.BuildMsi();
        }

        static void Msi_Load(SetupEventArgs e)
        {
            if (!e.IsUISupressed && !e.IsUninstalling)
                MessageBox.Show(e.ToString(), "Load");
        }

        static void Msi_BeforeInstall(SetupEventArgs e)
        {
            if (!e.IsUISupressed && !e.IsUninstalling)
                MessageBox.Show(e.ToString(), "BeforeInstall");
        }

        static void Msi_AfterInstall(SetupEventArgs e)
        {
            if (!e.IsUISupressed && !e.IsUninstalling)
                MessageBox.Show(e.ToString(), "AfterExecute");
        }

        static void project_BeforeInstall(SetupEventArgs e)
        {
            MessageBox.Show(e.ToString(), "BeforeInstall " + AppSearch.GetProductVersionFromUpgradeCode(e.UpgradeCode));
        }
    }
}