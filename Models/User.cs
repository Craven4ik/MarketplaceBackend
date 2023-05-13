using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MarketPlace.Models
{
    public class User
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        //[JsonIgnore]
        public string Password { get; set; }

        public string? Name { get; set; }
    }
}
