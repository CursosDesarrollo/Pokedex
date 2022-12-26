using Pokedex2.components.cardsPokemon;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Pokedex2.components
{
    class MainExecution
    {
        private readonly ResourceDictionary globalStyles;
        private readonly Button editButton;
        private readonly Button deleteButton;
        private readonly Grid mainGrid;
        public int RowsRendering { get; set; }
        public int CurrentRowRendering { get; set; }
        public GenerateCards CurrentObject { get; set; }
        public MainExecution(ResourceDictionary gS, Button editButton, Button deleteButton, Grid mainGrid)
        {
            globalStyles = gS;
            this.editButton = editButton;
            this.deleteButton = deleteButton;
            this.mainGrid = mainGrid;
        }

        public void NextCards()
        {
            var currentRow = CurrentObject.RenderingData[CurrentRowRendering];
            List<Border> cards = currentRow["elements"] as List<Border>;
            List<RowDefinition> rows = currentRow["rows"] as List<RowDefinition>;
            rows.ForEach((row) => mainGrid.RowDefinitions.Add(row));
            cards.ForEach((card) => mainGrid.Children.Add(card));
        }

        public void CreateCards(List<Dictionary<string, object>> data)
        {
            GenerateCards generateCards = new GenerateCards(mainGrid, globalStyles, editButton, deleteButton);
            generateCards.CreateCards(data);
            RowsRendering = generateCards.RenderingData.Count - 1;
            CurrentRowRendering = 0;
            CurrentObject = generateCards;
            NextCards();
        }
    }
}
