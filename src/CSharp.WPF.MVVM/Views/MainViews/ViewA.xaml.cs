using CSharp.WPF.MVVM.ViewModels.MainViews;

namespace CSharp.WPF.MVVM.Views.MainViews
{
    /// <summary>
    /// ViewA.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ViewA : BaseView
    {
        public ViewA(ViewAViewModel model)
        {
            InitializeComponent();
            this.DataContext = model;
        }
    }
}
