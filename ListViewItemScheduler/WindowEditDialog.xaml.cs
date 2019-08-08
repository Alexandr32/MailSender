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
using System.Windows.Shapes;

namespace ListViewItemScheduler
{
    /// <summary>
    /// Логика взаимодействия для WindowEditDialog.xaml
    /// </summary>
    public partial class WindowEditDialog : Window
    {
        public WindowEditDialog()
        {
            InitializeComponent();
            DataContext = this;
        }

        public string TextMail { get; set; }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            TextMail = string.Empty;
            Close();
        }
    }
}
