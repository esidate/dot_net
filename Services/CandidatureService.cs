using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dot_net.Entities;
using dot_net.Data;
using dot_net.Helpers;

namespace dot_net.Services
{
    public interface ICandidatureService
    {
        void addCandidature(string jsonContent);
        void archiveCandidature(int id);
        Candidature updateCandidature(int id ,string candidatureJson);
        Candidature GetById(int id) ;
        IEnumerable<Candidature> GetAll();
    }

    public class CandidatureService : ICandidatureService
    {
        private DataContext _dataContext;
        public CandidatureService(DataContext datacontext){
            _dataContext= datacontext;
        }

        public void addCandidature(string jsonContent){
            Candidature candidature = new Candidature {
                JsonContent = jsonContent
            };
            _dataContext.Candidatures.Add(candidature);
            _dataContext.SaveChanges();
        }

        public Candidature GetById(int id) 
        {
            var candidature = _dataContext.Candidatures.FirstOrDefault(x => x.Id == id);
            return candidature;
        }

        public Candidature updateCandidature(int id ,string candidatureJson)
        {
            Candidature candidature = _dataContext.Candidatures.Find(id);
            candidature.JsonContent = candidatureJson;
            _dataContext.SaveChanges();
            return candidature;
        }


        public void archiveCandidature(int id){
            Candidature candidature = _dataContext.Candidatures.Find(id);
            candidature.Archived = 1;
            _dataContext.SaveChanges(); 
        }



        public IEnumerable<Candidature> GetAll()   
        {
            return  _dataContext.Candidatures.ToList();
        }

    }
}