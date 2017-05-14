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
using Fotick.Api.BLL.Contracts;
using Microsoft.Extensions.Logging;
using System.IO;

namespace Fotick.Api.BLL.Managers
{
    public class TagsManager: ITagsManager
    {
        private readonly IConfiguration _configuration;
        private readonly IImageRepository _imageRepository;
        private readonly ITagRepository _tagRepository;
        private string _accessToken;
        private readonly ILogger<TagsManager> _logger;
        private HttpClient _client;
        public TagsManager(IConfiguration configuration, IImageRepository imageRepository, ITagRepository tagRepository, ILogger<TagsManager> logger)
        {
            _configuration = configuration;
            _imageRepository = imageRepository;
            _tagRepository = tagRepository;
            _logger = logger;
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
            var result = await msg.Content.ReadAsStringAsync();
            _accessToken = result.ToDictionary()["access_token"];
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _accessToken);
        }

        public async Task<IEnumerable<Tag>> GetTags(string url){
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
            var curl = _configuration["Clarifai:url"];
            var model = _configuration["Clarifai:model"];
            curl = curl.Replace("{0}", model);
            var msg = await _client.PostAsync(curl, new StringContent(obj, Encoding.UTF8, "application/json"));
            var json = await msg.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Model>(json);
            var list = new List<Tag>();
            foreach (dynamic item in result.outputs[0].data.concepts)
            {
                var tag = _tagRepository.GetByText(item.name as string);
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
