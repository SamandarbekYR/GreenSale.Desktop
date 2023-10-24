using GreenSale.ViewModels.Models.Auth;
using GreenSale.ViewModels.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenSale.Integrated.Interfaces.Users
{
    public interface IUserService
    {
        public Task<UserModel> GetAsync();
        public Task<bool> UpdateAsync(UserDto dto);
        public Task<bool> UpdateSecurityAsync(UsersecurtyDto dto);
        public Task<long> CountAsync();
        public Task<bool> DeleteAsync();
    }
}
