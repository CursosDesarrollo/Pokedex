using System;
using System.Windows;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex2.components.otherElements
{
    class DialogConfirmation
    {
        public static bool Message(string messageBoxText)
        {
            string caption = "Cuadro de confirmacion";
            MessageBoxButton button = MessageBoxButton.YesNoCancel;
            MessageBoxImage icon = MessageBoxImage.Warning;
            var result = MessageBox.Show(messageBoxText, caption, button, icon);
            return MessageBoxResult.Yes == result;
        }
    }
}
