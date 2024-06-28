using CSharp.WPF.MVVM.ViewModels.MainViews;

namespace CSharp.WPF.MVVM.Views.MainViews
{
    /// <summary>
    /// ViewB.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ViewB : BaseView
    {
        public ViewB(ViewBViewModel model)
        {
            InitializeComponent();
            this.DataContext = model;
        }
    }
}
