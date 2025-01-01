using CommunityToolkit.Mvvm.Messaging.Messages;

namespace CSharp.Authorization.OAuth.Messages
{
    public class ProgramShutDownMessage(bool ShutDwonResult) : ValueChangedMessage<bool>(ShutDwonResult)
    {
    }
}
