using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CSharp.WPF.MVVM.Messages;
using CSharp.WPF.MVVM.Messages.Login;
using CSharp.WPF.MVVM.Services.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.WPF.MVVM.ViewModels.Login
{

    public class LoginWindowModel : BaseModel, IDisposable
    {
        #region Command
        public RelayCommand? LoginCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// 윈도우 닫힘 커멘드
        /// </summary>
        public RelayCommand? WindowClosedCommand
        {
            get;
            private set;
        }

        #endregion

        #region Variable
        private bool disposed = false;
        private readonly ILoginService loginService;
        #endregion

        public LoginWindowModel(ILoginService loginService)
        {
            try
            {
                this.loginService = loginService;
                SettingCommand();
            }
            catch (Exception ex)
            {
                LogException(ex.Message);
            }
        }

        ~LoginWindowModel()
        {
            Dispose(disposing: false);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }

            if (disposing)
            {

            }
            // Note disposing has been done.
            disposed = true;

            base.Dispose(disposing);
        }


        private void SettingCommand()
        {
            try
            {
                LoginCommand = new RelayCommand(Login);
                WindowClosedCommand = new RelayCommand(WindowClosed);
            }
            catch (Exception ex)
            {
                LogException(ex.Message);
            }
        }
        private async void Login()
        {
            try
            {
                var userInfo = await loginService.TryLogin();

                WeakReferenceMessenger.Default.Send(new LoginMessage(userInfo));
            }
            catch (Exception ex)
            {
                LogException(ex.Message);
            }
        }

        private void WindowClosed()
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
    }
}
