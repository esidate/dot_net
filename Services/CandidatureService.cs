using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dot_net.Entities;
using dot_net.Data;
using dot_net.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace dot_net.Services
{
    public interface ICandidatureService
    {
        CandidatureModel addCandidature(string jsonContent);
        Candidature updateCandidature(int id, string note ,int validated);
        Candidature getById(int id);
        Candidature getByToken(string token);
        Task<object>  getUntreatedCandidatures();
        Task<object>  getTreatedCandidatures();
    }

    public class CandidatureService : ICandidatureService
    {
        private DataContext _dataContext;
        private IUserService _userService;
        public CandidatureService(IUserService userService,DataContext datacontext)
        {
            _dataContext = datacontext;
            _userService = userService;
        }

        public CandidatureModel addCandidature(string jsonContent)
        {
            var candidatureBody = (JObject) JsonConvert.DeserializeObject(jsonContent);
            string lastName = candidatureBody["donneesPersonnelles"]["nom"].Value<string>();
            string firstName  = candidatureBody["donneesPersonnelles"]["prenom"].Value<string>();
            string refrenceToken = _userService.GeneratePassword(10);
            string creationDate = DateTime.Now.ToString("dd-MM-yyyy");
            Candidature candidature = new Candidature
            {
                JsonContent = jsonContent,
                CandidateLastName = lastName,
                CandidateFirstName = firstName,
                RefrenceToken = refrenceToken,
                CreatedDate = creationDate
            };
            _dataContext.Candidatures.Add(candidature);
            _dataContext.SaveChanges();
            return new CandidatureModel {
                id = candidature.Id, 
                lastName = lastName,
                firstName = firstName,
                refrenceToken = refrenceToken,
                createdDate = creationDate
            };
        }

        public Candidature getById(int id)
        {
            var candidature = _dataContext.Candidatures.FirstOrDefault(x => x.Id == id);
            if(candidature == null)
                return null;
            else
                return candidature;
        }

        public Candidature getByToken(string token)
        {
            var candidature = _dataContext.Candidatures.FirstOrDefault(x => x.RefrenceToken == token);
            if(candidature == null)
                return null;
            else
                return candidature;
        }

        public Candidature updateCandidature(int id, string note,int validated)
        {
            Candidature candidature = _dataContext.Candidatures.Find(id);
            if(candidature == null)
                return null;
            else{
                candidature.Note = note;
                candidature.Validated = validated;
                _dataContext.SaveChanges();
                return candidature;
            }
        }

       
        public async Task<object> getTreatedCandidatures()
        {
            var candidatures = await Task.Run(() => 
            _dataContext.Candidatures.Where(candid => candid.Validated != 0).Select(c => 
            new {
                c.Id,
                c.CandidateLastName,
                c.CandidateFirstName,
                c.RefrenceToken,
                c.CreatedDate,
                c.Validated
            }
            ).ToList());

            return candidatures;
        }

        public async Task<object> getUntreatedCandidatures()
        {
            var candidatures = await Task.Run(() => 
            _dataContext.Candidatures.Where(candid => candid.Validated == 0).Select(c => 
            new {
                c.Id,
                c.CandidateLastName,
                c.CandidateFirstName,
                c.RefrenceToken,
                c.CreatedDate,
                c.Validated
            }
            ).ToList());

            return candidatures;
        }

    }
}