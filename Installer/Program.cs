using System;
using System.Windows.Forms;
using WixSharp;
using WixSharp.Forms;

namespace Installer
{
    internal class Program
    {
        static void Main()
        {
            // var project = new ManagedProject("MyProduct",
            //                  new Dir(@"%ProgramFiles%\My Company\My Product",
            //                      new File("Program.cs")));

            var root = @"C:\Users\user\source\repos\InstallTest";
            var sln = @"C:\Users\user\source\repos\InstallTest\Installer";

            var project = new ManagedProject(
                "MyApp",
                new Dir(
                    $@"{root}\Ian\MyApp",
                    new File($@"{sln}\Files\Docs\readme.md"),
                    new File($@"{sln}\Files\Bin\MyApp.exe"),
                    new File($@"{sln}\Files\Bin\MyApp.dll"),
                    new File($@"{sln}\Files\Bin\MyApp.pdb"),
                    new File($@"{sln}\Files\Bin\MyApp.runtimeconfig.json")
                ),
                new IniFile(@"config.ini", $@"INSTALLDIR", IniFileAction.createLine, "user", "text", "default")
            );

            project.GUID = new Guid("6fe30b47-2577-43ad-9095-1861ba25889b");

            project.ManagedUI = ManagedUI.Empty;    //no standard UI dialogs
            project.ManagedUI = ManagedUI.Default;  //all standard UI dialogs

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
            project.AfterInstall += Msi_AfterInstall;

            //project.SourceBaseDir = "<input dir path>";
            //project.OutDir = "<output dir path>";

            project.BuildMsi();
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
    }
}