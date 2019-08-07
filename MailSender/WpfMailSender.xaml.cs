using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MailSenderNameSpace.ViewModel;

namespace MailSenderNameSpace
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class WpfMailSender : MetroWindow
    {

        //DBClass db;

        public WpfMailSender()
        {
            InitializeComponent();

            // Передача данных почт отправителей
            tbSender.ComboBoxSenders = VariablesClass.Senders;

            tbSmtp.ComboBoxSmtpClient = VariablesClass.SmtpClient;

            //db = new DBClass();
            //dgEmails.ItemsSource = db.Emails;
        }

        private void BtnSendEmail_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Переход на вкладку "планировщик"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClock_Click(object sender, RoutedEventArgs e)
        {
            tabControl.SelectedItem = tabPlanner;
        }

        /// <summary>
        /// Обработчик события при нажатии на клавишу "отправить сразу"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSendAtOnce_Click(object sender, RoutedEventArgs e)
        {
            string strLogin = tbSender.SelectedText;
            string strPassword = tbSender.SelectedValue;

            //MessageBox.Show(string.Format("strLogin = {0} strPassword ={1}", strLogin, strPassword));

            if (string.IsNullOrEmpty(strLogin))
            {
                MessageBox.Show("Выберите отправителя");
                return;
            }
            if (string.IsNullOrEmpty(strPassword))
            {
                MessageBox.Show("Укажите пароль отправителя");
                return;
            }

            if (string.IsNullOrEmpty(editTextBodyMail.Text))
            {
                MessageBox.Show("Введите текст письма");
                return;
            }



            var emailSender = new EmailSendServiceClass(strLogin, strPassword)
            {
                // Получаем данные из контрода
                StrSmtp = tbSmtp.SelectedText,
                ISmtpPort = Convert.ToInt32(tbSmtp.SelectedValue),

                Body = editTextBodyMail.Text,
                Subject = "Рассылка"
            };

            // Передаем данные из Grid со списком адресов
            //emailSender.SendMails((IQueryable<Email>)dgEmails.ItemsSource);

            // Получаем доступ к ViewModelLocator
            var locator = (ViewModelLocator)FindResource("Locator");

            //string mail = "";
            //foreach (var item in locator.Main.Emails)
            //{
            //    mail = mail + item.Name + " "; 
            //}

            //MessageBox.Show(mail);
            //emailSender.SendMails(locator.Main.Emails);

            SchedulerClass sc = new SchedulerClass();
            //sc.DatesEmailTexts = emailInfo.
            sc.SendEmails(emailSender, locator.Main.Emails);
        }

        /// <summary>
        /// Обработчик события "Отправить запланировано"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSend_Click(object sender, RoutedEventArgs e)
        {
            SchedulerClass sc = new SchedulerClass();
            TimeSpan tsSendTime = sc.GetSendTime(tbTimePicker.Text);
            if (tsSendTime == new TimeSpan())
            {
                MessageBox.Show("Некорректный формат даты");
                return;
            }
            DateTime dtSendDateTime = (cldSchedulDateTimes.SelectedDate ?? DateTime.Today).Add(tsSendTime);
            if (dtSendDateTime < DateTime.Now)
            {
                MessageBox.Show("Дата и время отправки писем не могут быть раньше, чем настоящее время");
                return;
            }
            EmailSendServiceClass emailSender = new EmailSendServiceClass(tbSender.SelectedText, tbSender.SelectedValue);
            //sc.SendEmails(dtSendDateTime, emailSender, (Obse<Email>)dgEmails.ItemsSource);

            // Получаем доступ к ViewModelLocator
            var locator = (ViewModelLocator)FindResource("Locator");

            sc.SendEmails(dtSendDateTime, emailSender, locator.Main.Emails);

        }

        private void TabSwitcherControl_btnNextClick(object sender, RoutedEventArgs e)
        {
            tabControl.SelectedIndex = 2;
        }

        private void TabSwitcherControl_btnPreviousClick(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
