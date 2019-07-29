using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.ObjectModel;
using WpfTestMailSender.Services;

namespace WpfTestMailSender.ViewModel
{
    /// <summary>
    /// ���� ����� �������� ��������, � �������� �������� ������������� ����� ��������� ������.
    /// <para>
    /// ����������� �������� <strong>mvvminpc</strong>, ����� �������� ������������� 
    /// �������� � ���� ������ �������������.
    /// </para>
    /// <para>
    /// �� ����� ������ ������������ Blend ��� �������� ������ � ���������� �����������.
    /// </para>
    /// <para>
    /// ��. Http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        // �������� ������ �� ���������
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
        /// ���������� �� ��������� �����
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
        /// ����������
        /// </summary>
        /// <param name="email"></param>
        void SaveEmail(Email email)
        {
            // ���� Id �� ����� 0, �� ����� ������ ����������� � ��������� Emails .
            EmailInfo.Id = _serviceProxy.CreateEmail(email);
            if (EmailInfo.Id != 0)
            {
                Emails.Add(EmailInfo);
                RaisePropertyChanged(nameof(EmailInfo));
            }
        }

        /// <summary>
        /// ������ ������ �� ������ ���������������� ������ � �������� Emails
        /// ������������ ��� ����� ��� ��������� ������� UI ����� RelayCommand
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
        /// ��-�� ��� ��������� ������ UI
        /// </summary>
        public RelayCommand ReadAllCommand { get; set; }

        /// <summary>
        /// �������������� ����� ��������� ������ MainViewModel.
        /// </summary>
        // ������ DataAccessService ����� �������� � MainViewModel ��������� IoC.
        public MainViewModel(IDataAccessService servProxy)
        {
            _serviceProxy = servProxy;
            Emails = new ObservableCollection<Email>();
            // �������� ������� ���� ����� ��� ������ ���������� ��� ������ ����� ��-��
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