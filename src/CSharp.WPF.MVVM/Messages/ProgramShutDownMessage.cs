using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.WPF.MVVM.Messages
{
    public class ProgramShutDownMessage : ValueChangedMessage<bool>
    {
        public ProgramShutDownMessage(bool ShutDwonResult) : base(ShutDwonResult)
        {

        }
    }
}
