/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:WpfTestMailSender"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;

namespace WpfTestMailSender.ViewModel
{
    /// <summary>
    /// Ётот класс содержит статические ссылки на все модели представлений в
    /// приложение и обеспечивает точку входа дл€ прив€зок.
    /// </ summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// »нициализирует новый экземпл€р класса ViewModelLocator.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}

            // IOC-контейнере регистрируетс€ класс MainViewModel
            SimpleIoc.Default.Register<MainViewModel>();
        }

        /// <summary>
        /// —в-во возвращает экземпл€р класса MainViewModel дл€ работы с ним
        /// </summary>
        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }
        
        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}