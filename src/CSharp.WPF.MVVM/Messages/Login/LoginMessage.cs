using CommunityToolkit.Mvvm.Messaging.Messages;
using CSharp.WPF.MVVM.Models.Users;

namespace CSharp.WPF.MVVM.Messages.Login
{
    public class LoginMessage(UserInfo loginUserInfo) : ValueChangedMessage<UserInfo>(loginUserInfo)
    {
    }
}
