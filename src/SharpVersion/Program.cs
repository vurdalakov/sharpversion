namespace Vurdalakov.SharpVersion
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Reflection;

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if (0 == args.Length)
                {
                    Help();
                }

                var printFlags = 0;
                var fileName = "";

                foreach (var arg in args)
                {
                    if (('-' == arg[0]) || ('/' == arg[0]))
                    {
                        if ((printFlags > 0) || (arg.Length != 2))
                        {
                            Help();
                        }

                        switch (Char.ToLower(arg[1]))
                        {
                            case 'f':
                                printFlags = 1;
                                break;
                            case 'a':
                                printFlags = 2;
                                break;
                            case 'v':
                                printFlags = 4;
                                break;
                            case 'p':
                                printFlags = 7;
                                break;
                            default:
                                Help();
                                break;
                        }
                    }
                    else
                    {
                        if (!String.IsNullOrEmpty(fileName))
                        {
                            Help();
                        }

                        fileName = arg;
                    }
                }

                if (String.IsNullOrEmpty(fileName))
                {
                    Help();
                }

                if (Directory.Exists(fileName))
                {
                    ProcessDirectory(fileName);
                }
                else
                {
                    ProcessFile(fileName, printFlags);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR:\r\n{ex.Message}");
            }
        }

        private static void ProcessFile(String fileName, Int32 printFlags)
        {
            var fileInfo = new FileInfo(fileName);

            if (0 == printFlags)
            {
                PrintSummary(fileInfo.FullName);
                return;
            }

            if (1 == (printFlags & 1))
            {
                PrintFileInfo(fileInfo);
            }

            if (2 == (printFlags & 2))
            {
                PrintAssemblyName(fileInfo.FullName);
            }

            if (4 == (printFlags & 4))
            {
                PrintVersionInfo(fileInfo.FullName);
            }
        }

        private static Boolean TryGetAssemblyName(String filePath, out AssemblyName assemblyName)
        {
            try
            {
                assemblyName = AssemblyName.GetAssemblyName(filePath);
                return true;
            }
            catch (BadImageFormatException)
            {
                assemblyName = null;
                return false; // native DLL or other non-managed file
            }
        }

        private static void PrintSummary(String filePath)
        {
            var assemblyVersion = TryGetAssemblyName(filePath, out var assemblyName) ? $"'{assemblyName.Version}'" : "(not a .NET assembly)";
            Console.WriteLine($"Assembly version:         {assemblyVersion}");

            var versionInfo = FileVersionInfo.GetVersionInfo(filePath);
            Console.WriteLine($"File version:             '{versionInfo.FileVersion}'");
            Console.WriteLine($"Product version:          '{versionInfo.ProductVersion}'");

            Console.WriteLine("--- Run 'sver -h' for more options");
        }

        private static void PrintFileInfo(FileInfo fileInfo)
        {
            Console.WriteLine("--- File info:");

            Console.WriteLine($"File name:                '{fileInfo.Name}'");
            Console.WriteLine($"File path:                '{fileInfo.FullName}'");
            Console.WriteLine($"File size:                {fileInfo.Length:N0} bytes");
            Console.WriteLine($"Created:                  {fileInfo.CreationTime:yyyy-MM-ddTHH-mm-ss-ffff}");
            Console.WriteLine($"Modified:                 {fileInfo.LastWriteTime:yyyy-MM-ddTHH-mm-ss-ffff}");
            Console.WriteLine($"Accessed:                 {fileInfo.LastAccessTime:yyyy-MM-ddTHH-mm-ss-ffff}");
            Console.WriteLine($"Attributes:               {fileInfo.Attributes}");
        }

        private static void PrintAssemblyName(String filePath)
        {
            Console.WriteLine("--- Assembly name:");

            if (!TryGetAssemblyName(filePath, out var assemblyName))
            {
                Console.WriteLine("Not a .NET assembly");
                return;
            }

            Console.WriteLine($"Name:                    '{assemblyName.Name}'");
            Console.WriteLine($"Version:                  '{assemblyName.Version}'");
            Console.WriteLine($"Version compatibility:    '{assemblyName.VersionCompatibility}'");
            Console.WriteLine($"Culture:                  '{(String.IsNullOrEmpty(assemblyName.CultureName) ? "neutral" : assemblyName.CultureName)}'");
            Console.WriteLine($"Hash algorithm:           '{assemblyName.HashAlgorithm}'");
            Console.WriteLine($"Processor architecture:   '{assemblyName.ProcessorArchitecture}'");
            Console.WriteLine($"Public key token:         '{assemblyName.GetPublicKeyToken().ToHexString()}'");
            Console.WriteLine($"Flags:                    '{assemblyName.Flags}'");
            Console.WriteLine($"Content type:             '{assemblyName.ContentType}'");
            //Console.WriteLine($"Public key:               '{assemblyName.GetPublicKey().ToHexString()}'");
            Console.WriteLine($"Full name:                '{assemblyName.FullName}'");
        }

        private static void PrintVersionInfo(String filePath)
        {
            Console.WriteLine("--- Version info:");

            var versionInfo = FileVersionInfo.GetVersionInfo(filePath);

            Console.WriteLine($"File description:         '{versionInfo.FileDescription}'");
            Console.WriteLine($"File version:             '{versionInfo.FileVersion}'");
            Console.WriteLine($"Product name:             '{versionInfo.ProductName}'");
            Console.WriteLine($"Product version:          '{versionInfo.ProductVersion}'");
            Console.WriteLine($"Copyright:                '{versionInfo.LegalCopyright}'");
            Console.WriteLine($"Language:                 '{versionInfo.Language}'");
            Console.WriteLine($"Original filename:        '{versionInfo.OriginalFilename}'");
            Console.WriteLine($"Internal name:            '{versionInfo.InternalName}'");
            Console.WriteLine($"Private build:            '{versionInfo.PrivateBuild}'");
            Console.WriteLine($"Special build:            '{versionInfo.SpecialBuild}'");
            Console.WriteLine($"Company name:             '{versionInfo.CompanyName}'");
            Console.WriteLine($"Trademarks:               '{versionInfo.LegalTrademarks}'");
            Console.WriteLine($"Private build:            '{versionInfo.PrivateBuild}'");
            Console.WriteLine($"Comments:                 '{versionInfo.Comments}'");
        }

        private static void ProcessDirectory(String directoryPath)
        {
            var directoryInfo = new DirectoryInfo(directoryPath);

            Console.WriteLine("--- Directory info:");

            Console.WriteLine($"Directory name:  '{directoryInfo.Name}'");
            Console.WriteLine($"Directory path:  '{directoryInfo.FullName}'");
            Console.WriteLine($"Created:         {directoryInfo.CreationTime:yyyy-MM-ddTHH-mm-ss-ffff}");
            Console.WriteLine($"Modified:        {directoryInfo.LastWriteTime:yyyy-MM-ddTHH-mm-ss-ffff}");
            Console.WriteLine($"Accessed:        {directoryInfo.LastAccessTime:yyyy-MM-ddTHH-mm-ss-ffff}");
            Console.WriteLine($"Attributes:      {directoryInfo.Attributes}");
        }

        private static void Help()
        {
            Console.WriteLine("SharpVerion 1.01");
            Console.WriteLine("A command-line utility that shows .NET assembly information");
            Console.WriteLine("https://github.com/vurdalakov/sharpversion");
            Console.WriteLine("Usage:\n\tsver [-f|-a|-v|-p] <file name|directory name>");
            Console.WriteLine("-f - print all file info");
            Console.WriteLine("-a - print all assembly info");
            Console.WriteLine("-v - print all version info");
            Console.WriteLine("-p - print it all");
            Environment.Exit(1);
        }
    }

    public static class Extensions
    {
        public static String ToHexString(this Byte[] array) => BitConverter.ToString(array).Replace("-", "").ToLower();
    }
}
