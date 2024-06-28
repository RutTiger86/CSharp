using CommunityToolkit.Mvvm.Messaging.Messages;

namespace CSharp.WPF.MVVM.Messages.Login
{
    public class LogoutMessage : ValueChangedMessage<bool>
    {
        public LogoutMessage(bool LogoutResult) : base(LogoutResult)
        {

        }
    }
}
