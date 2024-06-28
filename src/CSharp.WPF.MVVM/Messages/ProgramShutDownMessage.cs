using CommunityToolkit.Mvvm.Messaging.Messages;

namespace CSharp.WPF.MVVM.Messages
{
    public class ProgramShutDownMessage : ValueChangedMessage<bool>
    {
        public ProgramShutDownMessage(bool ShutDwonResult) : base(ShutDwonResult)
        {

        }
    }
}
