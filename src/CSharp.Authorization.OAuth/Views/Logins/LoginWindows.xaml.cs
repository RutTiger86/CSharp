using CSharp.Authorization.OAuth.Models.Logins;
using CSharp.Authorization.OAuth.ViewModels.Logins;
using System.Windows;

namespace CSharp.Authorization.OAuth.Views.Logins
{
    /// <summary>
    /// LoginWindows.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class LoginWindows : Window
    {
        public LoginWindows(ILoginService loginService)
        {
            InitializeComponent();
            this.DataContext = new LoginWindowModel(loginService);
        }
    }
}
