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
    public class Saastotili : Tili
    {
        //property
        public double SaastoTavoite { get; set; }

        // konstruktori
        public Saastotili(string henkilo, string tili, double tavoite) : base(henkilo, tili)
        {
            Omistaja = henkilo;
            tilinNimi = tili;
            SaastoTavoite = tavoite;
        }


        // Tämän metodin nimi ei ole kovin hyvä, sillä se ei kuvaa toimintaa, koska säästötililtä ei voi nostaa rahaa.
        // Metodi on siksi näin, että voitiin ylikuormittaa kantaluokan samanniminen metodi.
        public void NostaRahaa()
        {
            Console.WriteLine("Säästötililtä ei voi nostaa rahaa! \n");
        }



    }
}
