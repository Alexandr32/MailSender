using CodePasswordDLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSenderNameSpace
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
            { "alex3289btnktest@yandex.ru", CodePassword.GetPassword("by43AA9:uftuA") },
            { "alex3289btnktest2@yandex.ru", CodePassword.GetPassword("by43AA9:uftu3") }
        };
    }
}
