using EcommerceBackendApi.Models;

namespace EcommerceBackendApi.ViewModels
{
    public class AddUserRequestDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public int UniqueStoreId { get; set; }    //fk
    }
}
