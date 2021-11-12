using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal static class JsonUtility
    {
        public static List<Dictionary<string, string>> ReadJson(string path)
        {
            string jsonText;
            try
            {
                jsonText = File.ReadAllText(path);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Couldn't read json file: " + ex.Message);
                return null;
            }
            return JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(jsonText);
        }
    }
}
