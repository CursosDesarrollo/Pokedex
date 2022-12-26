using Pokedex2.components.typesPokemon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Pokedex2.components.views
{
    /// <summary>
    /// Lógica de interacción para FormTypes.xaml
    /// </summary>
    public partial class FormTypes : Window
    {
        private readonly List<String> typesPokemon;
        private readonly List<String> cloneTypesPokemon;
        private readonly TextBlock errorTypes;
        public FormTypes(TextBlock errorTypes)
        {
            InitializeComponent();
            typesPokemon = TypesPokemon.GetObject().types;
            cloneTypesPokemon = TypesPokemon.GetObject().cloneTypes;
            this.errorTypes = errorTypes;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ValidateTypes newValidate = new ValidateTypes();
            newValidate.Validate(this);
            if (errorTypes.Text.Length == 0) return;
            errorTypes.Text = "";
            errorTypes.Visibility = Visibility.Collapsed;
        }

        private void HandleCheck(object sender, RoutedEventArgs e)
        {
            CheckBox newCheck = (CheckBox)sender;
            if ((bool)newCheck.IsChecked) cloneTypesPokemon.Add(newCheck.Name);
            else cloneTypesPokemon.Remove(newCheck.Name);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (cloneTypesPokemon.Count > 0) cloneTypesPokemon.Clear();
            this.Close();
        }
    }
}
