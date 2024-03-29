﻿using System.Text.Json.Serialization;

namespace EcommerceBackendApi.ViewModels
{
    public class GetStoreRequestDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        [JsonIgnore]
        public byte[] PassHash { get; set; }
        [JsonIgnore]
        public byte[] PassSalt { get; set; }
        public int UniqueStoreId { get; set; }
    }
}
