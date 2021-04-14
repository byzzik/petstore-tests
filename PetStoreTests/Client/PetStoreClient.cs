namespace PetStoreTests.Client
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;

    using Configuration;

    using Infrastructure;

    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;

    using Models;

    using Newtonsoft.Json;

    public class PetStoreClient : IPetStoreClient
    {
        #region Fields

        private readonly string _apiKey;
        private readonly string _apiVersion;
        private readonly string _baseUrl;
        private readonly HttpClient _client;
        private readonly PetStoreClientConfiguration _clientConfiguration;

        private readonly IHttpClientFactory _clientFactory;
        private readonly string _petRoute;
        private readonly string _userName;
        private readonly string _userPassword;

        #endregion

        #region Constructors

        public PetStoreClient(IOptions<PetStoreClientConfiguration> configuration)
        {
            _clientFactory = ServiceProviderConfigurator.CreateServiceProvider().GetRequiredService<IHttpClientFactory>();
            _clientConfiguration = configuration.Value;
            _baseUrl = _clientConfiguration.BaseUrl;
            _apiVersion = _clientConfiguration.ApiVersion;
            _petRoute = _clientConfiguration.PetRoute;
            _apiKey = _clientConfiguration.ApiKey;
            _userName = _clientConfiguration.UserName;
            _userPassword = _clientConfiguration.UserPassword;

            _client = _clientFactory.CreateClient();
            _client.BaseAddress = new Uri(configuration.Value.BaseUrl);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.Add("api_key", $"{_apiKey}");
        }

        #endregion

        #region IPetStoreClient

        public async Task<Pet> AddPet(Pet newPet)
        {
            var payload = new StringContent(JsonConvert.SerializeObject(newPet), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync($"{_apiVersion}/{_petRoute}", payload);
            return response.IsSuccessStatusCode ? JsonConvert.DeserializeObject<Pet>(response.Content.ReadAsStringAsync().Result) : null;
        }

        public async Task<List<Pet>> GetPet(PetStatus status)
        {
            HttpResponseMessage response = await _client.GetAsync($"{_apiVersion}/{_petRoute}/findByStatus?status={status}");
            return response.IsSuccessStatusCode ? JsonConvert.DeserializeObject<List<Pet>>(response.Content.ReadAsStringAsync().Result) : null;
        }

        public async Task<List<Pet>> GetPet(ulong? id)
        {
            HttpResponseMessage response = await _client.GetAsync($"{_apiVersion}/{_petRoute}/{id}");
            return response.IsSuccessStatusCode ? JsonConvert.DeserializeObject<List<Pet>>(response.Content.ReadAsStringAsync().Result) : null;
        }

        public async Task<ApiResponse> UpdatePet(ulong? id, string name, PetStatus status)
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync(
                                               $"{_apiVersion}/{_petRoute}",
                                               new
                                               {
                                                   name,
                                                   status
                                               });
            return response.IsSuccessStatusCode ? JsonConvert.DeserializeObject<ApiResponse>(response.Content.ReadAsStringAsync().Result) : null;
        }

        public async Task<Pet> UpdatePet(Pet updatedPet)
        {
            var payload = new StringContent(JsonConvert.SerializeObject(updatedPet), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PutAsync($"{_apiVersion}/{_petRoute}", payload);
            return response.IsSuccessStatusCode ? JsonConvert.DeserializeObject<Pet>(response.Content.ReadAsStringAsync().Result) : null;
        }

        public async Task<ApiResponse> DeletePet(ulong? id)
        {
            HttpResponseMessage response = await _client.DeleteAsync($"{_apiVersion}/{_petRoute}/{id}");
            return response.IsSuccessStatusCode ? JsonConvert.DeserializeObject<ApiResponse>(response.Content.ReadAsStringAsync().Result) : null;
        }

        #endregion
    }
}
