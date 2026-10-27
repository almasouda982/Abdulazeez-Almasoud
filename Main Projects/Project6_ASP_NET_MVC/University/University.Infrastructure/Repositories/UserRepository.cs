using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.Domain.Models;
using University.Infrastructure.Data;

namespace University.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _db;
        public UserRepository(AppDbContext db)
        {
            _db = db;
        }
        public User? GetByUsername(string username)
        {
            return _db.Users.FirstOrDefault(x => x.UserName == username);
        }
        public User? GetByEmail(string email)
        {
            return _db.Users.FirstOrDefault(x => x.Email == email);
        }
        public void Add(User user)
        {
            _db.Users.Add(user);
            _db.SaveChanges();
        }
    }
}
