using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class JsonObjects
    {
        public JsonObject[]? List { get; set; }
    }

    public class JsonObject
    {
        public string? Id { get; set; }
        public string? Maker { get; set; }
        public string? img { get; set; }
        public string? Url { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int[]? Ratings { get; set; }
    }
}
