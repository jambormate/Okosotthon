using System;
using System.Collections.Generic;
using System.Text;

namespace Okosotthon
{
    public class OkosZar: OkosEszkoz
    {
        public OkosZar(string azonosito, string nev, string pinKod)
        : base(azonosito, nev)
        {
            throw new NotImplementedException();
        }

        public override void ParancsVegrehajtasa(string parancs)
        {
            throw new NotImplementedException();
        }


        public override string AllapotJelentes()
        {
            throw new NotImplementedException();
        }

        protected override bool OnTesztFuttatasa()
        {
            throw new NotImplementedException();
        }

        public override void GyariBeallitasokVisszaallitasa()
        {
            throw new NotImplementedException();
        }


    }
}
