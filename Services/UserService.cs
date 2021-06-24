using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dot_net.Entities;
using dot_net.Data;
using dot_net.Helpers;
using dot_net.Models;
namespace dot_net.Services
{
    public interface IUserService
    {
        Task<User> Authenticate(string username, string password);
        Task<IEnumerable<User>> GetAll();
        Task<IEnumerable<User>> GetEvaluators();
        User GetById(int id);
        Task<User> AddEvaluator(User User);
        void AddUser(User User);
        bool toggleEvaluatorsBlock(int id);
        string GeneratePassword(int length);
        int modifyUser(ModifyUserModel model);
    }

    public class UserService : IUserService
    {
        private DataContext _dataContext;

        public UserService(DataContext datacontext)
        {
            _dataContext = datacontext;

        }

        public async Task<User> Authenticate(string username, string password)
        {
            var user = _dataContext.Users.FirstOrDefault(user => user.Username == username && user.Blocked == false);

            if (user == null)
                return null;

            var verified = BCrypt.Net.BCrypt.Verify(password, user.Password);
            if (verified)
            {
                var userData = new User { Username = user.Username, FirstName = user.FirstName, LastName = user.LastName, Password = user.Password };
                return await Task.Run(() => user);
            }
            else
                return null;
        }

        public User GetById(int id)
        {
            var user = _dataContext.Users.FirstOrDefault(x => x.Id == id);
            return user.WithoutPassword();
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await Task.Run(() => _dataContext.Users.ToList());
        }

        public async Task<IEnumerable<User>> GetEvaluators()
        {
            return await Task.Run(() => _dataContext.Users.Where(user => user.Role == Role.Evaluator));
        }

        public async Task<User> AddEvaluator(User User)
        {
            var user = _dataContext.Users.FirstOrDefault(user => User.Username == user.Username);

            if (user == null)
            {
                // User.Role = "Evaluator";
                var password = GeneratePassword(10);
                User.Password = BCrypt.Net.BCrypt.HashPassword(password);
                var addUser = await _dataContext.Users.AddAsync(User);
                _dataContext.SaveChanges();
                var userEntity = addUser.Entity;
                userEntity.Password = password; // Return plain text password (temporary password aka token)
                return userEntity;
            }
            else
                return null;
        }

        public int modifyUser(ModifyUserModel model){
            var user = _dataContext.Users.FirstOrDefault(user => user.Username == model.username);

            if (user != null){
                if(_dataContext.Users.FirstOrDefault(u => u.Username == model.newUsername && model.username != model.newUsername) == null)
                {
                    user.Username = model.newUsername;
                    user.Password = BCrypt.Net.BCrypt.HashPassword(model.newPassword);
                    user.Role = "Evaluator";
                    _dataContext.SaveChanges();
                    return 2;
                }
                else
                    return 1;
            }
            else
                return 0;
        }

        public bool toggleEvaluatorsBlock(int id)
        {
            User eval = _dataContext.Users.FirstOrDefault(user => user.Role == "Evaluator" && user.Id == id);
            if (eval == null)
                return false;
            else
            {
                eval.Blocked = !eval.Blocked;
                _dataContext.SaveChanges();
                return true;
            }
        }

        public string GeneratePassword(int length)
        {
            using (System.Security.Cryptography.RNGCryptoServiceProvider cryptRNG = new System.Security.Cryptography.RNGCryptoServiceProvider())
            {
                byte[] tokenBuffer = new byte[length];
                cryptRNG.GetBytes(tokenBuffer);
                return System.Convert.ToBase64String(tokenBuffer).Replace('+', 'x').Replace('-', 'y').Replace('/', 'z').Replace('=', 't');
            }
        }

        public async void AddUser(User User)
        {
            var password = User.Password;
            User.Password = BCrypt.Net.BCrypt.HashPassword(password);
            await _dataContext.Users.AddAsync(User);
            _dataContext.SaveChanges();
        }
    }
}