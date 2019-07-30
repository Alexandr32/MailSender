using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTestMailSender
{

    /// <summary>
    /// Класс с данными необходимыми для подключения к серверу
    /// </summary>
    public static class DataMailSender
    {
        /// <summary>
        /// Почта отправителя
        /// </summary>
        public static string Sender = "alex3289btnktest@yandex.ru";

        /// <summary>
        /// Протокол SMTP
        /// </summary>
        public static string SmtpClient = "smtp.yandex.ru";

        /// <summary>
        /// Порт почты
        /// </summary>
        public static int Port = 25;
    }
}
