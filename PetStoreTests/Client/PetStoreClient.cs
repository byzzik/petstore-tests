namespace PetStoreTests.Client
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;

    using Configuration;

    using Microsoft.Extensions.Options;

    using Models;

    public class PetStoreClient : IPetStoreClient
    {
        #region Fields

        private readonly string _apiKey;
        private readonly string _apiVersion;
        private readonly string _baseUrl;
        private readonly HttpClient _client;
        private readonly PetStoreClientConfiguration _clientConfiguration;
        private readonly string _petRoute;
        private readonly string _userName;
        private readonly string _userPassword;

        #endregion

        #region Constructors

        public PetStoreClient(IOptions<PetStoreClientConfiguration> configuration)
        {
            _clientConfiguration = configuration.Value;
            _baseUrl = _clientConfiguration.BaseUrl;
            _apiVersion = _clientConfiguration.ApiVersion;
            _petRoute = _clientConfiguration.PetRoute;
            _apiKey = _clientConfiguration.ApiKey;
            _userName = _clientConfiguration.UserName;
            _userPassword = _clientConfiguration.UserPassword;

            _client = new HttpClient
                      {
                          BaseAddress = new Uri(_baseUrl)
                      };
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.Add("api_key", $"{_apiKey}");
        }

        #endregion

        #region IPetStoreClient

        public async Task<Pet> AddPet(Pet newPet)
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync($"{_apiVersion}/{_petRoute}", newPet);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<Pet>();
        }

        public async Task<List<Pet>> GetPet(PetStatus status)
        {
            HttpResponseMessage response = await _client.GetAsync($"{_apiVersion}/{_petRoute}/findByStatus?status={status}");
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsAsync<List<Pet>>();
            return null;
        }

        public async Task<List<Pet>> GetPet(ulong? id)
        {
            HttpResponseMessage response = await _client.GetAsync($"{_apiVersion}/{_petRoute}/{id}");
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsAsync<List<Pet>>();
            return null;
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
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsAsync<ApiResponse>();
            return null;
        }

        public async Task<Pet> UpdatePet(Pet updatedPet)
        {
            HttpResponseMessage response = await _client.PutAsJsonAsync($"{_apiVersion}/{_petRoute}", updatedPet);
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsAsync<Pet>();
            return null;
        }

        public async Task<ApiResponse> DeletePet(ulong? id)
        {
            HttpResponseMessage response = await _client.DeleteAsync($"{_apiVersion}/{_petRoute}/{id}");
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsAsync<ApiResponse>();
            return null;
        }

        #endregion
    }
}
