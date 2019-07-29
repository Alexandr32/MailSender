using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.ObjectModel;
using WpfTestMailSender.Services;

namespace WpfTestMailSender.ViewModel
{
    /// <summary>
    /// Этот класс содержит свойства, с которыми основное представление может связывать данные.
    /// <para>
    /// Используйте фрагмент <strong>mvvminpc</strong>, чтобы добавить привязываемые 
    /// свойства к этой модели представления.
    /// </para>
    /// <para>
    /// Вы также можете использовать Blend для привязки данных с поддержкой инструмента.
    /// </para>
    /// <para>
    /// См. Http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        // Передает данные из хранилища
        IDataAccessService _serviceProxy;

        public RelayCommand<Email> SaveCommand { get; set; }

        ObservableCollection<Email> _Emails;
        public ObservableCollection<Email> Emails
        {
            get => _Emails;
            set
            {
                _Emails = value;
                RaisePropertyChanged(nameof(Emails));
            }
        }


        Email _EmailInfo;
        /// <summary>
        /// Информация об полуателе писма
        /// </summary>
        public Email EmailInfo
        {
            get { return _EmailInfo; }
            set
            {
                _EmailInfo = value;
                RaisePropertyChanged(nameof(EmailInfo));
            }
        }

        /// <summary>
        /// Сохранение
        /// </summary>
        /// <param name="email"></param>
        void SaveEmail(Email email)
        {
            // Если Id не равно 0, то новое письмо добавляется в коллекцию Emails .
            EmailInfo.Id = _serviceProxy.CreateEmail(email);
            if (EmailInfo.Id != 0)
            {
                Emails.Add(EmailInfo);
                RaisePropertyChanged(nameof(EmailInfo));
            }
        }

        /// <summary>
        /// Запрос данных из класса предоставляющего данные в свойство Emails
        /// используется как метод для обработки команды UI через RelayCommand
        /// </summary>
        void GetEmails()
        {
            Emails.Clear();
            foreach (var item in _serviceProxy.GetEmails())
            {
                Emails.Add(item);
            }
        }

        /// <summary>
        /// Св-во для обработки команд UI
        /// </summary>
        public RelayCommand ReadAllCommand { get; set; }

        /// <summary>
        /// Инициализирует новый экземпляр класса MainViewModel.
        /// </summary>
        // Объект DataAccessService будет доступен в MainViewModel благодаря IoC.
        public MainViewModel(IDataAccessService servProxy)
        {
            _serviceProxy = servProxy;
            Emails = new ObservableCollection<Email>();
            // Передаем свойтву каой метод оно должно подтавлять при вызови этого св-ва
            ReadAllCommand = new RelayCommand(GetEmails);

            ReadAllCommand = new RelayCommand(GetEmails);
            SaveCommand = new RelayCommand<Email>(SaveEmail);

            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}
        }
    }
}