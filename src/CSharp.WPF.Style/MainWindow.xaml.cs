using System.Windows;

namespace CSharp.WPF.Style
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(MainWindowModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}