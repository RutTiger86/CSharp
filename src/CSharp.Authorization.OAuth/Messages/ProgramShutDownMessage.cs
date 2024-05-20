using CommunityToolkit.Mvvm.Messaging.Messages;

namespace CSharp.Authorization.OAuth.Messages
{
    public class ProgramShutDownMessage : ValueChangedMessage<bool>
    {
        public ProgramShutDownMessage(bool ShutDwonResult) : base(ShutDwonResult)
        {

        }
    }
}
