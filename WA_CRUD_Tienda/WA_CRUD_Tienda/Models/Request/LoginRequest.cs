using System.ComponentModel.DataAnnotations;

namespace WA_CRUD_Tienda.Models.Request
{
    public class LoginRequest
    {
        [Required]
        public string email { get; set; }
        
        [Required]
        public string password { get; set; } 
    }
}
