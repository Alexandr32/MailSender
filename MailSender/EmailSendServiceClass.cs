using System;
using System.Collections.Generic;
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
        private string strBody;
        // тема письма для отправки
        private string strSubject;
        #endregion

        SendEndWindow sendEndWindow;

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
                mm.Body = "Hello world!";
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
                    SendEndWindow windowError = new SendEndWindow();
                    windowError.ShowMessage("Невозможно отправить письмо " + ex.ToString());
                }

                sendEndWindow.ShowMessage("Работа завершена.");
            }

        }

        public void SendMails(IQueryable<Email> emails)
        {
            foreach (Email email in emails)
            {
                SendEmail(email.Value, email.Name);
            }
        }
    }
}
