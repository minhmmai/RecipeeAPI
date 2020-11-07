using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RecipeeAPI.DTOs.FbAuth
{
    public class UserTokenDTO
    {
        [JsonPropertyName("access_token")]
        public string Token { get; set; }
        [JsonPropertyName("token_type")]
        public string Type { get; set; }
        [JsonPropertyName("expires_in")]
        public long Expires { get; set; }
    }
}
