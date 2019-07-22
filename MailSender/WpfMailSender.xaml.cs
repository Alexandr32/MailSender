﻿using MahApps.Metro.Controls;
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

namespace WpfTestMailSender
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class WpfMailSender : MetroWindow
    {

        DBClass db;

        public WpfMailSender()
        {
            InitializeComponent();

            // Передача данных почт отправителей
            cbSenderSelect.ItemsSource = VariablesClass.Senders;
            cbSenderSelect.DisplayMemberPath = "Key";
            cbSenderSelect.SelectedValuePath = "Value";

            db = new DBClass();
            dgEmails.ItemsSource = db.Emails;
        }

        private void BtnSendEmail_Click(object sender, RoutedEventArgs e)
        {
            //EmailSendServiceClass emailSendServiceClass = new EmailSendServiceClass
            //{
            //    StrPassword = passwordBox.Password
            //};
            //emailSendServiceClass.SendEmail();
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
            // cbSenderSelect - элемент выбор отправителя
            string strLogin = cbSenderSelect.Text;
            string strPassword = cbSenderSelect.SelectedValue.ToString();
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

            EmailSendServiceClass emailSender = new EmailSendServiceClass(strLogin, strPassword);

            // Передаем данные из Grid со списком адресов
            emailSender.SendMails((IQueryable<Email>)dgEmails.ItemsSource);
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
            EmailSendServiceClass emailSender = new
            EmailSendServiceClass(cbSenderSelect.Text, cbSenderSelect.SelectedValue.ToString());
            sc.SendEmails(dtSendDateTime, emailSender, (IQueryable<Email>)dgEmails.ItemsSource);


        }

        private void TabSwitcherControl_btnNextClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("btnNextClick");
        }

        private void TabSwitcherControl_btnPreviousClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("btnPreviousClick");
        }
    }
}