using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
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

        ObservableCollection<Email> _Emails = new ObservableCollection<Email>();

        /// <summary>
        /// Колекция с данными 
        /// </summary>
        public ObservableCollection<Email> Emails
        {
            get => _Emails;
            set
            {
                _Emails = value;
                // Реализация поиска по имени
                // Когда обновляется свой-во Emails создаем и обновляем данные св-ва EmailView
                // которое отображается во view
                _EmailView = new CollectionViewSource { Source = value };
                _EmailView.Filter += (sender, e) => 
                {
                    if (!(e.Item is Email email) || string.IsNullOrWhiteSpace(_FilterName)) return;
                    // Если строка не входит в искомый объект то сообщаем что строка не найдена
                    if(!email.Name.Contains(_FilterName))
                    {
                        e.Accepted = false;
                    }
                };
                // Обновляем привязку к свойству
                RaisePropertyChanged(nameof(EmailView));
            }
        }

        string _FilterName = string.Empty;
        /// <summary>
        /// Свойство для поика по имени
        /// </summary>
        public string FilterName
        {
            get => _FilterName;
            set
            {
                _FilterName = value;
                RaisePropertyChanged(nameof(FilterName));
            }
        }

        // Данная коллецкия используется для реализации поиска по имени
        CollectionViewSource _EmailView;
        /// <summary>
        /// Коллекция для отображения данных в том числе и после поиска 
        /// </summary>
        public ICollectionView EmailView => _EmailView?.View;

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
            EmailInfo = new Email();
            // Передаем свойтву каой метод оно должно подтавлять при вызови этого св-ва
            ReadAllCommand = new RelayCommand(GetEmails);
            SaveCommand = new RelayCommand<Email>(SaveEmail);

        }
    }
}