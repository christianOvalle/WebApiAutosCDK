using System.ComponentModel.DataAnnotations;

namespace WebApiAutosCDK.DTOs
{
    public class EditarAdminDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
