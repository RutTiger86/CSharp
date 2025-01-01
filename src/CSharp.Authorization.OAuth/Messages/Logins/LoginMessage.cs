using CommunityToolkit.Mvvm.Messaging.Messages;
using CSharp.Authorization.OAuth.Models.Google;

namespace CSharp.Authorization.OAuth.Messages.Logins
{
    public class LoginMessage(GoogleUserInfo loginUserInfo) : ValueChangedMessage<GoogleUserInfo>(loginUserInfo)
    {
    }
}
