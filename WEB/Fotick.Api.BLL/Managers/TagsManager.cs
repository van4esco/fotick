using System;
using System.Collections.Generic;
using System.Text;
using Fotick.Api.DAL.Repositories;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using Fotick.Api.BLL.Extensions;
using Fotick.Api.BLL.Models;
using System.Threading.Tasks;
using Fotick.Api.DAL.Entities;
using Newtonsoft.Json;
namespace Fotick.Api.BLL.Managers
{
    public class TagsManager
    {
        private readonly IConfiguration _configuration;
        private readonly IImageRepository _imageRepository;
        private readonly ITagRepository _tagRepository;
        private string _accessToken;
        private HttpClient _client;
        public TagsManager(IConfiguration configuration, IImageRepository imageRepository, ITagRepository tagRepository)
        {
            _configuration = configuration;
            _imageRepository = imageRepository;
            _tagRepository = tagRepository;
            InitializeClient();
        }

        private async void InitializeClient()
        {
            _client = new HttpClient() {
                BaseAddress = new Uri(_configuration["Clarifai:base_url"])
            };
            var content = new FormUrlEncodedContent(new List<KeyValuePair<string,string>>(){
                new KeyValuePair<string,string>("client_id",_configuration["Clarifai:client_id"]),
                new KeyValuePair<string,string>("client_secret",_configuration["Clarifai:client_secret"]),
                new KeyValuePair<string,string>("grant_type",_configuration["Clarifai:grant_type"]),
            });
            var msg = await _client.PostAsync(_configuration["Clarifai:token_url"], content);
            _accessToken = msg.ToDictionary()["access_token"];
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _accessToken);
            _client.PostAsync("", new StringContent(JsonConvert.SerializeObject(), Encoding.UTF8, "application/json"));
        }

        public async Task<IEnumerable<Tag>> GetTags(string url){
            var obj = JsonConvert.SerializeObject(new TagManagerModel
            {
                Inputs = new List<Input>(){
                    new Input{
                        Data = new Data{
                            Image = new Image{
                                Url = url
                            }
                        }
                    }
                }
            });
            var url = _configuration["Clarifai:url"];
            var model = _configuration["Clarifai:model"];
            url.Replace("{0}", model);
            var msg = await _client.PostAsync(model, new StringContent(obj, Encoding.UTF8, "application/json"));
            var json = await msg.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<dynamic>(json).outputs[0].data.concepts;
            var list = new List<Tag>();
            foreach (var item in result)
            {
                var tag = _tagRepository.GetByText(item.name);
                if(tag == null){
                    tag = new Tag
                    {
                        Text = item.name
                    };
                    _tagRepository.Add(tag);
                }
                list.Add(tag);
            }
            return list;
        }
    }
}
