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
        /// <summary>
        /// /Список email'ов кому мы отправляем письмо
        /// </summary>
        List<string> listStrMails;

        /// <summary>
        /// Пароль от почты
        /// </summary>
        public string StrPassword { set; private get; }

        SendEndWindow sendEndWindow;

        public EmailSendServiceClass()
        {
            listStrMails = new List<string> { "alex3289@gmail.com" , "alex3289btnk@yandex.ru" };
            sendEndWindow = new SendEndWindow();
            
        }

        /// <summary>
        /// Отправка писем
        /// </summary>
        public void SendEmail()
        {
            foreach (string mail in listStrMails)
            {
                // Используем using, чтобы гарантированно удалить объект MailMessage после использования
                using (MailMessage mm = new MailMessage(DataMailSender.Sender, mail))
                {
                    // Формируем письмо
                    // Заголовок письма
                    mm.Subject = "Привет из C#";
                    // Тело письма
                    mm.Body = "Hello, world!";
                    mm.IsBodyHtml = false; // Не используем html в теле письма
                                           // Авторизуемся на smtp-сервере и отправляем письмо
                                           // Оператор using гарантирует вызов метода Dispose, даже если при вызове
                                           // методов в объекте происходит исключение.

                    using (SmtpClient sc = new SmtpClient(DataMailSender.SmtpClient, DataMailSender.Port))
                    {
                        sc.EnableSsl = true;
                        sc.Credentials = new NetworkCredential(DataMailSender.Sender,  StrPassword);
                        try
                        {
                            sc.Send(mm);
                        }
                        catch (Exception ex)
                        {

                            SendEndWindow windowError = new SendEndWindow();
                            windowError.ShowMessage("Невозможно отправить письмо " + ex.ToString());
                            
                        }
                    }
                }
            }
            
            sendEndWindow.ShowMessage("Работа завершена.");
        }
    }
}
