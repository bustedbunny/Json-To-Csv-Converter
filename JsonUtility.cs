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
        public static JsonObjects ReadJson(string path)
        {
            StreamReader jsonFileReader;
            try
            {
                jsonFileReader = File.OpenText(path);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Couldn't read json file: " + ex.Message);
                return null;
            }
            JsonObjects output = new()
            {
                List = JsonSerializer.Deserialize<JsonObject[]>(jsonFileReader.ReadToEnd(),
                                new JsonSerializerOptions
                                {
                                    PropertyNameCaseInsensitive = true
                                })
            };
            return output;
        }
    }
}
