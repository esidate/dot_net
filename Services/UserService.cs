using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dot_net.Entities;
using dot_net.Data;
using dot_net.Helpers;

namespace dot_net.Services
{
    public interface IUserService
    {
        Task<User> Authenticate(string username, string password);
        Task<IEnumerable<User>> GetAll();
        Task<IEnumerable<User>> GetEvaluators();
        User GetById(int id);
        Task<IEnumerable<User>> GetEvaluators();
    }

        public class UserService : IUserService
    {
        private DataContext _dataContext;
        
        public UserService(DataContext datacontext){
            _dataContext= datacontext;
            
        }

        public async Task<User> Authenticate(string username, string password)
        {
            // wrapped in "await Task.Run" to mimic fetching user from a db
            var user = await Task.Run(() =>_dataContext.Users.SingleOrDefault(user => user.Username == username && user.Password == password));
            // return null if user not found
            if (user == null)
                return null;
            // authentication successful so return user details
            return user;
        }
         
        public User GetById(int id) 
        {
            var user = _dataContext.Users.FirstOrDefault(x => x.Id == id);
            return user.WithoutPassword();
        }
        public async Task<IEnumerable<User>> GetAll()   
        {
            // wrapped in "await Task.Run" to mimic fetching users from a db
            return await Task.Run(() => _dataContext.Users.ToList());
        }

        public async Task<IEnumerable<User>> GetEvaluators()
        {
            // wrapped in "await Task.Run" to mimic fetching users from a db
            return await Task.Run(() => _dataContext.Users.Where(user => user.Role == Role.Evaluator ));
        }
    }
}