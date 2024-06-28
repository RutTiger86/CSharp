using CSharp.WPF.MVVM.Models.Users;

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
