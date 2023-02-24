using System.Text.Json.Serialization;

namespace EcommerceBackendApi.ViewModels
{
    public class GetUserRequestDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public int UniqueStoreId { get; set; }
    }
}
