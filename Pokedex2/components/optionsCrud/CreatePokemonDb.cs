using Pokedex2.components.typesPokemon;
using Pokedex2.components.views;
using Pokedex2.models;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Pokedex2.components.optionsCrud
{
    class CreatePokemonDb
    {
        public CreatePokemonDb() { }

        private string InsertPokemon(string name, string url)
        {
            Pokemon pokemon = new Pokemon();
            var insert = pokemon.Insert(new Dictionary<string, string>()
                {
                    {"nombre",name },
                    {"imagen_url",url }
                }).Exec();
            if (insert.Count > 0) return insert[0][Pokemon.primaryKey];
            return "0";
        }

        private void InsertTypes(string idPokemon)
        {
            var types = TypesPokemon.GetObject().types;
            var idTypes = IdTypes.GetObject().listId;
            DetailPokemon detail = new DetailPokemon();
            types.ForEach(val =>
            {
                detail.Insert(new Dictionary<string, string>()
                {
                    {"id_pokemon", idPokemon},
                    {"id_tipo_pokemon", Convert.ToString(idTypes[val]) }
                }).Exec();
            });
        }

        public void Create(string name, string url, FormPokemon form)
        {
            try
            {
                var idPokemon = InsertPokemon(name, url);
                InsertTypes(idPokemon);
                MessageBox.Show("Creado exitosamente");
                form.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show(ex.Message);
            }
        }
    }
}
