using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using MailSenderNameSpace.Services;

namespace MailSenderNameSpace.ViewModel
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
    }
}