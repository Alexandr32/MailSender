using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Threading;
using System.Windows;
using System.Collections.ObjectModel;

namespace MailSenderNameSpace
{
    /// <summary>
    /// Класс-планировщик, который создает расписание, следит за его выполнением и напоминает о событиях
    /// Также помогает автоматизировать рассылку писем в соответствии с расписанием
    /// </summary>
    public class SchedulerClass
    {
        // Таймер
        DispatcherTimer timer = new DispatcherTimer();

        // Экземпляр класса, отвечающего за отправку писем
        EmailSendServiceClass emailSender;

        // Дата и время отправки
        DateTime dtSend;

        // Коллекция email-ов адресатов
        ObservableCollection<Email> emails;

        Dictionary<DateTime, string> dicDates = new Dictionary<DateTime, string>();
        public Dictionary<DateTime, string> DatesEmailTexts
        {
            get { return dicDates; }
            set
            {
                dicDates = value;
                dicDates = dicDates.OrderBy(pair => pair.Key).ToDictionary(pair =>  pair.Key, pair => pair.Value);
            }
        }

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
        //// Отправка запланированных писем
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

        /// <summary>
        /// Отправка писем сразу
        /// </summary>
        /// <param name="dtSend"></param>
        /// <param name="emailSender"></param>
        /// <param name="emails"></param>
        public void SendEmails(EmailSendServiceClass emailSender, ObservableCollection<Email> emails)
        {
            this.emailSender = emailSender;
            this.emails = emails;
            emailSender.SendMails(emails);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (dicDates.Count == 0)
            {
                timer.Stop();
                MessageBox.Show("Письма отправлены.");
            }
            else if (dicDates.Keys.First<DateTime>().ToShortTimeString() ==  DateTime.Now.ToShortTimeString())
            {
                emailSender.Body = dicDates[dicDates.Keys.First<DateTime>()];
                emailSender.Subject = $"Рассылка от { dicDates.Keys.First<DateTime>().ToShortTimeString()}";
                emailSender.SendMails(emails);
                dicDates.Remove(dicDates.Keys.First<DateTime>());
            }


            //if (dtSend.ToShortTimeString() == DateTime.Now.ToShortTimeString())
            //{
            //    emailSender.SendMails(emails);
            //    timer.Stop();
            //    MessageBox.Show("Письма отправлены.");
            //}
        }
    }
}
