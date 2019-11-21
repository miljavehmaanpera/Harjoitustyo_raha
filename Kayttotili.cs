using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Harjoitustyo_raha
{
    [Serializable]
    public class Kayttotili : Tili
    {

        // konstruktori
        public Kayttotili(string henkilo, string tili) : base(henkilo, tili)
        {
            Omistaja = henkilo;
            tilinNimi = tili;
            Saldo = 0;
        }

        // ylikirjoitetaan kantaluokan samanniminen metodi
        public override void NostaRahaa(double maara)
        {
            this.Saldo = this.Saldo - maara;
        }

    }
}
