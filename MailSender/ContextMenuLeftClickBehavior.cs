﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Data;

namespace WpfTestMailSender
{
    /// <summary>
    /// Класс для вызова контекстного меню правой клавишей мышки
    /// </summary>
    public class ContextMenuLeftClickBehavior
    {
        /// <summary>
        /// Запрос состояние клика левой клавиши
        /// </summary>
        public static bool GetIsLeftClickEnabled(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsLeftClickEnabledProperty);
        }

        /// <summary>
        ///  Установливает состояние левого клика
        /// </summary>
        public static void SetIsLeftClickEnabled(DependencyObject obj, bool value)
        {
            obj.SetValue(IsLeftClickEnabledProperty, value);
        }

        /// <summary>
        /// Создается свойство
        /// </summary>
        public static readonly DependencyProperty IsLeftClickEnabledProperty = DependencyProperty.RegisterAttached(
            "IsLeftClickEnabled",
            typeof(bool),
            typeof(ContextMenuLeftClickBehavior),
            new UIPropertyMetadata(false, OnIsLeftClickEnabledChanged));

        /// <summary>
        /// Обработчик нажатия
        /// </summary>
        private static void OnIsLeftClickEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            // Проверка является ли нажатый элемент кнопкой
            if (sender is UIElement uiElement)
            {
                // Текущее состояние свойства
                bool IsEnabled = e.NewValue is bool;

                if (IsEnabled)
                {
                    if (uiElement is ButtonBase)
                    {
                        // Приводим элемент к типу кнопки и регестрируем событие
                        ((ButtonBase)uiElement).Click += OnMouseLeftButtonUp;
                    }
                    else
                    {
                        uiElement.MouseLeftButtonUp += OnMouseLeftButtonUp;
                    }
                }
                else
                {
                    if (uiElement is ButtonBase)
                        ((ButtonBase)uiElement).Click -= OnMouseLeftButtonUp;
                    else
                        uiElement.MouseLeftButtonUp -= OnMouseLeftButtonUp;
                }
            }
        }

        private static void OnMouseLeftButtonUp(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement fe)
            {
                // если мы используем привязку в нашем контекстном меню, то его DataContext не будет установлен, когда мы показываем меню левой кнопкой мыши
                // (кажется, что настройка DataContext для ContextMenu жестко закодирована в WPF, когда пользователь щелкает правой кнопкой мыши элемент управления, хотя я не уверен)
                // поэтому мы должны установить ContextMenu.DataContext вручную здесь
                if (fe.ContextMenu.DataContext == null)
                {
                    fe.ContextMenu.SetBinding(FrameworkElement.DataContextProperty, new Binding { Source = fe.DataContext });
                }

                // Открываем меню
                fe.ContextMenu.IsOpen = true;
            }
        }
    }
}