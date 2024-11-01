using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Repository;
namespace Data.ImplementeRepository
{
    public class ImplementeLoginRepository(AppDbContext context) : ILoginRepository
    {
        public async Task<bool> Login(string username, string password)
        {
            var Data = await context.User.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
            return Data != null;
        }
    }
}
