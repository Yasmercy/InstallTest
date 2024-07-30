using System.Diagnostics;
using System.IO;
using System.Linq;

class Script
{
    static void main(string[] args)
    {
        var opts = args[0];
        var destDir = args[1];
        // var srcDir = @"ADD DIRECTORY TO \\Public\...\deps\ HERE"

		// opts destFolder [*.dll]
		if (args.Length < 3)
            return;

        if (args[0] == "/u")
        {
            foreach (var file in args.Skip(2))
            {
                if (File.Exists($"{destDir}{file}"))
                {
                    File.Delete($"{destDir}{file}");
                }
            }
        }
        else if (args[0] == "/i")
        {
            Process.Start("net", $"use {srcDir}");
            foreach (var file in args.Skip(2))
            {
                if (File.Exists($"{srcDir}{file}") && !File.Exists($"{destDir}{file}"))
                {
                    File.Copy($"{srcDir}{file}", $"{destDir}{file}");
                }
            }
            Process.Start("net", $"use {srcDir} /delete");
        }
    }
}