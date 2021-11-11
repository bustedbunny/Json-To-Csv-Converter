using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class ConversionSettings
    {
        public string inputPath = "";
        public string outputPath = "";
        public JsonObjects data = new();
        public string separator = ",";
        public Encoding encoding = Encoding.UTF8;
    }
}
