using MySqlX.XDevAPI.Common;
using Pokedex2.models;
using System;
using System.Collections.Generic;
using System.Windows;
using static Google.Protobuf.WellKnownTypes.Field.Types;

namespace Pokedex2.components.optionsCrud
{
    class ReadPokemonDb
    {
        public ReadPokemonDb() { }

        private Dictionary<string, object> NewDictionary(Dictionary<string,string> value)
        {
            var newDictionary = new Dictionary<string, object>()
            {
                {"url",value["imagen_url"] },
                {"id",value["id_pokemon"] },
                {"name",value["nombre"] },
                {"types",new List <string>() { value["tipo"] } }
            };
            return newDictionary;
        }

        private List<Dictionary<string, object>> Results(List<Dictionary<string, string>> resultQuery)
        {
            var newList = new List<Dictionary<string, object>>();
            int currentRow = 0;
            string currentId = "0";
            resultQuery.ForEach(val =>
            {
                if (currentId == val["id_pokemon"])
                {
                    var types = (List<string>)newList[currentRow]["types"];
                    types.Add(val["tipo"]);
                    return;
                }
                var newDictionary = NewDictionary(val);
                newList.Add(newDictionary);
                currentId = val["id_pokemon"];
                currentRow = newList.Count - 1;
            });
            return newList;
        }

        private Pokemon TemplateQuery()
        {
            Pokemon newPokemon = new Pokemon();
            newPokemon.Select("p.id_pokemon,p.nombre,p.imagen_url,t.nombre as tipo",
                "pokemon as p,detalle_pokemon as d,tipo_pokemon as t")
                .Where("p.id_pokemon", "=", "d.id_pokemon", false)
                .AndWhere("t.id_tipo_pokemon", "=", "d.id_tipo_pokemon", false);
            return newPokemon;
        }

        public List<Dictionary<string, object>> ReadOne(string idPokemon)
        {
            try
            {
                Pokemon newPokemon = TemplateQuery();
                var result = newPokemon.AndWhere("p.id_pokemon", "=", idPokemon)
                    .Exec();
                return Results(result);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return new List<Dictionary<string, object>>();
            }
        }

        public List<Dictionary<string, object>> ReadFind(string find)
        {
            try
            {
                Pokemon newPokemon = TemplateQuery();
                var result = newPokemon.AndLike("p.nombre", find)
                    .Order("p.fecha_creacion", "ASC").Exec();
                return Results(result);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return new List<Dictionary<string, object>>();
            }
        }

        public List<Dictionary<string, object>> ReadAll()
        {
            try
            {
                Pokemon newPokemon = TemplateQuery();
                var result = newPokemon.Order("p.fecha_creacion", "ASC").Exec();
                return Results(result);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return new List<Dictionary<string, object>>();
            }
        }
    }
}
