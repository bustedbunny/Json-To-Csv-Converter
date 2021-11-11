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
        public static bool WriteToCsv(string path, JsonObjects data, string separator, Encoding encoding)
        {
            StringBuilder sbOutput = new();
            if (data == null || data.List == null || data.List[0] == null)
            {
                Console.WriteLine("Data or data list is null.");
                return false;
            }
            Type objType = data.List[0].GetType();
            PropertyInfo[] fields = objType.GetProperties();
            string[] fieldNames = new string[fields.Length];
            for (int i = 0; i < fields.Length; i++)
            {
                fieldNames[i] = fields[i].Name;
            }
            sbOutput.AppendLine(string.Join(separator, fieldNames));
            for (int i = 0; i < data.List.Length; i++)
            {
                string?[] newLine = new string[fieldNames.Length];
                for (int j = 0; j < fields.Length; j++)
                {
                    newLine[j] = objType.GetProperty(fieldNames[j])?.GetValue(data.List[i])?.ToString();
                }
                sbOutput.AppendLine(string.Join(separator, newLine));
            }
            File.WriteAllText(path, sbOutput.ToString(), encoding);
            return true;
        }
    }
}
