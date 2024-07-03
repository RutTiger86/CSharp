using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using log4net;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CSharp.WPF.Style
{

    public class MainWindowModel : ObservableObject
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        #region Command

        /// <summary>
        /// 메인윈도우 닫힘 커멘드
        /// </summary>
        public RelayCommand MainClosedCommand
        {
            get;
            private set;
        }


        public RelayCommand<Type> MenuClickCommand
        {
            get;
            private set;
        }


        public RelayCommand<string> SearchCommand
        {
            get;
            private set;
        }
        

        #endregion

        #region  Binding Value

        private object mainView;

        public object MainView
        {
            get => mainView;
            set => SetProperty(ref mainView, value);
        }

        private string searchText;

        public string SearchText
        {
            get => searchText;
            set => SetProperty(ref searchText, value);
        }

        
        #endregion
        private readonly IServiceProvider serviceProvider;
        public MainWindowModel(IServiceProvider serviceProvider)
        {
            try
            {
                this.serviceProvider = serviceProvider;
                SettingCommand();
            }
            catch (Exception ex)
            {
                log.Error(ex);

            }
        }


        private void SettingCommand()
        {
            try
            {
                MenuClickCommand = new RelayCommand<Type>(p => OnMenuClick(p));
                MainClosedCommand = new RelayCommand(OnMainClosed);
                SearchCommand = new RelayCommand<string>( p=> OnSearch(p));
            }
            catch (Exception ex)
            {
                log.Error(ex);

            }
        }
        private void OnMainClosed()
        {
            try
            {
                Application.Current.Shutdown();
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        private void OnSearch(string message)
        {
            try
            {
                MessageBox.Show(message);
            }
            catch (Exception ex)
            {

                log.Error(ex);
            }
        }

        private void OnMenuClick(Type parm)
        {
            try
            {
                MainView = serviceProvider.GetRequiredService(parm);
            }
            catch (Exception ex)
            {

                log.Error(ex);
            }
        }
    }
}
