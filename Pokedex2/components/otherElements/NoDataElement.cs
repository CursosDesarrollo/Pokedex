using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;

namespace Pokedex2.components.otherElements
{
    class NoDataElement
    {
        private readonly TextBlock message;
        private static NoDataElement instance = null;
        private NoDataElement()
        {
            message = new TextBlock();
            message.VerticalAlignment = VerticalAlignment.Center;
            message.HorizontalAlignment = HorizontalAlignment.Center;
            message.FontFamily = new FontFamily("Flexo");
            message.FontSize = 50;
        }

        public static NoDataElement GetObject()
        {
            if (instance is null) instance = new NoDataElement();
            return instance;
        }

        public TextBlock Message(string message)
        {
            this.message.Text = message;
            return this.message;
        }
    }
}
