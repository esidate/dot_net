using System;
namespace dot_net.Entities
{
    public class Candidature
    {
        public int Id { get; set; }
        public string RefrenceToken {get; set;}
        public string CandidateFirstName {get; set;}
        public string CandidateLastName {get; set;}
        public string JsonContent { get; set; }
        public string Note { get; set; }
        public string CreatedDate { get; set; }
    }
}
