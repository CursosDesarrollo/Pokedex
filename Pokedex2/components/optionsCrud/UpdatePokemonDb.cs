using Pokedex2.components.typesPokemon;
using Pokedex2.components.views;
using Pokedex2.models;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Windows;

namespace Pokedex2.components.optionsCrud
{
    class UpdatePokemonDb
    {
        public UpdatePokemonDb() { }

        private void InsertTypes(List<string> newTypes, DetailPokemon modelDetail, string idPokemon)
        {
            var listId = IdTypes.GetObject().listId;
            newTypes.ForEach(value =>
            {
                modelDetail.Insert(new Dictionary<string, string>()
                {
                    {"id_pokemon",idPokemon},
                    {"id_tipo_pokemon",Convert.ToString(listId[value]) }
                }).Exec();
            });
        }

        private void DeleteTypes(DetailPokemon modelDetail, string idPokemon)
        {
            var typesOriginal = modelDetail.Select("id_detalle_pokemon")
                .Where("id_pokemon", "=", idPokemon)
                .Exec();
            typesOriginal.ForEach(value =>
            {
                modelDetail.Delete()
                .Where("id_detalle_pokemon", "=", value["id_detalle_pokemon"])
                .Exec();
            });
        }

        private void UpdateTypes(List<string> types, string idPokemon)
        {
            var newTypes = TypesPokemon.GetObject().types;
            var jsonTypes = JsonSerializer.Serialize(types);
            var jsonNewTypes = JsonSerializer.Serialize(newTypes);
            if (jsonTypes == jsonNewTypes) return;
            DetailPokemon newDetail = new DetailPokemon();
            DeleteTypes(newDetail, idPokemon);
            InsertTypes(newTypes, newDetail, idPokemon);
        }

        private void UpdateDate(Pokemon newPokemon, string idPokemon)
        {
            newPokemon.Update(new Dictionary<string, string>()
            {
                {"fecha_actualizacion","current_date()" }
            },false).Where("id_pokemon","=", idPokemon).Exec();
        }

        public void Update(string idPokemon, string name, string url, FormPokemon newForm)
        {
            try
            {
                Pokemon newPokemon = new Pokemon();
                ReadPokemonDb newRead = new ReadPokemonDb();
                var result = newRead.ReadOne(idPokemon)[0];
                UpdateTypes((List<string>)result["types"], idPokemon);
                UpdateDate(newPokemon,idPokemon);
                var newDictionary = new Dictionary<string, string>();
                if (url != (string)result["url"]) newDictionary.Add("imagen_url", url);
                if (name != (string)result["name"]) newDictionary.Add("nombre", name);
                if (newDictionary.Count != 0)
                {
                    newPokemon.Update(newDictionary)
                    .Where("id_pokemon", "=", idPokemon)
                    .Exec();
                }
                MessageBox.Show("Se actualizo correctamente");
                newForm.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
