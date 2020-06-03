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

                if (0 == printFlags)
                {
                    printFlags = 7;
                }

                if (String.IsNullOrEmpty(fileName))
                {
                    Help();
                }

                var fileInfo = new FileInfo(fileName);

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
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR:\r\n{ex.Message}");
            }
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
        }

        private static void PrintAssemblyName(String filePath)
        {
            var assemblyName = AssemblyName.GetAssemblyName(filePath);

            Console.WriteLine("--- Assembly name:");
            Console.WriteLine($"Name:                     '{assemblyName.Name}'");
            Console.WriteLine($"Version:                  '{assemblyName.Version}'");
            Console.WriteLine($"Culture:                  '{(String.IsNullOrEmpty(assemblyName.CultureName) ? "neutral" : assemblyName.CultureName)}'");
            Console.WriteLine($"Hash algorithm:           '{assemblyName.HashAlgorithm}'");
            Console.WriteLine($"Processor architecture:   '{assemblyName.ProcessorArchitecture}'");
            Console.WriteLine($"Public key token:         '{assemblyName.GetPublicKeyToken().ToHexString()}'");
            Console.WriteLine($"Flags:                    '{assemblyName.Flags}'");
            //Console.WriteLine($"Public key:               '{assemblyName.GetPublicKey().ToHexString()}'");
            Console.WriteLine($"Full name:                '{assemblyName.FullName}'");
        }

        private static void PrintVersionInfo(String filePath)
        {
            var versionInfo = FileVersionInfo.GetVersionInfo(filePath);

            Console.WriteLine("--- Version info:");
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

        private static void Help()
        {
            Console.WriteLine("Usage:r\n\tsver [-f|-a|-v] <filename>");
            Environment.Exit(1);
        }
    }

    public static class Extensions
    {
        public static String ToHexString(this Byte[] array) => BitConverter.ToString(array).Replace("-", "").ToLower();
    }
}
