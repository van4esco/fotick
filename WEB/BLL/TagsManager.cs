using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using BLL.Extensions;
using DAL.Entities;
using Newtonsoft.Json;
using BLL.Models;
using DAL;

namespace BLL
{
    public class TagsManager:IDisposable
    {
        private string _accessToken;
        private HttpClient _client;
        public TagsManager()
        {
            InitializeClient();
        }

        private async void InitializeClient()
        {
            _client = new HttpClient()
            {
                BaseAddress = new Uri(ConfigurationManager.AppSettings["Clarifai:base_url"])
            };
            var content = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>(){
                new KeyValuePair<string,string>("client_id",ConfigurationManager.AppSettings["Clarifai:client_id"]),
                new KeyValuePair<string,string>("client_secret",ConfigurationManager.AppSettings["Clarifai:client_secret"]),
                new KeyValuePair<string,string>("grant_type",ConfigurationManager.AppSettings["Clarifai:grant_type"]),
            });
            var msgT = _client.PostAsync(ConfigurationManager.AppSettings["Clarifai:token_url"], content);
            msgT.Wait();
            var result = msgT.Result.Content.ReadAsStringAsync();
            result.Wait();
            _accessToken = result.Result.ToDictionary()["access_token"];
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _accessToken);
        }

        public async Task<IEnumerable<Tag>> GetTags(string url)
        {
            var obj = JsonConvert.SerializeObject(new TagManagerModel
            {
                Inputs = new List<Input>(){
                    new Input{
                        Data = new Data{
                            Image = new Models.Image{
                                Url = url
                            }
                        }
                    }
                }
            });
            var curl = ConfigurationManager.AppSettings["Clarifai:url"];
            var model = ConfigurationManager.AppSettings["Clarifai:model"];
            curl = curl.Replace("{0}", model);
            var msg = await _client.PostAsync(curl, new StringContent(obj, Encoding.UTF8, "application/json"));
            var json = await msg.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Model>(json);
            var list = new List<Tag>();
            using (var db = FontickDbContext.Create())
            {
                foreach (var item in result.Outputs[0].Data.Concepts)
                {
                    var tag = db.Tags.FirstOrDefault(p => p.Text == item.Name);
                    if (tag == null)
                    {
                        tag = new Tag
                        {
                            Text = item.Name
                        };
                        db.Tags.Add(tag);
                    }
                    list.Add(tag);
                }
                db.SaveChanges();
                return list;
            }
        }

        public void Dispose()
        {
            if (_client != null)
                _client.Dispose();
        }
    }
}
