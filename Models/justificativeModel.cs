using System.ComponentModel.DataAnnotations;

namespace dot_net.Models
{
    public class JustificativeModel
    {
        [Required]
        public string fileName { get; set; }
    }
}