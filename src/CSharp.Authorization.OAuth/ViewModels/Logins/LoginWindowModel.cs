using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CSharp.Authorization.OAuth.Messages;
using CSharp.Authorization.OAuth.Messages.Logins;
using CSharp.Authorization.OAuth.Models.Google;
using CSharp.Authorization.OAuth.Models.Logins;
using CSharp.Authorization.OAuth.Models.Response;
using System.Windows;

namespace CSharp.Authorization.OAuth.ViewModels.Logins
{
    public class LoginWindowModel : BaseModel, IDisposable
    {     
        #region Command
        public RelayCommand? GoogleStartCommand
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
                GoogleStartCommand = new RelayCommand(GoogleStart);
                WindowClosedCommand = new RelayCommand(WindowClosed);
            }
            catch (Exception ex)
            {
                LogException(ex.Message);
            }
        }
        private async void GoogleStart()
        {
            try
            {
                BaseResponse<GoogleUserInfo> googleInfo = await loginService.TryGoogleLogin();

                if (!googleInfo.Result)
                {
                    LogError($"{googleInfo.ErrorCode} : {googleInfo.ErrorMessage}");
                    MessageBox.Show($"Try Google Login Fail. {googleInfo.ErrorCode} : {googleInfo.ErrorMessage}");                    
                }
                else
                {
                    WeakReferenceMessenger.Default.Send(new LoginMessage(googleInfo.Data));
                }
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
