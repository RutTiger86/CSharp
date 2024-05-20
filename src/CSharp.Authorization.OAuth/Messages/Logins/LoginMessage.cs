using CommunityToolkit.Mvvm.Messaging.Messages;
using CSharp.Authorization.OAuth.Models.Google;

namespace CSharp.Authorization.OAuth.Messages.Logins
{
    public class LoginMessage : ValueChangedMessage<GoogleUserInfo>
    {
        public LoginMessage(GoogleUserInfo loginUserInfo) : base(loginUserInfo)
        {

        }
    }
}
