using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MailSenderNameSpace.View
{
    /// <summary>
    /// Логика взаимодействия для SaveEmailView.xaml
    /// </summary>
    public partial class SaveEmailView : UserControl
    {
        public SaveEmailView()
        {
            InitializeComponent();
        }

        // Обработчик ошибок
        public void TextBox_Error(object sendler, ValidationErrorEventArgs args)
        {
            if (args.Action == ValidationErrorEventAction.Added)
            {
                ((Control)sendler).ToolTip = args.Error.ToString(); 
            }
            else
            {
                ((Control)sendler).ToolTip = "";
            }
        }
    }
}
