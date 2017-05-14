using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Extensions
{
    public static class StringExtensions
    {
        public static Dictionary<string, string> ToDictionary(this string str)
        {
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(str);
        }
    }
}
