using Pokedex2.components.optionsCrud;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Pokedex2.components.cardsPokemon
{
    class GenerateCards
    {
        private readonly Grid mainGrid;
        private readonly ResourceDictionary globalStyles;
        public List<Dictionary<String, object>> RenderingData { get; }
        public InteractionButtons InteractionDeleteEdit { get; }
        public List<string> ListElementsChange { get; }
        public GenerateCards(Grid mG, ResourceDictionary gS, Button editButton, Button deleteButton)
        {
            InteractionDeleteEdit = new InteractionButtons(deleteButton, editButton);
            mainGrid = mG;
            RenderingData = new List<Dictionary<string, object>>();
            ConfigContainerCards();
            globalStyles = gS;
            ListElementsChange = ElementsChange.GetObject().ListElementChange;
        }

        private void ConfigContainerCards()
        {
            mainGrid.ColumnDefinitions.Add(new ColumnDefinition());
            mainGrid.ColumnDefinitions.Add(new ColumnDefinition());
            mainGrid.ColumnDefinitions.Add(new ColumnDefinition());
        }

        private List<List<Border>> ArrayCards(int countTotal)
        {
            List<List<Border>> newArray = new List<List<Border>>();
            for (int i = 0; i < countTotal; i++) newArray.Add(new List<Border>());
            return newArray;
        }

        private List<List<RowDefinition>> ArrayRows(int rows, int totalLoads)
        {
            int positionRowsArray = 0;
            List<List<RowDefinition>> newArray = new List<List<RowDefinition>>();
            for (int i = 0; i < totalLoads; i++) newArray.Add(new List<RowDefinition>());
            for (int i = 0; i < rows; i++)
            {
                RowDefinition newRow = new RowDefinition();
                newRow.Height = GridLength.Auto;
                newArray[positionRowsArray].Add(newRow);
                if (newArray[positionRowsArray].Count == 3) positionRowsArray++;
            }
            return newArray;
        }

        private Border NewCard(Dictionary<String, object> data,int row, int column)
        {
            CardPokemon newCard = new CardPokemon(
                row,
                column,
                globalStyles,
                (string)data["url"],
                (string)data["id"],
                (string)data["name"],
                ListElementsChange,
                InteractionDeleteEdit
                );
            Border newBorder = newCard.Create((List<string>)data["types"]);
            return newBorder;
        }

        private void AddDictionary (List<RowDefinition> rows, List<Border> cards)
        {
            Dictionary<String, object> newObject = new Dictionary<string, object>
                {
                    { "rows", rows },
                    { "elements", cards }
                };
            RenderingData.Add(newObject);
        }

        public void CreateCards(List<Dictionary<String, object>> data)
        {
            int countRows = (int)Math.Ceiling((decimal)data.Count / 3),
                countTotal = (int)Math.Ceiling((decimal)countRows / 3);
            List<List<Border>> cards = ArrayCards(countTotal);
            List<List<RowDefinition>> rowsArray = ArrayRows(countRows, countTotal);
            int positionColumn = 0;
            for (int i = 1; i <= data.Count; i++)
            {
                int rowPosition = (int)Math.Ceiling((decimal)i / 3) - 1, 
                    positionList = (int)Math.Ceiling((decimal)i / 9) - 1;
                Border newBorder = NewCard(data[i - 1], rowPosition, positionColumn);
                positionColumn++;
                if (positionColumn == 3) positionColumn = 0;
                cards[positionList].Add(newBorder);
            }
            for (int i = 0; i < countTotal; i++) AddDictionary(rowsArray[i], cards[i]);
        }
    }
}
