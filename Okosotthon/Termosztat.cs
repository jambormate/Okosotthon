using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace Okosotthon
{
    public class Termosztat : OkosEszkoz,
        IIdozitheto, IStatisztika
    {
        public double JelenlegiHomerseklet { get; private set; }
        public double CelHomerseklet { get; private set; }

        private DateTime idozitoVege;
        private bool idozitoAktiv;

        public Termosztat(string azonosito, string nev, double celHomerseklet)
            : base(azonosito, nev)
        {
            this.JelenlegiHomerseklet = 21.0;
            this.CelHomerseklet = celHomerseklet;
            idozitoAktiv = false;
        }

        public override void ParancsVegrehajtasa(string parancs)
        {
            if (parancs.StartsWith("BEALLIT_HOMERSEKLET:"))
            {
                string ertek = parancs.Substring("BEALLIT_HOMERSEKLET:".Length);
                this.CelHomerseklet = double.Parse(ertek, CultureInfo.InvariantCulture);
            }
        }
        public void IdozitestBeallit(int perc)
        {
            if (perc < 0)
                throw new ArgumentOutOfRangeException(nameof(perc), "Az időzítés nem lehet negatív.");

            idozitoVege = DateTime.Now.AddMinutes(perc);
            idozitoAktiv = perc > 0;
        }

        public int GetMaradtIdo()
        {
            if (!idozitoAktiv)
                return 0;

            TimeSpan maradek = idozitoVege - DateTime.Now;

            if (maradek.TotalSeconds <= 0)
            {
                idozitoAktiv = false;
                return 0;
            }

            return (int)Math.Ceiling(maradek.TotalMinutes);
        }

        public string StatisztikatKeszit()
        {
            return
                "Termosztát statisztika\n" + "Jelenlegi hőmérséklet: " +
                JelenlegiHomerseklet.ToString("F1", CultureInfo.InvariantCulture) + " °C\n" + "Célhőmérséklet: " + 
                CelHomerseklet.ToString("F1", CultureInfo.InvariantCulture) + " °C\n" + "Hátralévő idő: " + GetMaradtIdo() + " perc";
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