using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harjoitustyo_raha
{
    class Saastopossu : ISailyttava
    {

        // tällä ei ole mitään muuta tekoa kuin se, että kun pääohjelmassa luodaan näin:
        // ISailyttava possu = new Saastopossu();
        // niin huomataan, että voidaan käyttää vain metodia, mutta ei tätä propertya.
        // Itsessään tämä luokka on siis vähän turha toiminnaltaan, tarkoitus oli vain tehdä tämä rakenne.
        public int Maara { get; set; }

        //metodi
        public void SailytaRahaa()
        {
            Console.WriteLine("Laitoit possuun rahaa. Röh!");
        }

        

    }
      
}
