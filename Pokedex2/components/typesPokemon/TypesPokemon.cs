using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex2.components.typesPokemon
{
    class TypesPokemon
    {
        private static TypesPokemon typesPokemon = null;
        public readonly List<string> types;
        public readonly List<string> cloneTypes;
        private TypesPokemon() 
        {
            types = new List<string>();
            cloneTypes = new List<string>();
        }

        public static TypesPokemon GetObject() 
        {
            if (typesPokemon is null) typesPokemon = new TypesPokemon();
            return typesPokemon;
        }
    }
}
