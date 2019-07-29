using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfTestMailSender
{
    /// <summary>
    /// Класс для отправки писем
    /// </summary>
    class EmailSendServiceClass
    {

        #region vars
        // email, c которого будет рассылаться почта
        private string strLogin;
        // пароль к email, с которого будет рассылаться почта
        private string strPassword;
        // smtp-server
        private string strSmtp = "smtp.yandex.ru";
        // порт для smtp-server
        private int iSmtpPort = 25;
        // текст письма для отправки
        public string StrBody { get; set; }
        // тема письма для отправки
        private string strSubject;
        #endregion


        public EmailSendServiceClass(string sLogin, string sPassword)
        {
            strLogin = sLogin;
            strPassword = sPassword;
        }

        /// <summary>
        /// Отправка письма
        /// </summary>
        public void SendEmail(string mail, string name)
        {

            using (MailMessage mm = new MailMessage(strLogin, mail))
            {
                mm.Subject = strSubject;
                mm.Body = StrBody;
                mm.IsBodyHtml = false;
                SmtpClient sc = new SmtpClient(strSmtp, iSmtpPort)
                {
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(strLogin, strPassword)
                };

                try
                {
                    sc.Send(mm);
                }
                catch (Exception ex)
                {
                    
                    MessageBox.Show("Невозможно отправить письмо " + ex.ToString());
                }

                MessageBox.Show("Работа завершена.");
            }

        }

        public void SendMails(ObservableCollection<Email> emails)
        {
            foreach (Email email in emails)
            {
                SendEmail(email.Value, email.Name);
            }
        }
    }
}
