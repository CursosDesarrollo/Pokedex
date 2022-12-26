using Pokedex2.components;
using Pokedex2.components.optionsCrud;
using Pokedex2.components.otherElements;
using Pokedex2.components.views;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Pokedex2
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainExecution components;
        public MainWindow()
        {
            InitializeComponent();
            components = new MainExecution(Application.Current.Resources, EditButton, DeleteButton, ContainerCards);
            CardsContainer();
        }

        private void CloseApp(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MinimizeApp(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void WindowMove(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed) DragMove();
        }

        private void ListPokemon(List<Dictionary<string,object>> listData)
        {
            if (listData.Count == 0)
            {
                var elementNoData = NoDataElement.GetObject();
                ContainerCards.Children.Add(elementNoData.Message("Sin datos"));
                return;
            }
            components.CreateCards(listData);
        }

        private void CardsContainer()
        {
            ReadPokemonDb newRead = new ReadPokemonDb();
            var listData = newRead.ReadAll();
            ListPokemon(listData);
        }

        private void RebootContainer()
        {
            ContainerCards.Children.Clear();
            ContainerCards.RowDefinitions.Clear();
            ContainerCards.ColumnDefinitions.Clear();
        }

        private void RebootListElementsChange()
        {
            ElementsChange.GetObject().ListElementChange.Clear();
        }

        private void RebootButtonsEditDelete()
        {
            if (DeleteButton.Visibility == Visibility.Visible
                || EditButton.Visibility == Visibility.Visible) RebootListElementsChange();
            if (EditButton.Visibility == Visibility.Visible) EditButton.Visibility = Visibility.Hidden;
            if (DeleteButton.Visibility == Visibility.Visible) DeleteButton.Visibility = Visibility.Hidden;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RebootButtonsEditDelete();
            FormPokemon newForm = new FormPokemon();
            newForm.ShowDialog();
            RebootContainer();
            CardsContainer();
        }

        private void ChangeScroll(object sender, ScrollChangedEventArgs e)
        {
            if (e.VerticalChange == 0) return;
            var scrollViewer = (ScrollViewer)sender;
            var chanVertical = e.VerticalOffset;
            var scrollHeight = scrollViewer.ScrollableHeight;
            if (chanVertical != scrollHeight) return;
            components.CurrentRowRendering++;
            if (components.CurrentRowRendering <= components.RowsRendering) components.NextCards();
        }

        private void ChangeFind(object sender, TextChangedEventArgs e)
        {
            var inputText = (TextBox)sender;
            if (inputText.Text.Length > 0)
            {
                DropFind.Visibility = Visibility.Visible;
                return;
            }
            if (DropFind.Visibility == Visibility.Visible) DropFind.Visibility = Visibility.Collapsed;
        }

        private void DropTextFind(object sender, RoutedEventArgs e)
        {
            InputFind.Text = "";
            InputFind.Focus();
        }

        private void ExecActionFind()
        {
            RebootButtonsEditDelete();
            TextTargetFind.Text = InputFind.Text;
            TargetFind.Visibility = Visibility.Visible;
            RebootContainer();
            ReadPokemonDb newRead = new ReadPokemonDb();
            var listData = newRead.ReadFind(InputFind.Text);
            ListPokemon(listData);
        }

        private void ActionFind(object sender, RoutedEventArgs e)
        {
            if (InputFind.Text.Length == 0 || TextTargetFind.Text == InputFind.Text) return;
            ExecActionFind();
        } 

        private void DeleteTargetFind(object sender, RoutedEventArgs e)
        {
            RebootButtonsEditDelete();
            TargetFind.Visibility = Visibility.Collapsed;
            InputFind.Text = "";
            TextTargetFind.Text = "";
            InputFind.Focus();
            RebootContainer();
            CardsContainer();
        }

        private void ActionEdit(object sender, RoutedEventArgs e)
        {
            var elementChange = ElementsChange.GetObject().ListElementChange[0];
            ReadPokemonDb newRead = new ReadPokemonDb();
            var listData = newRead.ReadOne(elementChange);
            if (listData.Count == 0) return;
            var newForm = new FormPokemon();
            newForm.InitVariables(listData[0]);
            newForm.ShowDialog();
            RebootButtonsEditDelete();
            if(InputFind.Text.Length > 0)
            {
                ExecActionFind();
                return;
            }
            RebootContainer();
            CardsContainer();
        }

        private void ActionDelete(object sender, RoutedEventArgs e)
        {
            var result = DialogConfirmation.Message("Estas seguro que quieres eliminarlos?");
            if (!result) return;
            DeletePokemonDb newDelete = new DeletePokemonDb();
            newDelete.Delete();
            RebootButtonsEditDelete();
            if (InputFind.Text.Length > 0)
            {
                ExecActionFind();
                return;
            }
            RebootContainer();
            CardsContainer();
        }
    }
}
