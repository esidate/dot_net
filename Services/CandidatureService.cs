using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dot_net.Entities;
using dot_net.Data;
using dot_net.Models;


namespace dot_net.Services
{
    public interface ICandidatureService
    {
        void addCandidature(string jsonContent);
        bool archiveCandidature(int id);
        Candidature updateCandidature(int id, string candidatureJson);
        Candidature getById(int id);
        Task<CandidatureIdsModel> getAllIds();
    }

    public class CandidatureService : ICandidatureService
    {
        private DataContext _dataContext;
        public CandidatureService(DataContext datacontext)
        {
            _dataContext = datacontext;
        }

        public void addCandidature(string jsonContent)
        {
            Candidature candidature = new Candidature
            {
                JsonContent = jsonContent
            };
            _dataContext.Candidatures.Add(candidature);
            _dataContext.SaveChanges();
        }

        public Candidature getById(int id)
        {
            var candidature = _dataContext.Candidatures.FirstOrDefault(x => x.Id == id);
            if(candidature == null)
                return null;
            else
                return candidature;
        }

        public Candidature updateCandidature(int id, string candidatureJson)
        {
            Candidature candidature = _dataContext.Candidatures.Find(id);
            if(candidature == null)
                return null;
            else{
                candidature.JsonContent = candidatureJson;
                _dataContext.SaveChanges();
                return candidature;
            }
        }

        public bool archiveCandidature(int id)
        {
            Candidature candidature = _dataContext.Candidatures.Find(id);
            if(candidature == null)
                return false;
            else{
                candidature.Archived = 1;
                _dataContext.SaveChanges();
                return true;
            }
        }

        public async Task<CandidatureIdsModel> getAllIds()
        {
            IEnumerable<int> idsList = await Task.Run(() => _dataContext.Candidatures.Where(candid => candid.Archived == 0).Select(p => p.Id).ToList());
            CandidatureIdsModel candidatureIds = new CandidatureIdsModel() 
            {
                ids = idsList
            };
            return candidatureIds;
        }

    }
}