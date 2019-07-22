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

        // Возвращает список почт от имени которых можно выполнить отправку
        private static Dictionary<string, string> dicSenders = new Dictionary<string, string>()
        {
            { "79257443993@yandex.ru", CodePassword.GetPassword("1234l;i") },
            { "sok74@yandex.ru", CodePassword.GetPassword(";liq34tjk") }
        };
    }
}
