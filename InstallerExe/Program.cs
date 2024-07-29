using InstallerExe.Properties;
using System;
using System.Diagnostics;
using System.IO;

public static class Launcher
{
    static public int Main(string[] args)
    {
        var msi = Path.GetTempFileName();
        try
        {
            File.WriteAllBytes(msi, Resources.MyApp);

            // string msi_args = args.Any() ? string.Join(" ", args) : "/i";
            var x = $"msiexec.exe {args[0]} {msi} --% {args[1]}";
            var p = Process.Start("msiexec.exe", $"{args[0]} {msi} {args[1]} ");
            p.WaitForExit();
            return p.ExitCode;
        }
        catch (Exception e)
        {
            // report the error
            return -1;
        }
        finally
        {
            try
            {
                if (File.Exists(msi))
                    File.Delete(msi);
            }
            catch { }
        }

        // return ResourceManager.GetString("String1", resourceCulture);
    }
}
