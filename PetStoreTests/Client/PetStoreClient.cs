﻿namespace PetStoreTests.Client
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

        private readonly string _apiVersion;
        private string BaseUrl { get; }
        private readonly HttpClient _client;

        private readonly string _petRoute;
        private readonly string _userName;
        private readonly string _userPassword;

        #endregion

        #region Constructors

        public PetStoreClient(IOptions<PetStoreClientConfiguration> configuration)
        {
            var clientFactory = ServiceProviderConfigurator.CreateServiceProvider().GetRequiredService<IHttpClientFactory>();
            PetStoreClientConfiguration clientConfiguration = configuration.Value;
            BaseUrl = clientConfiguration.BaseUrl;
            _apiVersion = clientConfiguration.ApiVersion;
            _petRoute = clientConfiguration.PetRoute;
            string apiKey = clientConfiguration.ApiKey;
            _userName = clientConfiguration.UserName;
            _userPassword = clientConfiguration.UserPassword;

            _client = clientFactory.CreateClient();
            _client.BaseAddress = new Uri(BaseUrl);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.Add("api_key", $"{apiKey}");
        }

        #endregion

        #region IPetStoreClient

        public async Task<Pet> AddPet(Pet newPet)
        {
            var payload = new StringContent(JsonConvert.SerializeObject(newPet), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync($"{_apiVersion}/{_petRoute}", payload);
            return response.IsSuccessStatusCode ? JsonConvert.DeserializeObject<Pet>(response.Content.ReadAsStringAsync().Result) : null;
        }

        public async Task<List<Pet>> GetPetByStatus(PetStatus status)
        {
            HttpResponseMessage response = await _client.GetAsync($"{_apiVersion}/{_petRoute}/findByStatus?status={status}");
            return response.IsSuccessStatusCode ? JsonConvert.DeserializeObject<List<Pet>>(response.Content.ReadAsStringAsync().Result) : null;
        }

        public async Task<Pet> GetPetById(ulong? id)
        {
            HttpResponseMessage response = await _client.GetAsync($"{_apiVersion}/{_petRoute}/{id}");
            return response.IsSuccessStatusCode
                       ? JsonConvert.DeserializeObject<Pet>(response.Content.ReadAsStringAsync().Result)
                       : null;
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
