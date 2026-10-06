using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace Okosotthon
{
    public class Termosztat : OkosEszkoz
    {
        public double JelenlegiHomerseklet { get; private set; }
        public double CelHomerseklet { get; private set; }
        public Termosztat(string azonosito, string nev, double celHomerseklet)
            : base(azonosito, nev)
        {
            this.JelenlegiHomerseklet = 21.0;
            this.CelHomerseklet = celHomerseklet;
        }

        public override void ParancsVegrehajtasa(string parancs)
        {
            if (parancs.StartsWith("BEALLIT_HOMERSEKLET:"))
            {
                string ertek = parancs.Substring("BEALLIT_HOMERSEKLET:".Length);
                this.CelHomerseklet = double.Parse(ertek, CultureInfo.InvariantCulture);
            }
        }

        public override string AllapotJelentes()
        {
            return "Jelenlegi hőmérséklet: " + this.JelenlegiHomerseklet +
                   " °C, célhőmérséklet: " + this.CelHomerseklet + " °C";
        }

        protected override bool OnTesztFuttatasa()
        {
            return this.CelHomerseklet >= 5.0 &&
                   this.CelHomerseklet <= 35.0;
        }

    }
}
