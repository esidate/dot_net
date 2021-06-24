using System.ComponentModel.DataAnnotations;

namespace dot_net.Models
{
    public class ModifyUserModel
    {

        [Required]
        public string username { get; set; }

        [Required]
        public string newUsername { get; set; }

        [Required]
        public string newPassword { get; set; }
    }
}