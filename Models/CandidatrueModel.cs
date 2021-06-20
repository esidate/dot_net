using System.ComponentModel.DataAnnotations;

namespace dot_net.Models
{
    public class CandidatureModel
    {

        [Required]
        public string firstname { get; set; }
        [Required]
        public string lastname { get; set; }
        

    }
}