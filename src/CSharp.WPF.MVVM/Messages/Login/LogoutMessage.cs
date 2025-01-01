using CommunityToolkit.Mvvm.Messaging.Messages;

namespace CSharp.WPF.MVVM.Messages.Login
{
    public class LogoutMessage(bool LogoutResult) : ValueChangedMessage<bool>(LogoutResult)
    {
    }
}
