using CommunityToolkit.Mvvm.Messaging.Messages;
using CSharp.WPF.MVVM.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.WPF.MVVM.Messages.Login
{
    public class LoginMessage : ValueChangedMessage<UserInfo>
    {
        public LoginMessage(UserInfo loginUserInfo) : base(loginUserInfo)
        {

        }
    }
}
