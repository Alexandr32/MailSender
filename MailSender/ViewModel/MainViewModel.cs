using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using MailSenderNameSpace.Services;
using System;
using System.Windows;
using ListViewItemScheduler;
using System.Collections.Generic;
using MyToolBar;

namespace MailSenderNameSpace.ViewModel
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
        /// <summary>
        /// ��-�� ��� ��������� ������ UI
        /// </summary>
        public RelayCommand ReadAllCommand { get; set; }

        /// <summary>
        /// ������� ������ ���������� ������ ���������������� ������
        /// </summary>
        public RelayCommand AddScheduledMailCommand { get; set; }
        public RelayCommand<ToolBarControl> SendSchedulerCommand { get; set; }


        ObservableCollection<Email> _Emails = new ObservableCollection<Email>();

        /// <summary>
        /// �������� � ������� 
        /// </summary>
        public ObservableCollection<Email> Emails
        {
            get => _Emails;
            set
            {
                _Emails = value;
                // ���������� ������ �� �����
                // ����� ����������� ����-�� Emails ������� � ��������� ������ ��-�� EmailView
                // ������� ������������ �� view
                _EmailView = new CollectionViewSource { Source = value };
                _EmailView.Filter += (sender, e) => 
                {
                    if (!(e.Item is Email email) || string.IsNullOrWhiteSpace(_FilterName)) return;
                    // ���� ������ �� ������ � ������� ������ �� �������� ��� ������ �� �������
                    if(!email.Name.Contains(_FilterName))
                    {
                        e.Accepted = false;
                    }
                };
                // ��������� �������� � ��������
                RaisePropertyChanged(nameof(EmailView));
            }
        }

        string _FilterName = string.Empty;
        /// <summary>
        /// �������� ��� ����� �� �����
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

        // ������ ��������� ������������ ��� ���������� ������ �� �����
        CollectionViewSource _EmailView;
        /// <summary>
        /// ��������� ��� ����������� ������ � ��� ����� � ����� ������ 
        /// </summary>
        public ICollectionView EmailView => _EmailView?.View;

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
        /// �������������� ����� ��������� ������ MainViewModel.
        /// </summary>
        // ������ DataAccessService ����� �������� � MainViewModel ��������� IoC.
        public MainViewModel(IDataAccessService servProxy)
        {
            _serviceProxy = servProxy;
            Emails = new ObservableCollection<Email>();
            EmailInfo = new Email();
            // �������� ������� ���� ����� ��� ������ ���������� ��� ������ ����� ��-��
            ReadAllCommand = new RelayCommand(GetEmails);
            SaveCommand = new RelayCommand<Email>(SaveEmail);
            AddScheduledMailCommand = new RelayCommand(AddScheduledMail);
            SendSchedulerCommand = new RelayCommand<ToolBarControl>(SendScheduler);

            ScheduledMail = new ObservableCollection<ItemSchedulerControl>();

        }

        /// <summary>
        /// ���������� �������������� �����
        /// </summary>
        private void SendScheduler(ToolBarControl tbSender)
        {
            var dic = new Dictionary<DateTime, string>();
            foreach (var item in scheduledMail)
            {
                dic.Add(DateTime.Parse(item.Time), item.TextMail);
            }

            SchedulerClass sc = new SchedulerClass
            {
                DatesEmailTexts = dic
            };

            EmailSendServiceClass emailSender = new EmailSendServiceClass(tbSender.SelectedText, tbSender.SelectedValue);
            //sc.SendEmails(dtSendDateTime, emailSender, (Obse<Email>)dgEmails.ItemsSource);
            
            sc.SendEmails(emailSender, Emails);

            MessageBox.Show($"{tbSender.SelectedText}, {tbSender.SelectedValue}");
        }

        ObservableCollection<ItemSchedulerControl> scheduledMail;
        public ObservableCollection<ItemSchedulerControl> ScheduledMail
        {
            get => scheduledMail;
            set
            {
                scheduledMail = value;
                RaisePropertyChanged(nameof(ScheduledMail));
            }
        }

        /// <summary>
        /// ��������� ��������������� ������
        /// </summary>
        void AddScheduledMail()
        {
            ScheduledMail.Add(new ItemSchedulerControl(ScheduledMail));
        }
    }
}