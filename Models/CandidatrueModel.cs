using System.ComponentModel.DataAnnotations;

namespace dot_net.Models
{
    public class CandidatureModel
    {
        [Required]
        public string candidature { get; set; }
    }
}