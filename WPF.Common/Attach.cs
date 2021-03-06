using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WPF.Common
{
    public class Attach
    {
        public static readonly DependencyProperty SelectAllOnEntry = DependencyProperty.RegisterAttached
        (
            "SelectAllOnEntry",
            typeof(bool),
            typeof(Attach),
            new FrameworkPropertyMetadata
            {
                DefaultValue = false,
                PropertyChangedCallback = OnSelectAllOnEntryChanged,
            }
        );

        public static bool GetSelectAllOnEntry(TextBox textBox)
        {
            return textBox.GetValue(SelectAllOnEntry) is bool b && b;
        }

        public static void SetSelectAllOnEntry(TextBox textBox, bool value)
        {
            textBox.SetValue(SelectAllOnEntry, value);
        }

        private static void OnSelectAllOnEntryChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not TextBox textbox)
                return;
            textbox.GotFocus += Textbox_GotKeyboardFocus;
            textbox.PreviewMouseDown += IgnoreMouse;
        }

        private static void IgnoreMouse(object sender, MouseButtonEventArgs e)
        {
            if (sender is not TextBox textbox)
                return;

            if (!textbox.IsKeyboardFocused)
            {
                textbox.Focus();
                e.Handled = true;
            }
        }

        private static void Textbox_GotKeyboardFocus(object sender, RoutedEventArgs e)
        {
            if (sender is not TextBox textBox)
                return;

            textBox.SelectAll();
        }
    }
}
