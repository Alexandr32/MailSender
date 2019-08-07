using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MailSenderNameSpace
{
    /// <summary>
    /// Класс для отправки писем
    /// </summary>
    public class EmailSendServiceClass
    {

        #region vars
        // email, c которого будет рассылаться почта
        private string strLogin;
        // пароль к email, с которого будет рассылаться почта
        private string strPassword;
        // smtp-server
        public string StrSmtp { get; set; }
        // порт для smtp-server
        public int ISmtpPort { get; set; }
        // текст письма для отправки
        public string Body { get; set; }
        // тема письма для отправки
        public string Subject { get; set; }
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
                mm.Subject = Subject;
                mm.Body = Body;
                mm.IsBodyHtml = false;

                SmtpClient sc = new SmtpClient(StrSmtp, ISmtpPort)
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

                    MessageBox.Show("Невозможно отправить письмо: " + ex.ToString());
                }


                MessageBox.Show("Работа завершена.");
            }

        }

        /// <summary>
        /// Отправка нескольких писем
        /// </summary>
        /// <param name="emails"></param>
        public void SendMails(ObservableCollection<Email> emails)
        {
            foreach (Email email in emails)
            {
                SendEmail(email.Value, email.Name);
            }
        }
    }
}
