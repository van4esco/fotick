using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class TagManagerModel
    {
        [JsonProperty("inputs")]
        public IEnumerable<Input> Inputs { get; set; }
    }

    public class Input
    {
        [JsonProperty("data")]
        public Data Data { get; set; }
    }

    public class Data
    {
        [JsonProperty("image")]
        public Image Image { get; set; }
    }

    public class Image
    {
        [JsonProperty("url")]
        public string Url { get; set; }
    }

    public class Concept
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("value")]
        public string value { get; set; }
        [JsonProperty("id")]
        public string id { get; set; }
        [JsonProperty("app_id")]
        public string app_id { get; set; }
    }
    public class DataResponse
    {
        [JsonProperty("concepts")]
        public IEnumerable<Concept> Concepts { get; set; }
    }

    public class Output
    {
        [JsonProperty("data")]
        public DataResponse Data { get; set; }
    }

    public class Model
    {
        [JsonProperty("outputs")]
        public List<Output> Outputs { get; set; }
    }
}
