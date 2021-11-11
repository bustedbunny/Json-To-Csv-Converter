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

            List<string> commands = args.Split(" ").ToList();
            bool quickExit = false;
            ConversionSettings settings = new();
            for (int i = 0; i < commands.Count; i++)
            {
                switch (commands[i])
                {
                    case "-i":
                        if (i + 1 >= commands.Count)
                        {
                            Console.WriteLine("No input path is declared, but not defined.");
                            return;
                        }
                        settings.inputPath = Path.GetFullPath(commands[i + 1]);
                        settings.data = JsonUtility.ReadJson(settings.inputPath);
                        if (settings.data == null || settings.data.List == null)
                        {
                            Console.WriteLine("Json file is not valid.");
                            return;
                        }
                        i++;
                        break;
                    case "-o":
                        if (i + 1 >= commands.Count)
                        {
                            Console.WriteLine("Output path is declared, but not defined.");
                            return;
                        }
                        settings.outputPath = Path.GetFullPath(commands[i + 1]);
                        i++;
                        break;
                    case "-s":
                        if (i + 1 >= commands.Count)
                        {
                            Console.WriteLine("Separator is declared, but not defined.");
                            return;
                        }
                        settings.separator = commands[i + 1];
                        i++;
                        break;

                    case "-e":
                        if (i + 1 >= commands.Count)
                        {
                            Console.WriteLine("Encoding is declared, but not defined.");
                            return;
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
                    case "-q":
                        quickExit = true;
                        break;
                    default:
                        break;

                }
            }
            if (settings.outputPath == "")
            {
                settings.outputPath = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), Path.GetFileNameWithoutExtension(settings.inputPath));
                settings.outputPath += ".csv";
            }

            if (CsvUtility.WriteToCsv(settings.outputPath, settings.data, settings.separator, settings.encoding))
            {
                Console.WriteLine($"Conversion is done. Output file: {settings.outputPath}");
            }

            if (!quickExit)
            {
                Console.WriteLine("Press any key to close this window . . .");
                Console.ReadKey();
            }
        }
    }
}