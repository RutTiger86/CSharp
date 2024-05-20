using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CSharp.Authorization.OAuth.Messages;
using CSharp.Authorization.OAuth.Messages.Logins;
using CSharp.Authorization.OAuth.ViewModels;

namespace CSharp.Authorization.OAuth
{
    public class MainWindowModel : BaseModel, IRecipient<LoginMessage>
    {
        #region Command

        /// <summary>
        /// 메인윈도우 닫힘 커멘드
        /// </summary>
        public RelayCommand? MainClosedCommand
        {
            get;
            private set;
        }


        public RelayCommand? LogoutCommand
        {
            get;
            private set;
        }

        #endregion

        #region  Binding Value

        private string? userName = string.Empty;
        public string? UserName
        {
            get => userName;
            set => SetProperty(ref userName, value);
        }
        #endregion

        public MainWindowModel()
        {
            try
            {
                LogInfo("★★★★★ MainWindowModel Start ★★★★★");
                SettingMessage();
                SettingCommand();
                LogInfo("loginWindows Show");
            }
            catch (Exception ex)
            {
                LogException(ex.Message);
            }
        }
        private void SettingMessage()
        {
            try
            {
                WeakReferenceMessenger.Default.Register<LoginMessage>(this);
            }
            catch (Exception ex)
            {
                LogException(ex.Message);
            }
        }

        private void SettingCommand()
        {
            try
            {
                MainClosedCommand = new RelayCommand(MainClosed);
                LogoutCommand = new RelayCommand(Logout);
                LogInfo("SettingCommand Done");
            }
            catch (Exception ex)
            {
                LogException(ex.Message);
            }
        }

        private void Logout()
        {
            try
            {
                UserName = string.Empty;
                WeakReferenceMessenger.Default.Send(new LogoutMessage(true));
            }
            catch (Exception ex)
            {
                LogException(ex.Message);
            }
        }

        private void MainClosed()
        {
            try
            {
                WeakReferenceMessenger.Default.Send(new ProgramShutDownMessage(true));
            }
            catch (Exception ex)
            {
                LogException(ex.Message);
            }
        }

        public void Receive(LoginMessage message)
        {
            UserName = message.Value.email;
        }
    }
}
