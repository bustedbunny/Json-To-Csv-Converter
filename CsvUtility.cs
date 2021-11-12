using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class CsvUtility
    {
        public static bool WriteToCsv(string outputPath, string inputPath, string separator, Encoding encoding)
        {
            StringBuilder sbOutput = new();

            var data = JsonUtility.ReadJson(inputPath);
            if (data == null)
            {
                Console.WriteLine("Json file is not valid.");
                return false;
            }
            if (data == null || data[0] == null)
            {
                Console.WriteLine("Data or data list is null.");
                return false;
            }

            for (int i = 0; i < data.Count; i++)
            {
                if (i == 0)
                {
                    sbOutput.AppendLine(string.Join(separator, data[i].Keys));
                }
                sbOutput.AppendLine(string.Join(separator, data[i].Values));
            }
            try
            {
                File.WriteAllText(outputPath, sbOutput.ToString(), encoding);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to write to file: " + ex.Message);
                return false;
            }
        }
    }
}
