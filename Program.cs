using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ConsoleApp1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string[]? newArgs;
            if (args.Length > 0)
            {
                newArgs = args;
            }
            else
            {
                Console.WriteLine("Expecting input.");
                newArgs = Console.ReadLine()?.Split(" ");
            }

            if (newArgs == null || newArgs.Length < 1)
            {
                Console.WriteLine("Invalid args");
                Console.WriteLine("Press any key to close this window . . .");
                Console.ReadKey();
                return;
            }
            ConversionSettings settings = GetSettings(newArgs, out bool quickExit);
            if (settings != null)
            {
                if (settings.outputPath == "")
                {
                    string assemblyPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                    settings.outputPath = Path.Combine(assemblyPath, Path.GetFileNameWithoutExtension(settings.inputPath));
                    settings.outputPath += ".csv";
                }

                if (CsvUtility.WriteToCsv(settings.outputPath, settings.inputPath, settings.separator, settings.encoding))
                {
                    Console.WriteLine($"Conversion is done. Output file: {settings.outputPath}");
                }
            }
            if (!quickExit)
            {
                Console.WriteLine("Press any key to close this window . . .");
                Console.ReadKey();
            }
        }

        private static ConversionSettings GetSettings(string[] commands, out bool quickExit)
        {
            quickExit = false;
            ConversionSettings settings = new();
            for (int i = 0; i < commands.Length; i++)
            {
                switch (commands[i])
                {
                    case "-q":
                        quickExit = true;
                        break;
                    case "-i":
                        if (i + 1 >= commands.Length)
                        {
                            Console.WriteLine("No input path is declared, but not defined.");
                            return null;
                        }
                        settings.inputPath = Path.GetFullPath(commands[i + 1]);
                        i++;
                        break;
                    case "-o":
                        if (i + 1 >= commands.Length)
                        {
                            Console.WriteLine("Output path is declared, but not defined.");
                            return null;
                        }
                        settings.outputPath = Path.GetFullPath(commands[i + 1]);
                        i++;
                        break;
                    case "-s":
                        if (i + 1 >= commands.Length)
                        {
                            Console.WriteLine("Separator is declared, but not defined.");
                            return null;
                        }
                        settings.separator = commands[i + 1];
                        i++;
                        break;

                    case "-e":
                        if (i + 1 >= commands.Length)
                        {
                            Console.WriteLine("Encoding is declared, but not defined.");
                            return null;
                        }
                        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                        try
                        {
                            settings.encoding = Encoding.GetEncoding(Convert.ToInt32(commands[i + 1]));
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Invalid encoding name: " + ex.Message);
                        }
                        i++;
                        break;
                    default:
                        break;
                }
            }
            return settings;
        }
    }
}