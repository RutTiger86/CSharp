using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CSharp.WPF.MVVM.Messages;
using CSharp.WPF.MVVM.Messages.Login;
using CSharp.WPF.MVVM.Models.Users;
using CSharp.WPF.MVVM.Views.MainViews;
using Microsoft.Extensions.DependencyInjection;

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

        public RelayCommand<Type> ChangeViewCommand
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
            set
            {
                SetProperty(ref loginUserInfo, value);
            }
        }

        private object currentView;
        public object CurrentView
        {
            get => currentView;
            set => SetProperty(ref currentView, value);
        }

        private bool viewInit = false;
        public bool ViewInit
        {
            get => viewInit;
            set => SetProperty(ref viewInit, value);
        }

        #endregion

        private readonly IServiceProvider serviceProvider;

        public MainWindowModel(IServiceProvider serviceProvider)
        {
            try
            {
                LogInfo("★★★★★ MainWindowModel Start ★★★★★");
                this.serviceProvider = serviceProvider;
                SettingMessage();
                SettingCommand();
                LoginInit();
                LogInfo("loginWindows Show");
            }
            catch (Exception ex)
            {
                LogException(ex.Message);
            }
        }

        public void LoginInit()
        {
            ViewInit = true;
            CurrentView = serviceProvider.GetRequiredService(typeof(ViewA));
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
                MainClosedCommand = new RelayCommand(OnMainClosed);
                LogoutCommand = new RelayCommand(OnLogout);
                ChangeViewCommand = new RelayCommand<Type>(OnChangeView);
                LogInfo("SettingCommand Done");
            }
            catch (Exception ex)
            {
                LogException(ex.Message);
            }
        }
        private void OnChangeView(Type? viewType)
        {
            CurrentView = serviceProvider.GetRequiredService(viewType);
        }

        private void OnLogout()
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

        private void OnMainClosed()
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
            LoginInit();
        }
    }
}
