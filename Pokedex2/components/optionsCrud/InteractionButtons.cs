using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Pokedex2.components.optionsCrud
{
    class InteractionButtons
    {
        public Button DeleteButton { get; }
        public Button EditButton { get; }

        public InteractionButtons(Button deleteButton, Button editButton) 
        {
            this.DeleteButton = deleteButton;
            this.EditButton = editButton;
        }

        public void ChangeVisibilityDelete (bool visibility = false)
        {
            if(visibility && (DeleteButton.Visibility == Visibility.Hidden)) DeleteButton.Visibility = Visibility.Visible;
            if (!visibility && (DeleteButton.Visibility == Visibility.Visible)) DeleteButton.Visibility = Visibility.Hidden;
        }

        public void ChangeVisibilityEdit(bool visibility = false)
        {
            if (visibility && (EditButton.Visibility == Visibility.Hidden)) EditButton.Visibility = Visibility.Visible;
            if (!visibility && (EditButton.Visibility == Visibility.Visible)) EditButton.Visibility = Visibility.Hidden;
        }
    }
}
