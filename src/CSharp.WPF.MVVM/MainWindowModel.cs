using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CSharp.WPF.MVVM.Messages;
using CSharp.WPF.MVVM.Messages.Login;
using CSharp.WPF.MVVM.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.WPF.MVVM
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

        private UserInfo? loginUserInfo;

        public UserInfo? LoginUserInfo
        {
            get => loginUserInfo;
            set => SetProperty(ref loginUserInfo, value);
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
                LoginUserInfo = null;

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

            LoginUserInfo = message.Value;
        }
    }
}
