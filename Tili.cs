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
    public class Tili : ITallettava
    {
        // kaikki tilin "ominaisuudet" olisi voitu tehdä properteilla, mutta käytettiin sekä ns. perinteisiä muuttujia 
        // että properteja harjoituksen vuoksi
        private string omistaja;
        private double talletettavaMaara;
        public string tilinNimi;

        // propertyt
        public string Omistaja
        {
            get { return omistaja; }
            set { omistaja = value; }
        }

        public double Saldo { get; set; }


        // konstruktori
        public Tili(string henkilo, string tili) 
        {
            Omistaja = henkilo;
            tilinNimi = tili;
        }

        // metodit
        public void TalletaRahaa(double maara)
        {
            talletettavaMaara = maara;
            this.Saldo = this.Saldo + talletettavaMaara;
        }

        // Seuraavassa metodissa ei ole mitään järkeä, sitä ei käytetä ja se on kirjattu tänne vain siksi, 
        // että sen päälle voidaan kirjoittaa aliluokissa.
        public virtual void NostaRahaa(double maara)
        {
            this.Saldo = this.Saldo;
        }












    }
}
