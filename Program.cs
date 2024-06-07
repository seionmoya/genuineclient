using System;
using System.IO;
using Microsoft.Win32;

namespace Seion.GenuineClient
{
    public class Program
    {
        private static void Main()
        {
            // create fake files
            Console.WriteLine("Creating fake files...");

            var rootDir = $@"{Environment.CurrentDirectory}\GenuineClient";
            CreateEmptyTextFile($@"{rootDir}\BattlEye\BEClient_x64.dll");
            CreateEmptyTextFile($@"{rootDir}\BattlEye\BEService_x64.exe");
            CreateEmptyTextFile($@"{rootDir}\ConsistencyInfo");
            CreateEmptyTextFile($@"{rootDir}\Uninstall.exe");
            CreateEmptyTextFile($@"{rootDir}\UnityCrashHandler64.exe");

            // set install path
            Console.WriteLine("Installing key in registry...");

            var success = SetRegistryKey(
                @"HKEY_LOCAL_MACHINE\Software\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall\EscapeFromTarkov",
                "InstallLocation",
                rootDir);

            if (success)
            {
                Console.WriteLine("Done.");
            }

            // completed
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private static void CreateEmptyTextFile(string filepath)
        {
            var path = Path.GetDirectoryName(filepath);

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            File.WriteAllText(filepath, string.Empty);
        }

        private static bool SetRegistryKey(string key, string subkey, string value)
        {
            try
            {
                Registry.SetValue(key, subkey, value);
                return true;
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Please restart with \"Run as admininistrator\".");
                return false;
            }
        }
    }
}
