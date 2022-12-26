using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex2.components.optionsCrud
{
    class ElementsChange
    {
        private static ElementsChange instance = null;
        public List<string> ListElementChange { get; }
        private ElementsChange() 
        {
            ListElementChange = new List<string>();
        }

        public static ElementsChange GetObject()
        {
            if(instance is null) instance = new ElementsChange();
            return instance;
        }
    }
}
