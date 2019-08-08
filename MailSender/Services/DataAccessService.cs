using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSenderNameSpace.Services
{
    /// <summary>
    /// Интерфейс для предотавления данных
    /// </summary>
    public interface IDataAccessService
    {
        ObservableCollection<Email> GetEmails();

        int CreateEmail(Email email);
    }

    public class DataAccessService : IDataAccessService
    {
        // Контекст данных получаемый из БД
        EmailsDataContext context;
        public DataAccessService()
        {
            // EmailsDataContext класс сгенерированный БД
            context = new EmailsDataContext();
        }

        /// <summary>
        /// Создает новую запись
        /// </summary>
        /// <param name="email"></param>
        /// <returns>Возвращает Id письма.</returns>
        public int CreateEmail(Email email)
        {
            context.Email.InsertOnSubmit(email);
            context.SubmitChanges();
            return email.Id;
        }

        /// <summary>
        /// Запрос почт из БД
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<Email> GetEmails()
        {
            ObservableCollection<Email> Emails = new ObservableCollection<Email>();
            foreach (var item in context.Email)
            {
                Emails.Add(item);
            }
            return Emails;
        }
    }
}
