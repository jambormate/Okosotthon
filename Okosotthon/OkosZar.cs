using System;
using System.Collections.Generic;
using System.Text;

namespace Okosotthon
{
    public class OkosZar : OkosEszkoz,
        IHangjelzo, IStatisztika
    {
        private string pinKod;
        private int sikeresNyitasok;
        private int sikertelenProbalkozasok;
        private int egymasUtaniHibak;

        public bool ZartE { get; private set; }
        public bool VeszelzesAktiv { get; private set; }

        public OkosZar(string azonosito, string nev, string pinKod)
            : base(azonosito, nev)
        {
            this.pinKod = pinKod;
            this.ZartE = true;
            VeszelzesAktiv = false;
        }

        public override void ParancsVegrehajtasa(string parancs)
        {
            if (parancs.StartsWith("NYITAS:"))
            {
                string megadottPin = parancs.Substring("NYITAS:".Length);

                if (megadottPin == this.pinKod)
                {
                    this.ZartE = false;
                    sikeresNyitasok++;
                    egymasUtaniHibak = 0;
                }
                else
                {
                    sikertelenProbalkozasok++;
                    egymasUtaniHibak++;

                    if (egymasUtaniHibak >= 3)
                    {
                        VeszjelzesInditasa();
                    }
                    else
                    {
                        Csipog(200);
                    }
                }
            }
            else if (parancs == "ZARAS")
            {
                this.ZartE = true;
            }
            else if (parancs == "RIASZTO_KIKAPCSOLAS")
            {
                VeszelzesAktiv = false;
                egymasUtaniHibak = 0;
            }
        }

        public void Csipog(int hosszu)
        {
            Console.WriteLine("Csipogás: " + hosszu + " ms");
        }

        public void VeszjelzesInditasa()
        {
            VeszelzesAktiv = true;
            Console.WriteLine("VÉSZJELZÉS! Illetéktelen hozzáférési kísérlet!");
        }

        public string StatisztikatKeszit()
        {
            return
                "Okoszár statisztika\n" +
                "Sikeres nyitások: " + sikeresNyitasok + "\n" +
                "Sikertelen PIN-próbálkozások: " + sikertelenProbalkozasok + "\n" +
                "Állapot: " + (ZartE ? "ZÁRVA" : "NYITVA") + "\n" +
                "Vészjelzés aktív: " + (VeszelzesAktiv ? "Igen" : "Nem");
        }

        public override string AllapotJelentes()
        {
            return ZartE ? "ZÁRVA" : "NYITVA";
        }

        protected override bool OnTesztFuttatasa()
        {
            return !string.IsNullOrEmpty(pinKod);
        }

        public override void GyariBeallitasokVisszaallitasa()
        {
            base.GyariBeallitasokVisszaallitasa();

            this.pinKod = "0000";
            this.ZartE = true;
            VeszelzesAktiv = false;
            sikeresNyitasok = 0;
            sikertelenProbalkozasok = 0;
            egymasUtaniHibak = 0;
        }
    }
}