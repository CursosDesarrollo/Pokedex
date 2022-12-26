using Pokedex2.components.views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Pokedex2.components.typesPokemon
{
    class ValidateTypes
    {
        public ValidateTypes() { }

        private void EmptyList(List<string> list)
        {
            if (list.Count == 0) throw new Exception("Tienes que elegir un tipo");
        }

        private void MaxTypes(List<string> list)
        {
            if (list.Count > 2) throw new Exception("Un pokemon puede tener maximo dos tipos");
        }

        public void Validate(FormTypes form) 
        {
            List<string> cloneListTypes = TypesPokemon.GetObject().cloneTypes,
                listTypes = TypesPokemon.GetObject().types;
            try
            {
                EmptyList(cloneListTypes);
                MaxTypes(cloneListTypes);
                if (listTypes.Count > 0) listTypes.Clear();
                listTypes.AddRange(cloneListTypes);
                cloneListTypes.Clear();
                form.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}