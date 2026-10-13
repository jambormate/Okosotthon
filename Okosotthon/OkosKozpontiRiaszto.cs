using System;
using System.Collections.Generic;
using System.Text;

namespace Okosotthon
{
    public class OkosKozpontiRiaszto : OkosEszkoz,
        IIdozitheto, IHangjelzo, IStatisztika
    {
        private DateTime idozitoVege;
        private bool idozitoAktiv;
        private int csipogasokSzama;
        private int riasztasokSzama;

        public bool RiasztasAktiv { get; private set; }

        public OkosKozpontiRiaszto(
            string azonosito,
            string nev)
            : base(azonosito, nev)
        {
            idozitoAktiv = false;
            RiasztasAktiv = false;
        }

        public void IdozitestBeallit(int perc)
        {
            if (perc < 0)
                throw new ArgumentOutOfRangeException(nameof(perc));

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

        public void Csipog(int hosszu)
        {
            if (hosszu < 0)
                throw new ArgumentOutOfRangeException(nameof(hosszu));

            csipogasokSzama++;
            Console.WriteLine("Központi riasztó csipogása: " + hosszu + " ms");
        }

        public void VeszjelzesInditasa()
        {
            RiasztasAktiv = true;
            riasztasokSzama++;

            Console.WriteLine("!!! KÖZPONTI VÉSZJELZÉS !!!");
        }

        public string StatisztikatKeszit()
        {
            return
                "Központi riasztó statisztika\n" +
                "Hátralévő idő: " + GetMaradtIdo() + " perc\n" +
                "Csipogások száma: " + csipogasokSzama + "\n" +
                "Riasztások száma: " + riasztasokSzama + "\n" +
                "Riasztás aktív: " +
                (RiasztasAktiv ? "Igen" : "Nem");
        }

        public override void ParancsVegrehajtasa(string parancs)
        {
            if (parancs == "RIASZTO_IND")
            {
                VeszjelzesInditasa();
            }
            else if (parancs == "RIASZTO_LEALL")
            {
                RiasztasAktiv = false;
            }
            else if (parancs == "CSIPOG")
            {
                Csipog(200);
            }
        }

        public override string AllapotJelentes()
        {
            return RiasztasAktiv ? "VÉSZJELZÉS AKTÍV": "Készenlétben";
        }

        protected override bool OnTesztFuttatasa()
        {
            return true;
        }

        public override void GyariBeallitasokVisszaallitasa()
        {
            base.GyariBeallitasokVisszaallitasa();

            idozitoAktiv = false;
            RiasztasAktiv = false;
            csipogasokSzama = 0;
            riasztasokSzama = 0;
        }
    }
}
