using CSharp.WPF.MVVM.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.WPF.MVVM.Services.Login
{
    public interface ILoginService
    {
        public Task<UserInfo> TryLogin();
    }

    public class LoginService : BaseModel, ILoginService
    {
        public Task<UserInfo> TryLogin()
        {
            return Task.FromResult(new UserInfo()
            {
                email = "test@test.com",
                id = 100,
                name = "Test Name",
            });
        }

    }
}
