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

        public async Task<StarshipWithMoviesModel> GetStarshipInfoAsync(int id)
        {
            var url = $"starships/{id}";
            var response = await _httpClient.GetAsync(url);
            
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var deserializedObject = JsonConvert.DeserializeObject<StarShipModel>(responseBody, jsonSerializerSettings);
                
                if (deserializedObject.Films.Length!=0)
                {
                    List<MovieModel> movies = new List<MovieModel>();
                    foreach (var item in deserializedObject.Films)
                    {
                        var mov = GetMovieInfoAsync(item.Replace("https://swapi.dev/api/films/", ""));
                        if (mov.Result != null)
                            movies.Add(mov.Result);
                    }
                    return new StarshipWithMoviesModel() { StarshipName = deserializedObject.Name, StarshipId = id, Movies = movies };
                }
            }
            return null;
        }
        private async Task<MovieModel> GetMovieInfoAsync(string id)
        {
            var url = $"films/{id}";
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var deserializedObject = JsonConvert.DeserializeObject<MovieModel>(responseBody, jsonSerializerSettings);
                return deserializedObject;
            }
            return null;
        }
    }
}
