using CommunityToolkit.Mvvm.Messaging.Messages;

namespace CSharp.Authorization.OAuth.Messages.Logins
{
    public class LogoutMessage : ValueChangedMessage<bool>
    {
        public LogoutMessage(bool LogoutResult) : base(LogoutResult)
        {

        }
    }
}
