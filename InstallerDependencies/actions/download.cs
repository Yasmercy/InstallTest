using System.IO;

class Script
{
    static void main(string[] args)
    {
        if (args.Length < 2)
            return;

        if (args[0] == "/u")
        {
            if (args.Length > 2 && File.Exists(args[2]))
            {
                File.Delete(args[2]);
            }
        }
        else 
        {
            if (!File.Exists(args[1]))
            {
                File.Copy(args[0], args[1]);
            }
        }
    }
}