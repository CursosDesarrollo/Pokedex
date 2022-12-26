using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex2.components.optionsCrud
{
    class IdTypes
    {
        private static IdTypes instance = null;
        public readonly Dictionary<string, int> listId;

        private IdTypes() 
        {
            listId = new Dictionary<string, int>();
        }

        public static IdTypes GetObject()
        {
            if(instance is null)
            {
                instance = new IdTypes();
                AddId();
            }
            return instance;
        }

        private static void AddId()
        {
            instance.listId.Add(("bicho"), 1);
            instance.listId.Add(("dragon"), 2);
            instance.listId.Add(("hada"), 3);
            instance.listId.Add(("fuego"), 4);
            instance.listId.Add(("fantasma"), 5);
            instance.listId.Add(("tierra"), 6);
            instance.listId.Add(("normal"), 7);
            instance.listId.Add(("psiquico"), 8);
            instance.listId.Add(("acero"), 9);
            instance.listId.Add(("siniestro"), 10);
            instance.listId.Add(("electrico"), 11);
            instance.listId.Add(("lucha"), 12);
            instance.listId.Add(("volador"), 13);
            instance.listId.Add(("planta"), 14);
            instance.listId.Add(("hielo"), 15);
            instance.listId.Add(("veneno"), 16);
            instance.listId.Add(("roca"), 17);
            instance.listId.Add(("agua"), 18);
        }
    }
}
