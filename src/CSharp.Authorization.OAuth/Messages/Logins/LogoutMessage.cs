﻿using CommunityToolkit.Mvvm.Messaging.Messages;

namespace CSharp.Authorization.OAuth.Messages.Logins
{
    public class LogoutMessage(bool LogoutResult) : ValueChangedMessage<bool>(LogoutResult)
    {
    }
}
