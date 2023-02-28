namespace EcommerceBackendApi.ViewModels
{
    public class LoginUserRequestDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public int UniqueStoreId { get; set; }    // fk
    }
}
