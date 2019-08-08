using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSenderNameSpace
{
    /// <summary>
    /// Класс для работы с БД
    /// </summary>
    class DBClass
    {
        private EmailsDataContext emails = new EmailsDataContext();

        /// <summary>
        /// Все данные извлеченные из БД
        /// </summary>
        public IQueryable<Email> Emails
        {
            get
            {
                return from c in emails.Email select c;
            }
        }
    }
}
