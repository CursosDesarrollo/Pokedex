using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex2.models
{
    class Pokemon : Model
    {
        public readonly static string table = "pokemon";
        public readonly static string primaryKey = "id_pokemon";

        public Pokemon() : base(table, primaryKey) {}
    }
}
