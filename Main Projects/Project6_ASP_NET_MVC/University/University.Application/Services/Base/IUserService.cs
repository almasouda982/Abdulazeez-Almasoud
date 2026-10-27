using Azure.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.Application.Dtos;
using University.Domain.Models;

namespace University.Application.Services.Base
{
    public interface IUserService
    {
        User? Login(LoginDto loginDto);

        User? Register(RegisterDto registerDto);

    }

}
