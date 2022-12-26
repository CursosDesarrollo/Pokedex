using Pokedex2.components.views;
using Pokedex2.models;
using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex2.components.optionsCrud
{
    class DeletePokemonDb
    {
        public DeletePokemonDb() { }

        private void DeleteTypes(DetailPokemon newDatil, List<string> elementsChange)
        {
            elementsChange.ForEach(value => 
            newDatil.Delete().Where("id_pokemon", "=", value).Exec());
        }

        private void DeletePokemon(Pokemon newPokemon, List<string> elementsChange)
        {
            elementsChange.ForEach(value =>
            newPokemon.Delete().Where("id_pokemon", "=", value).Exec());
        }

        public void Delete()
        {
            try
            {
                var elementsChange = ElementsChange.GetObject().ListElementChange;
                Pokemon newPokemon = new Pokemon();
                DetailPokemon newDatil = new DetailPokemon();
                DeleteTypes(newDatil, elementsChange);
                DeletePokemon(newPokemon, elementsChange);
                MessageBox.Show("Se elimino correctamente");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
