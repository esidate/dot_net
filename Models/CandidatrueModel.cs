using System.ComponentModel.DataAnnotations;
using System;
namespace dot_net.Models
{
    public class CandidatureModel
    {
        public int id { get; set; }
        public string candidature { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string refrenceToken { get; set; }
        public string createdDate { get; set; }
        public string note { get; set; }
        public string passage { get; set;}
        public string grade { get; set;}
        public int validated { get; set; } // 0: pas encore traité, 1 non acceptée, 2 acceptée
    }
}