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

namespace MyToolBar
{
    /// <summary>
    /// Логика взаимодействия для UserControl1.xaml
    /// </summary>
    public partial class ToolBarControl : UserControl
    {
        public ToolBarControl()
        {
            InitializeComponent();
        }

        string textLabel;
        /// <summary>
        /// Св-во заголовка
        /// </summary>
        public string TextLabel
        {
            get => textLabel;
            set
            {
                textLabel = value;
                label.Content = textLabel;
            }
        }

        Dictionary<string, string> comboBoxSenders;
        public Dictionary<string, string> ComboBoxSenders
        {
            get => comboBoxSenders;
            set
            {
                comboBoxSenders = value;
                comboBox.ItemsSource = comboBoxSenders;
                comboBox.DisplayMemberPath = "Key";
                comboBox.SelectedValuePath = "Value";
            }
        }

        Dictionary<string, int> comboBoxSmtpClient;
        public Dictionary<string, int> ComboBoxSmtpClient
        {
            get => comboBoxSmtpClient;
            set
            {
                comboBoxSmtpClient = value;
                comboBox.ItemsSource = comboBoxSmtpClient;
                comboBox.DisplayMemberPath = "Key";
                comboBox.SelectedValuePath = "Value";
            }
        }

        /// <summary>
        /// Выбраный текст comboBox
        /// </summary>
        public string SelectedText
        {
            get => comboBox.Text;
        }

        /// <summary>
        /// Выбраное значение comboBox
        /// </summary>
        public string SelectedValue
        {
            get => comboBox.SelectedValue.ToString();
        }

        bool isVisibalComboBox = true;
        /// <summary>
        /// Св-во отображения ComboBox
        /// </summary>
        public bool IsVisibalComboBox
        {
            get => isVisibalComboBox;
            set
            {
                isVisibalComboBox = value;
                if (isVisibalComboBox)
                {
                    comboBox.Visibility = Visibility.Visible;
                    comboBox.Width = 270;
                }
                else
                {
                    comboBox.Visibility = Visibility.Hidden;
                    comboBox.Width = 0;
                }
            }
        }

        public event RoutedEventHandler btnNextAdd;
        public event RoutedEventHandler btnNextEdit;
        public event RoutedEventHandler btnNextDelet;

        private void BtnAddSender_Click(object sender, RoutedEventArgs e)
        {
            btnNextAdd?.Invoke(sender, e);
        }

        private void BtnEditSender_Click(object sender, RoutedEventArgs e)
        {
            btnNextEdit?.Invoke(sender, e);
        }

        private void BtnDeleteSender_Click(object sender, RoutedEventArgs e)
        {
            btnNextDelet?.Invoke(sender, e);
        }
    }
}
