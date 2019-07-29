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
    /// ���� ����� �������� ����������� ������ �� ��� ������ ������������� �
    /// ���������� � ������������ ����� ����� ��� ��������.
    /// </ summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// �������������� ����� ��������� ������ ViewModelLocator.
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

            // ������������ ������ ������-�������
            // IOC-���������� �������������� ����� MainViewModel
            SimpleIoc.Default.Register<MainViewModel>();
            // ����������� ������ �� ������� ����� ������
            SimpleIoc.Default.Register<IDataAccessService, DataAccessService>();
        }

        /// <summary>
        /// ��-�� ���������� ��������� ������ MainViewModel ��� ������ � ���
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