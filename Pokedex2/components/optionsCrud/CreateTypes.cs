using Pokedex2.components.cardsPokemon;
using Pokedex2.components.typesPokemon;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Pokedex2.components.optionsCrud
{
    class CreateTypes
    {
        private CreateTypes() {}

        private static Border AddType(SolidColorBrush backgroundLabel, SolidColorBrush foregroundText, string namePokemon,double marginLeft = 0)
        {
            Border borderType = ConfigBorderType();
            Label labelType = ConfigLabelType(backgroundLabel);
            TextBlock textType = ConfigTextType(foregroundText, namePokemon);
            labelType.Content = textType;
            borderType.Margin = new Thickness(marginLeft, 0,0,0);
            borderType.Child = labelType;
            return borderType;
        }

        private static Border ConfigBorderType()
        {
            Border newBorder = new Border();
            newBorder.BorderBrush = ColorsTarget.ConvertColor(164, 164, 164);
            newBorder.CornerRadius = new CornerRadius(2);
            newBorder.BorderThickness = new Thickness(1);
            newBorder.Background = ColorsTarget.ConvertColor(164, 164, 164);
            return newBorder;
        }

        private static Label ConfigLabelType(SolidColorBrush background)
        {
            Label newLabel = new Label();
            newLabel.Background = background;
            newLabel.HorizontalContentAlignment = HorizontalAlignment.Center;
            newLabel.Width = 58;
            Style newStyle = new Style();
            newStyle.TargetType = typeof(Border);
            Setter newSetter = new Setter();
            newSetter.Property = Border.CornerRadiusProperty;
            newSetter.Value = new CornerRadius(2);
            newLabel.Resources.Add(typeof(Border), newStyle);
            return newLabel;
        }

        private static TextBlock ConfigTextType(SolidColorBrush foreground,string text)
        {
            TextBlock newText = new TextBlock();
            newText.Foreground = foreground;
            newText.FontFamily = new FontFamily("Flexo");
            newText.FontSize = 10;
            newText.Text = text;
            return newText;
        }

        private static string FirstLetterUpperCase(string text)
        {
            char[] upperText = text.ToCharArray();
            upperText[0] = Char.ToUpper(upperText[0]);
            return new String(upperText);
        }

        public static void CreateTargets(StackPanel container)
        {
            List<string> types = TypesPokemon.GetObject().types;
            Dictionary<string, SolidColorBrush[]> colorTypes = ColorsTarget.GetObject().colorsTypes;
            container.Children.Clear();
            for (int i=0;i<types.Count;i++)
            {
                string val = types[i], 
                    text = FirstLetterUpperCase(val);
                SolidColorBrush background = colorTypes[val][0],
                    foreground = colorTypes[val][1];
                double margin = i == 1 ? 10 : 0;
                Border newBorder = AddType(background, foreground, text, margin);
                container.Children.Add(newBorder);
            }
        }
    }
}
