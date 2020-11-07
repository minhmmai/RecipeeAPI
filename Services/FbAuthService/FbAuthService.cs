using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using RecipeeAPI.Endpoints.Facebook;
using RecipeeAPI.DTOs.FbAuth;
using System.Text.Json;
using System.Text.Json.Serialization;
using RecipeeAPI.DTOs.Auth;
using AutoMapper.Configuration.Conventions;
using RecipeeAPI.Models;

namespace RecipeeAPI.Services.FbAuthService
{
    public class FbAuthService : IFbAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly string _clientId;
        private readonly string _clientSecret;
        public FbAuthService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _clientId = _configuration["AuthSettings:Facebook:ClientId"];
            _clientSecret = _configuration["AuthSettings:Facebook:ClientSecret"];

        }

        public async Task<FbUser> GetFbUserAsync(UserTokenDTO userToken)
        {
            var response = await _httpClient.GetAsync($"{FbEndpoints.Me}?fields=id,name,email&access_token={userToken.Token}");

            if (response.IsSuccessStatusCode)
            {
                var user = JsonSerializer.Deserialize<FbUser>(
                    await response.Content.ReadAsStringAsync(),
                    new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });
                return user;
            }
            else
            {
                return null;
            }
        }
    }
}
