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
using WpfTestMailSender.Services;

namespace WpfTestMailSender.ViewModel
{
    /// <summary>
    /// Этот класс содержит статические ссылки на все модели представлений в
    /// приложение и обеспечивает точку входа для привязок.
    /// </ summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса ViewModelLocator.
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

            // Используется шаблон сервис-локатор
            // IOC-контейнере регистрируется класс MainViewModel
            SimpleIoc.Default.Register<MainViewModel>();
            // Подстановка данных из которых брать данные
            SimpleIoc.Default.Register<IDataAccessService, DataAccessService>();
        }

        /// <summary>
        /// Св-во возвращает экземпляр класса MainViewModel для работы с ним
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