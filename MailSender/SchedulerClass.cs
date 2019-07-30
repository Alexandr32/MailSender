using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Windows;
using System.Collections.ObjectModel;

namespace WpfTestMailSender
{
    /// <summary>
    /// Класс-планировщик, который создает расписание, следит за его выполнением и напоминает о событиях
    /// Также помогает автоматизировать рассылку писем в соответствии с расписанием
    /// </summary>
    class SchedulerClass
    {
        // Таймер
        DispatcherTimer timer = new DispatcherTimer();
        
        // Экземпляр класса, отвечающего за отправку писем
        EmailSendServiceClass emailSender;
        
        // Дата и время отправки
        DateTime dtSend;

        // Коллекция email-ов адресатов
        ObservableCollection<Email> emails;
        
        /// <summary>
        /// Метод, который превращает строку из текстбокса tbTimePicker в TimeSpan
        /// </summary>
        /// <param name="strSendTime"></param>
        /// <returns></returns>
        public TimeSpan GetSendTime(string strSendTime)
        {
            TimeSpan tsSendTime = new TimeSpan();
            try
            {
                tsSendTime = TimeSpan.Parse(strSendTime);
            }
            catch { }
            return tsSendTime;
        }


        /// <summary>
        //// Непосредственно отправка писем
        /// </summary>
        /// <param name="dtSend"></param>
        /// <param name="emailSender"></param>
        /// <param name="emails"></param>
        public void SendEmails(DateTime dtSend, EmailSendServiceClass emailSender, ObservableCollection<Email> emails)
        {
            // Экземпляр класса, отвечающего за отправку писем, присваиваем
            this.emailSender = emailSender; 
            this.dtSend = dtSend;
            this.emails = emails;
            timer.Tick += Timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (dtSend.ToShortTimeString() == DateTime.Now.ToShortTimeString())
            {
                emailSender.SendMails(emails);
                timer.Stop();
                MessageBox.Show("Письма отправлены.");
            }
        }
    }
}
