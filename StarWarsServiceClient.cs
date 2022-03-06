using ApiSW.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApiSW
{
    public class StarWarsServiceClient
    {
        private readonly HttpClient _httpClient;
        private JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings()
        {
            ContractResolver = new DefaultContractResolver()
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            }
        };

        public StarWarsServiceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<object> GetStarshipInfoAsync(int id)
        {
            var url = $"starships/{id}";
            var response = await _httpClient.GetAsync(url);
            
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var deserializedObject = JsonConvert.DeserializeObject<StarShipModel>(responseBody, jsonSerializerSettings);
                
                if (deserializedObject.Films.Length!=0)
                {
                    List<Movie> movies = new List<Movie>();
                    foreach (var item in deserializedObject.Films)
                    {
                        var mov = GetMovieInfoAsync(item);
                        if (mov.Result != null)
                        {
                            movies.Add(mov.Result);
                        }
                    }
                    return movies;
                }
            }

            return null;
        }
        private async Task<Movie> GetMovieInfoAsync(string MovieUrl)
        {
            var url = $"films/{MovieUrl.Replace("https://swapi.dev/api/films/", "")}";//фигня какая то
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var deserializedObject = JsonConvert.DeserializeObject<Movie>(responseBody, jsonSerializerSettings);
                return deserializedObject;
            }
            return null;
        }
    }
}
