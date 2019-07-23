using CodePasswordDLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTestMailSender
{
    /// <summary>
    /// Хранит возможных отправителей электронных писем
    /// </summary>
    public static class VariablesClass
    {
        public static Dictionary<string, string> Senders
        {
            get { return dicSenders; }
        }

        public static Dictionary<string, int> SmtpClient
        {
            get { return listSmtpClient; }
        }

        // Возвращает список почт от имени которых можно выполнить отправку
        private static Dictionary<string, int> listSmtpClient = new Dictionary<string, int>()
        {
            { "smtp.yandex.ru", 25 },
            { "smtp.gmail.com", 58 },
            { "smtp.mail.ru", 25 }
        };

        // Возвращает список почт от имени которых можно выполнить отправку
        private static Dictionary<string, string> dicSenders = new Dictionary<string, string>()
        {
            { "79257443993@yandex.ru", CodePassword.GetPassword("1234l;i") },
            { "sok74@yandex.ru", CodePassword.GetPassword(";liq34tjk") }
        };
    }
}
