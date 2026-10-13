using System;
using System.Collections.Generic;
using System.Text;

namespace Okosotthon
{
    public class OkosotthonKozpont
    {
        private readonly List<OkosEszkoz> eszkozok;
        public OkosotthonKozpont()
        {
            this.eszkozok = new List<OkosEszkoz>();
        }
        public void EszkozHozzaadasa(OkosEszkoz eszkoz)
        {
            this.eszkozok.Add(eszkoz);
        }


        public void OsszesCsatlakoztatasa()
        {
            foreach (OkosEszkoz eszkoz in this.eszkozok)
            {
                eszkoz.Csatlakozas();
            }
        }

        public int RendszerDiagnosztikaFuttatasa()
        {
            int sikeres = 0;

            foreach (OkosEszkoz eszkoz in this.eszkozok)
            {
                if (eszkoz.DiagnosztikaFuttatasa())
                {
                    sikeres++;
                }
            }

            return sikeres;
        }

        public void OsszesRiasztoInditasa()
        {
            foreach (OkosEszkoz eszkoz in this.eszkozok)
            {
                if (eszkoz is IHangjelzo hangjelzo)
                {
                    hangjelzo.VeszjelzesInditasa();
                }
            }
        }

        public void OsszesEszkozCsipogtatasa(int hosszu)
        {
            foreach (OkosEszkoz eszkoz in this.eszkozok)
            {
                if (eszkoz is IHangjelzo hangjelzo)
                {
                    hangjelzo.Csipog(hosszu);
                }
            }
        }

        public void OsszesStatisztikaKiirasa()
        {
            foreach (OkosEszkoz eszkoz in this.eszkozok)
            {
                if (eszkoz is IStatisztika statisztika)
                {
                    Console.WriteLine(
                        statisztika.StatisztikatKeszit());

                    Console.WriteLine("--------------------");
                }
            }
        }

        public void OsszesIdozitoBeallitasa(int perc)
        {
            foreach (OkosEszkoz eszkoz in this.eszkozok)
            {
                if (eszkoz is IIdozitheto idozitheto)
                {
                    idozitheto.IdozitestBeallit(perc);
                }
            }
        }

        public void OsszesIdozitoLekerdezese()
        {
            foreach (OkosEszkoz eszkoz in this.eszkozok)
            {
                if (eszkoz is IIdozitheto idozitheto)
                {
                    Console.WriteLine(
                        idozitheto.GetMaradtIdo() + " perc van hátra.");
                }
            }
        }
    }
}