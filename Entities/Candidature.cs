using System.Text.Json.Serialization;

namespace dot_net.Entities
{
    public class Candidature
    {
        public int Id { get; set; }
        public string JsonContent { get; set; }
        public int Archived {get; set;}
    }
}