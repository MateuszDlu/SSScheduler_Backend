using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SuperSimpleScheduler_Backend.Models;

namespace SuperSimpleScheduler_Backend.Services.Interfaces
{
    public interface ILoginService
    {
        public Task<Object> loginAsync(string email, string password);
    }
}