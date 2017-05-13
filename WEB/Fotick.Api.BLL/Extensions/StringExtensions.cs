using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Fotick.Api.BLL.Extensions
{
    public static class StringExtensions
    {
        public static Dictionary<string,string> ToDictionary(this string str){
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(str);
        }
    }
}
