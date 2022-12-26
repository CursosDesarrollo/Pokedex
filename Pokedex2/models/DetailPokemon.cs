using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex2.models
{
    class DetailPokemon : Model
    {
        public readonly static string table = "detalle_pokemon";
        public readonly static string primaryKey = "id_detalle_pokemon";

        public DetailPokemon() : base(table, primaryKey) { }
    }
}
