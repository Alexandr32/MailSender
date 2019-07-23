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

namespace TabSwitcher
{
    /// <summary>
    /// Логика взаимодействия для UserControl1.xaml
    /// </summary>
    public partial class TabSwitcherControl : UserControl
    {
        public TabSwitcherControl()
        {
            InitializeComponent();
        }

        #region properties
        private bool bHidebtnPrevious = false; // поле, соответствующее тому, будет ли скрыта кнопка «Предыдущий»
        private bool bHideBtnNext = false; // поле, соответствующее тому, будет ли скрыта кнопка «Следующий»
        /// <summary>
        /// Свойство, соответствующее тому, будет ли скрыта кнопка «Предыдущий».
        /// Чтобы Свойство отразилось на PropertiesGrid у нашего контрола, его нужно представить именно свойством, а не полем
        /// </summary>
        public bool IsHidebtnPrevious
        {
            get { return bHidebtnPrevious; }
            set
            {
                bHidebtnPrevious = value;
                SetButtons(); // метод, который отвечает на отрисовку кнопок
            }
        } // IsHidebtnPrevious
        public bool IsHideBtnNext
        {
            get { return bHideBtnNext; }
            set
            {
                bHideBtnNext = value;
                SetButtons(); // метод, который отвечает за отрисовку кнопок
            }
        } // IsHidebtnPrevious
        private void BtnNextTruebtnPreviousFalse()
        {
            btnNext.Visibility = Visibility.Hidden;
            btnPrevious.Visibility = Visibility.Visible;
            btnPrevious.Width = 229;
            btnNext.Width = 0;
            btnPrevious.HorizontalAlignment = HorizontalAlignment.Stretch;
        }
        private void BtnPreviousTrueBtnNextFalse()
        {
            btnPrevious.Visibility = Visibility.Hidden;
            btnNext.Visibility = Visibility.Visible;
            btnNext.Width = 229;
            btnPrevious.Width = 0;
            btnNext.HorizontalAlignment = HorizontalAlignment.Stretch;
        }
        private void BtnPreviousFalseBtnNextFalse()
        {
            btnNext.Visibility = Visibility.Visible;
            btnPrevious.Visibility = Visibility.Visible;
            btnNext.Width = 115;
            btnPrevious.Width = 114;
        }
        private void BtnPreviousTrueBtnNextTrue()
        {
            btnPrevious.Visibility = Visibility.Hidden;
            btnNext.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Метод, который отвечает за отрисовку кнопок.
        /// Есть три варианта: когда обе кнопки доступны; доступна одна и недоступна
        /// вторая; обе кнопки недоступны
        /// </summary>
        private void SetButtons()
        {
            if (bHidebtnPrevious && bHideBtnNext)
                BtnPreviousTrueBtnNextTrue();
            else if (!bHideBtnNext && !bHidebtnPrevious)
                BtnPreviousFalseBtnNextFalse();
            else if (bHideBtnNext && !bHidebtnPrevious)
                BtnNextTruebtnPreviousFalse();
            else if (!bHideBtnNext && bHidebtnPrevious)
                BtnPreviousTrueBtnNextFalse();
        }
        #endregion

        /// <summary>
        /// Событие нажатия кнопки "вперед"
        /// </summary>
        public event RoutedEventHandler btnNextClick;
        /// <summary>
        /// Событие нажатия кнопки "назад"
        /// </summary>
        public event RoutedEventHandler btnPreviousClick;

        /// <summary>
        /// Обработчик кнопки "назад"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnPrevious_Click(object sender, RoutedEventArgs e)
        {
            btnPreviousClick?.Invoke(sender, e);
        }

        /// <summary>
        /// Обработчик кнопки "вперед"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            btnNextClick?.Invoke(sender, e);
        }
    }
}
