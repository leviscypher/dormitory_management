using System.ComponentModel.DataAnnotations;

namespace dormitory_management_be.Models
{
    public class AdminAccount
    {
        public int Id { get; set; }

        public string? Username { get; set; }
                     
        public string? Password { get; set; }
                     
        public string? Role { get; set; }
    }
}
