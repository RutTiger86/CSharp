using CommunityToolkit.Mvvm.Messaging.Messages;
using CSharp.WPF.MVVM.Models.Users;

namespace CSharp.WPF.MVVM.Messages.Login
{
    public class LoginMessage : ValueChangedMessage<UserInfo>
    {
        public LoginMessage(UserInfo loginUserInfo) : base(loginUserInfo)
        {

        }
    }
}
