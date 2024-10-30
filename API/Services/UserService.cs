using ApiDevBP.Entities;
using ApiDevBP.Models;
using SQLite;


namespace ApiDevBP.Services
{
    public class UserService
    {
        private readonly SQLiteConnection _db;

        public UserService(string dbPath)
        {
            _db = new SQLiteConnection(dbPath);
            _db.CreateTable<UserEntity>();
        }
        public IEnumerable<UserModel> GetAllUsers()
        {
            var users = _db.Table<UserEntity>().ToList();
            return users.Select(x => new UserModel { Id= x.Id, Name = x.Name, Lastname = x.Lastname});

        }
        public bool CreateUser(UserModel user)
        {
            var entity = new UserEntity { Id = user.Id, Name = user.Name, Lastname = user.Lastname};
            return _db.Insert(entity) > 0;
        }

        public bool UpdateUser(int id, UserModel user)
        {
            var existe = _db.Find<UserEntity>(id);
            if (existe == null)
            {
                return false;
            }

            existe.Id = user.Id;
            existe.Name = user.Name;
            existe.Lastname = user.Lastname;
            return _db.Update(existe) > 0;
        }

        public bool DeleteUser(int id)
        {
            return _db.Delete<UserEntity>(id) > 0;
        }
    }
}
