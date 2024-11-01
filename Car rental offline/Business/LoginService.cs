using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class LoginService(ILoginRepository repository) : ILoginRepository
    {
        public async Task<bool> Login(string username, string password)
        {
            return await repository.Login(username, password);
        }
    }
}