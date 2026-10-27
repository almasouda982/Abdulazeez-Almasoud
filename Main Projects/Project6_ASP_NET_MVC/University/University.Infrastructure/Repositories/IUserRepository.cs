using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.Domain.Models;

namespace University.Infrastructure.Repositories
{
    public interface IUserRepository
    {

        User? GetByUsername(string username);


        User? GetByEmail(string email);

        void Add(User user);


    }
}

