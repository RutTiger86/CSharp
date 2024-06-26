using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.WPF.MVVM.Messages.Login
{
    public class LogoutMessage : ValueChangedMessage<bool>
    {
        public LogoutMessage(bool LogoutResult) : base(LogoutResult)
        {

        }
    }
}
