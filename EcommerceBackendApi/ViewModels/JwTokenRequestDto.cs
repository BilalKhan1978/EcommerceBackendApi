﻿namespace EcommerceBackendApi.ViewModels
{
    public class JwTokenRequestDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
