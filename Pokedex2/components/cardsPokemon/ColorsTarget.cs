using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Pokedex2.components.cardsPokemon
{
    class ColorsTarget
    {
        private static ColorsTarget colorsTarget = null;
        public readonly Dictionary<string, SolidColorBrush[]> colorsTypes;
        private ColorsTarget() 
        {
            colorsTypes = new Dictionary<string, SolidColorBrush[]>();
        }

        public static ColorsTarget GetObject()
        {
            if (colorsTarget is null)
            {
                colorsTarget = new ColorsTarget();
                InitializeColorsTypes();
            }
            return colorsTarget;
        }

        public static SolidColorBrush ConvertColor(byte r, byte g, byte b)
        {
            Color argb = Color.FromArgb(255, r, g, b);
            return new SolidColorBrush(argb);
        }

        private static void InitializeColorsTypes()
        {
            SolidColorBrush black = ConvertColor(0, 0, 0);
            SolidColorBrush white = ConvertColor(255, 255, 255);
            colorsTarget.colorsTypes.Add("bicho", new SolidColorBrush[] { ConvertColor(114, 159, 63), white });
            colorsTarget.colorsTypes.Add("dragon", new SolidColorBrush[] { ConvertColor(241, 110, 87), white });
            colorsTarget.colorsTypes.Add("hada", new SolidColorBrush[] { ConvertColor(253, 185, 233), black });
            colorsTarget.colorsTypes.Add("fuego", new SolidColorBrush[] { ConvertColor(253, 125, 36), white });
            colorsTarget.colorsTypes.Add("fantasma", new SolidColorBrush[] { ConvertColor(123, 98, 163), white });
            colorsTarget.colorsTypes.Add("tierra", new SolidColorBrush[] { ConvertColor(171, 152, 66), white });
            colorsTarget.colorsTypes.Add("normal", new SolidColorBrush[] { ConvertColor(164, 172, 175), white });
            colorsTarget.colorsTypes.Add("psiquico", new SolidColorBrush[] { ConvertColor(243, 102, 185), white });
            colorsTarget.colorsTypes.Add("acero", new SolidColorBrush[] { ConvertColor(158, 183, 184), white });
            colorsTarget.colorsTypes.Add("siniestro", new SolidColorBrush[] { ConvertColor(112, 112, 112), white });
            colorsTarget.colorsTypes.Add("electrico", new SolidColorBrush[] { ConvertColor(238, 213, 53), white });
            colorsTarget.colorsTypes.Add("lucha", new SolidColorBrush[] { ConvertColor(213, 103, 35), white });
            colorsTarget.colorsTypes.Add("volador", new SolidColorBrush[] { ConvertColor(166, 224, 241), black });
            colorsTarget.colorsTypes.Add("planta", new SolidColorBrush[] { ConvertColor(155, 204, 80), white });
            colorsTarget.colorsTypes.Add("hielo", new SolidColorBrush[] { ConvertColor(81, 196, 231), white });
            colorsTarget.colorsTypes.Add("veneno", new SolidColorBrush[] { ConvertColor(185, 127, 201), white });
            colorsTarget.colorsTypes.Add("roca", new SolidColorBrush[] { ConvertColor(163, 140, 33), white });
            colorsTarget.colorsTypes.Add("agua", new SolidColorBrush[] { ConvertColor(69, 146, 196), white });
        }
    }
}
