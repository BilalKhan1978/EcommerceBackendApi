using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EcommerceBackendApi.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        [JsonIgnore]
        public byte[] PassHash { get; set; }
        [JsonIgnore]
        public byte[] PassSalt { get; set; }
        public int UniqueStoreId { get; set; }    //fk
    }

    public class Jwt
    {
        public string? key { get; set; }
        public string? Issuer { get; set; }
        public string? Audience { get; set; }
        public string? Subject { get; set; }
    }
}
