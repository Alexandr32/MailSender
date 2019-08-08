using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using MailSenderNameSpace.Services;

namespace MailSenderNameSpace.ViewModel
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
    }
}