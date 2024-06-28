using CSharp.WPF.MVVM.ViewModels.Login;
using System.Windows;

namespace CSharp.WPF.MVVM.Views.Login
{
    /// <summary>
    /// LoginWindows.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow(LoginWindowModel model)
        {
            InitializeComponent();
            this.DataContext = model;
        }
    }
}
