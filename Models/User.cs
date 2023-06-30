using System.ComponentModel.DataAnnotations;

namespace GraphQLClientAPI.Models
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
    }
}
