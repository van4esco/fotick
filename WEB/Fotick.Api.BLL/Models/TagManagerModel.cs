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

    public class Concept{
        public string name { get; set; }
        public string value { get; set; }
        public string id { get; set; }
        public string app_id { get; set; }
    }
    public class Data_2
    {
        public IEnumerable<Concept> concepts { get; set; }
    }

    public class Output{
        public Data_2 data { get; set; }
    }

    public class Model{
        public List<Output> outputs { get; set; }
    }
}
