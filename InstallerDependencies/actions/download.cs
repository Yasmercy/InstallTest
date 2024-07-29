using System;
using System.IO;
using WixToolset.Dtf.WindowsInstaller;

namespace InstallerDependencies.CustomActions
{
    public static class DownloadActions
    {
        [CustomAction]
        public static ActionResult Download(Session session)
        {
            CustomActionData data = session.CustomActionData;
            // var installDir = session.GetProductProperty("INSTALLDIR");
            // var success = Download(data["Src"], $"{installDir}{data["Dest"]}", data["Opts"]);
            
            return ActionResult.Success;
            // if (success)
            //     return ActionResult.Success;
            // else 
            //     return ActionResult.Failure;
        }

        private static bool Download(string Src, string Dest, string Opts)
        {
            switch (Opts)
            {
                case "/u":
                    {
                        if (File.Exists(Dest))
                        {
                            Try(File.Delete, Dest);
                        }
                        return true;
                    }
                default:
                    {
                        if (!File.Exists(Src))
                        {
                            return false;
                        }
                        else if (File.Exists(Dest))
                        {
                            return true;
                        }
                        return Try(File.Copy, Src, Dest);
                    }
            }
        }

        private static bool Try(Action<string> action, string param)
        {
            // maybe do something to the Session on error
            try { action(param); return true; }
            catch { return false; }
        }

        private static bool Try(Action<string, string> action, string param1, string param2)
        {
            // maybe do something to the Session on error
            try { action(param1, param2); return true; }
            catch { return false; }
        }
    }
}