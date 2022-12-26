using Pokedex2.components.optionsCrud;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;

namespace Pokedex2.components.cardsPokemon
{
    class CardPokemon
    {
        private readonly Border borderCard;
        private readonly Grid gridCard;
        private readonly StackPanel mainCard;
        private readonly Border decorationCard;
        private readonly Border borderImage;
        private readonly System.Windows.Controls.Image imagePokemon;
        private readonly TextBlock textId;
        private readonly TextBlock namePokemon;
        private readonly StackPanel containerTargets;
        private readonly CheckBox checkId;
        private readonly Dictionary<string, SolidColorBrush[]> colorsTypes;
        private readonly string idPokemon;
        private readonly ResourceDictionary globalStyles;
        private readonly List<string> elementsChange;
        private readonly InteractionButtons interactionButtons;

        public CardPokemon(
            int row, 
            int column, 
            ResourceDictionary gS, 
            string url, 
            string id, 
            string name,
            List<string> ElementsChange,
            InteractionButtons interactionButtons)
        {
            this.interactionButtons = interactionButtons;
            globalStyles = gS;
            idPokemon = id;
            elementsChange = ElementsChange;
            colorsTypes = ColorsTarget.GetObject().colorsTypes;
            borderCard = ConfigBorderCard(row, column);
            gridCard = ConfigGridCard();
            decorationCard = ConfigDecorationCard();
            mainCard = ConfigMainCard();
            borderImage = ConfigBorderImage();
            imagePokemon = ConfigImagePokemon(url);
            textId = ConfigIdAndTittle("N.º " + idPokemon, "TextId");
            namePokemon = ConfigIdAndTittle(name, "NamePokemon");
            checkId = ConfigCheckId();
            containerTargets = new StackPanel();
        }

        public Border Create(List<string> typesPokemon)
        {
            containerTargets.Orientation = Orientation.Horizontal;
            for (int i = 0; i < typesPokemon.Count; i++)
            {
                containerTargets.Children.Add(CreateTarget(typesPokemon[i], i));
            }
            borderImage.Child = imagePokemon;
            NameScope.SetNameScope(gridCard, new NameScope());
            mainCard.Children.Add(borderImage);
            mainCard.Children.Add(textId);
            mainCard.Children.Add(namePokemon);
            mainCard.Children.Add(containerTargets);
            mainCard.Children.Add(checkId);
            gridCard.Children.Add(decorationCard);
            gridCard.Children.Add(mainCard);
            gridCard.RegisterName(checkId.Name, checkId);
            gridCard.MouseLeftButtonDown += ActivateCheck;
            borderCard.Child = gridCard;
            return borderCard;
        }

        private Border CreateTarget(string typePokemon, int position)
        {
            Border newBorder = new Border();
            TextBlock newText = new TextBlock();
            char[] upperText = typePokemon.ToCharArray();
            newBorder.Background = colorsTypes[typePokemon][0];
            newBorder.Style = (Style)globalStyles["TargetProperty"];
            if (position > 0) newBorder.Margin = new Thickness(10, 0, 0, 0);
            upperText[0] = Char.ToUpper(upperText[0]);
            newText.Text = new String(upperText);
            newText.Foreground = colorsTypes[typePokemon][1];
            newBorder.Child = newText;
            return newBorder;
        }

        private Border ConfigBorderCard(int row, int column)
        {
            Border newBorder = new Border();
            newBorder.Style = (Style)globalStyles["BorderCardPokemon"];
            newBorder.Name = "Borde" + idPokemon;
            Grid.SetColumn(newBorder, column);
            Grid.SetRow(newBorder, row);
            return newBorder;
        }

        private Grid ConfigGridCard()
        {
            Grid newGrid = new Grid();
            newGrid.Style = (Style)globalStyles["GridCard"];
            newGrid.Cursor = Cursors.Hand;
            return newGrid;
        }

        private StackPanel ConfigMainCard()
        {
            StackPanel newPanel = new StackPanel();
            newPanel.Style = (Style)globalStyles["CardPokemon"];
            return newPanel;
        }

        private Border ConfigDecorationCard()
        {
            Border newBorder = new Border();
            newBorder.CornerRadius = new CornerRadius(10);
            DropShadowEffect newShadow = new DropShadowEffect();
            newShadow.Color = Color.FromRgb(27, 130, 177);
            newShadow.ShadowDepth = 8;
            newShadow.Opacity = 0.2;
            newBorder.Effect = newShadow;
            Style newStyle = new Style();
            newStyle.TargetType = typeof(Border);
            DataTrigger newTrigger = new DataTrigger();
            Binding newBinding = new Binding();
            newBinding.ElementName = "Pokemon" + idPokemon;
            newBinding.Path = new PropertyPath(CheckBox.IsCheckedProperty);
            newTrigger.Binding = newBinding;
            newTrigger.Value = true;
            Setter newSetter = new Setter();
            newSetter.Property = Border.BackgroundProperty;
            newSetter.Value = Brushes.White;
            newTrigger.Setters.Add(newSetter);
            newStyle.Triggers.Add(newTrigger);
            newBorder.Style = newStyle;
            return newBorder;
        }

        private Border ConfigBorderImage()
        {
            Border newBorder = new Border();
            newBorder.Style = (Style)globalStyles["BorderImage"];
            newBorder.CornerRadius = new CornerRadius(20);
            return newBorder;
        }

        private System.Windows.Controls.Image ConfigImagePokemon(string url)
        {
            System.Windows.Controls.Image newImage = new System.Windows.Controls.Image();
            newImage.Style = (Style)globalStyles["ImagePokemon"];
            BitmapImage newMap = new BitmapImage();
            newMap.BeginInit();
            newMap.UriSource = new Uri(url);
            newMap.EndInit();
            newImage.Source = newMap;
            return newImage;
        }

        private TextBlock ConfigIdAndTittle(string name, string style)
        {
            TextBlock newText = new TextBlock();
            newText.Style = (Style)globalStyles[style];
            newText.Text = name;
            return newText;
        }

        private CheckBox ConfigCheckId()
        {
            CheckBox newCheck = new CheckBox();
            newCheck.Name = "Pokemon" + idPokemon;
            newCheck.Visibility = Visibility.Collapsed;
            return newCheck;
        }

        private void ValidateCheck(bool isChecked )
        {
            checkId.IsChecked = !isChecked;
            if(isChecked) elementsChange.Remove(idPokemon);
            else elementsChange.Add(idPokemon);
        }

        private void ActivateCheck(object sender, RoutedEventArgs e)
        {
            ValidateCheck((bool)checkId.IsChecked);
            Console.WriteLine(elementsChange.Count);
            if (elementsChange.Count == 0)
            {
                interactionButtons.ChangeVisibilityDelete();
                interactionButtons.ChangeVisibilityEdit();
                return;
            }
            interactionButtons.ChangeVisibilityDelete(true);
            if (elementsChange.Count < 2) interactionButtons.ChangeVisibilityEdit(true);
            else interactionButtons.ChangeVisibilityEdit();
        }
    }
}
