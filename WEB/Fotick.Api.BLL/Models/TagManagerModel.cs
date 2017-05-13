using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
namespace Fotick.Api.BLL.Models
{
    public class TagManagerModel
    {
        [JsonProperty("inputs")]
        public IEnumerable<Input> Inputs { get; set; }
    }

    public class Input{
        [JsonProperty("data")]
        public Data Data { get; set; }
    }

    public class Data
    {
        [JsonProperty("image")]
        public Image Image { get; set; }
    }

    public class Image{
        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
