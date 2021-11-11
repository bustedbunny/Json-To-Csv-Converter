using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ConsoleApp1
{
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("Expecting input.");
            string? args = Console.ReadLine();
            if (args == null || args.Length < 1)
            {
                Console.WriteLine("Invalid args");
                return;
            }
            ConversionSettings settings = Program.GetSettings(args, out bool quickExit);
            if (settings != null)
            {
                if (settings.outputPath == "")
                {
                    string assemblyPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                    settings.outputPath = Path.Combine(assemblyPath, Path.GetFileNameWithoutExtension(settings.inputPath));
                    settings.outputPath += ".csv";
                }

                if (CsvUtility.WriteToCsv(settings.outputPath, settings.data, settings.separator, settings.encoding))
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

        private static ConversionSettings GetSettings(string? args, out bool quickExit)
        {
            quickExit = false;
            if (args == null)
            {
                Console.WriteLine("Input is null");
                return null;
            }
            List<string> commands = args.Split(" ").ToList();
            ConversionSettings settings = new();
            for (int i = 0; i < commands.Count; i++)
            {
                switch (commands[i])
                {
                    case "-q":
                        quickExit = true;
                        break;
                    case "-i":
                        if (i + 1 >= commands.Count)
                        {
                            Console.WriteLine("No input path is declared, but not defined.");
                            return null;
                        }
                        settings.inputPath = Path.GetFullPath(commands[i + 1]);
                        settings.data = JsonUtility.ReadJson(settings.inputPath);
                        if (settings.data == null || settings.data.List == null)
                        {
                            Console.WriteLine("Json file is not valid.");
                            return null;
                        }
                        i++;
                        break;
                    case "-o":
                        if (i + 1 >= commands.Count)
                        {
                            Console.WriteLine("Output path is declared, but not defined.");
                            return null;
                        }
                        settings.outputPath = Path.GetFullPath(commands[i + 1]);
                        i++;
                        break;
                    case "-s":
                        if (i + 1 >= commands.Count)
                        {
                            Console.WriteLine("Separator is declared, but not defined.");
                            return null;
                        }
                        settings.separator = commands[i + 1];
                        i++;
                        break;

                    case "-e":
                        if (i + 1 >= commands.Count)
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