using System.ComponentModel.DataAnnotations;

namespace dot_net.Models
{
    public class CandidatureModel
    {
        public int id { get; set; }

        [Required]
        public string candidature { get; set; }
        
        public int archived { get; set; }
    }
}