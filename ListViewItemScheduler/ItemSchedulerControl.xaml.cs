using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace ListViewItemScheduler
{
    /// <summary>
    /// Логика взаимодействия для ItemSchedulerControl.xaml
    /// </summary>
    public partial class ItemSchedulerControl : UserControl
    {
        ObservableCollection<ItemSchedulerControl> scheduledMail;

        public ItemSchedulerControl(ObservableCollection<ItemSchedulerControl> scheduledMail)
        {
            this.scheduledMail = scheduledMail;
            InitializeComponent();
        }

        string textMail;
        public string TextMail
        {
            get => textMail;
            set => textMail = value;
        }

        string time;
        public string Time
        {
            get => time;
            set => time = value;
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            scheduledMail.Remove(this);
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            WindowEditDialog dialog = new WindowEditDialog();
            dialog.ShowDialog();
            if (dialog.DialogResult == true)
            {
                TextMail = dialog.TextMail;
            }
        }
    }
}
