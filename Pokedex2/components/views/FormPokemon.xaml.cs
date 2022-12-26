using Pokedex2.components.optionsCrud;
using Pokedex2.components.typesPokemon;
using Pokedex2.components.otherElements;
using Pokedex2.models;
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
    /// Lógica de interacción para FormPokemon.xaml
    /// </summary>
    public partial class FormPokemon : Window
    {
        private readonly List<string> typesPokemon;
        public FormPokemon()
        {
            InitializeComponent();
            typesPokemon = TypesPokemon.GetObject().types;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FormTypes newForm = new FormTypes(ErrorTypes);
            newForm.ShowDialog();
            if(typesPokemon.Count == 0) return;
            CreateTypes.CreateTargets(ContainerNewTypes);
            TextButton.Text = "Cambiar Tipos";
        }

        private void InsertPokemon()
        {
            string name = TextBoxName.Text, url = TextBoxUrl.Text;
            bool validate = ValidateForm.ValidateInsert(name, url, ErrorName, ErrorUrl, ErrorTypes);
            if (!validate) return;
            CreatePokemonDb newPokemon = new CreatePokemonDb();
            newPokemon.Create(name, url, this);
            if (typesPokemon.Count > 0) typesPokemon.Clear();
        }

        private void UpdatePokemon()
        {
            string name = TextBoxName.Text, url = TextBoxUrl.Text,
                idPokemon = ElementsChange.GetObject().ListElementChange[0];
            bool validate = ValidateForm.ValidateUpdate(name, 
                url, 
                idPokemon, 
                ErrorName, 
                ErrorUrl, 
                ErrorTypes);
            if (!validate) return;
            var result = DialogConfirmation.Message("Esta seguro que desea actualizar este registro?");
            if (!result) return;
            UpdatePokemonDb newUpdate = new UpdatePokemonDb();
            newUpdate.Update(idPokemon, name, url, this);
            if (typesPokemon.Count > 0) typesPokemon.Clear();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            switch (OptionText.Text)
            {
                case "Aceptar":
                    InsertPokemon();
                    break;
                case "Actualizar":
                    UpdatePokemon();
                    break;
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (typesPokemon.Count > 0) typesPokemon.Clear();
            this.Close();
        }

        public void InitVariables(Dictionary<string,object> data)
        {
            TextBoxName.Text = data["name"] as string;
            TextBoxUrl.Text = data["url"] as string;
            typesPokemon.AddRange(data["types"] as List<string>);
            CreateTypes.CreateTargets(ContainerNewTypes);
            TextButton.Text = "Cambiar Tipos";
            OptionText.Text = "Actualizar";
        }
    }
}
