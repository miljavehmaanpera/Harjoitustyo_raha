using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

// Ohjelmassa voi luoda tilejä ja tallettaa ja nostaa rahaa olemassa olevalta tililtä.

namespace Harjoitustyo_raha
{
    class Program
    {

        static void Main(string[] args)
        {
            // Pääohjelmassa käytettävien muuttujien esittely. 'Vastaus' on aina yksi merkki, 
            // mutta koska myöhemmin halutaan muuntaa merkki isoksi kirjaimeksi, käytetään string-tyyppiä
            int numerovastaus;
            int tilienLkm;
            string vastaus; 
            string tilinOmistaja;
            string tiliName;
            double saastoTavoite;
            double talletettavaMaara;
            double nostettavaMaara;

            // ohjelma pyörii kunnes sen käyttö halutaan lopettaa
            do
            {
                Console.WriteLine("\nMitä haluat tehdä:");
                Console.WriteLine("[L] Luo uusi tili");
                Console.WriteLine("[T] Talleta rahaa olemassa olevalle tilille");
                Console.WriteLine("[N] Nosta rahaa tililtä");
                Console.WriteLine("[K] Katso tilitietoja");
                Console.WriteLine("[P] Laita säästöpossuun rahaa");
                Console.WriteLine("[E] Lopeta ohjelman käyttö");

                vastaus = Console.ReadLine();
                vastaus = vastaus.ToUpper(); 
                // muutetaan vastaus isoksi kirjaimeksi, jotta tulevissa if-lauseissa voidaan käyttää vain isoja kirjaimia


                if (vastaus == "L")
                {
                    do
                    {
                        // kysymystä toistetaan, kunnes saadaan kelvollinen vastaus (S tai K) tai ohjelma suljetaan (E)
                        Console.Write("Haluatko luoda käyttötilin [K] vai säästötilin [S]? ");
                        vastaus = Console.ReadLine().ToUpper();
                        
                    } while (vastaus != "K" && vastaus != "S" && vastaus != "E");

                    if (vastaus=="E")
                    {
                        break;
                    }

                    Console.Write("Syötä tilin omistaja: ");
                    tilinOmistaja = Console.ReadLine();

                    Console.Write("Syötä tilin nimi: ");
                    tiliName = Console.ReadLine();

                    Pankki pankki = new Pankki();
                    pankki.LuoTili(tilinOmistaja,tiliName); // tämä kirjaa henkilön ja tilin nimen tiedostoon

                    if (vastaus=="S")
                    {
                        // jos käyttäjän syöte on luku, tallennetaan se muuttujaan saastoTavoite, muuten muuttujan arvoksi tulee nolla
                        Console.Write("Syötä tilin säästötavoite: ");
                        double.TryParse(Console.ReadLine(),out saastoTavoite);

                        Saastotili saastotili = new Saastotili(tilinOmistaja, tiliName, saastoTavoite);

                        // Aiemmin tili lisättiin vain olemassa olevien tilien listalle, jossa ei ole tietoja rahamääristä.
                        // Ladataan olemassa olevat tilioliot tiedostosta listalle ja lisätään uusi luotu tili listan perään, 
                        // minkä jälkeen tallennetaan lopputulos samaan tiedostoon.

                        pankki.ListaSaastotileista = new List<Saastotili>();
                        pankki.LataaSaastotilit();
                        pankki.ListaSaastotileista.Add(saastotili);
                        pankki.TallennaSaastotili();
                    }
                    else if (vastaus=="K")
                    {
                        Kayttotili kayttotili = new Kayttotili(tilinOmistaja,tiliName);
                        
                        pankki.ListaKayttotileista = new List<Kayttotili>();
                        pankki.LataaKayttotilit();
                        pankki.ListaKayttotileista.Add(kayttotili);
                        pankki.TallennaKayttotili();
                    }  
                }

                else if (vastaus == "T")
                {
                    // tulostetaan ensin näytölle listaus olemassa olevista tileistä, käyttäjä valitsee, mille tilille haluaa tallettaa
                    Pankki pankki = new Pankki();
                    pankki.NaytaTilit();

                    // kysymystä toistetaan kunnes saadaan numerovastaus, joka tallennetaan muuttujaan
                    do
                    {
                        Console.Write("Syötä numero, mihin tilille haluat tallettaa rahaa: "); 

                    } while (! int.TryParse(Console.ReadLine(), out numerovastaus));

                    // luetaan tiedostosta olemassa olevat tilit ja lasketaan niiden määrä
                    List<string> tiliLista = pankki.LueTilit();
                    tilienLkm = tiliLista.Count;

                    if (numerovastaus==0)
                    {
                        break;
                    }
                    else if (numerovastaus <= tilienLkm) // jos vastausta vastaava tili on olemassa, talletetaan haluttu määrä rahaa
                    {
                        // käsitellään vastauksen mukaista riviä listalta
                        string alkio = tiliLista[numerovastaus - 1];

                        // jaetaan rivin osat kaksoispisteen molemmilta puolilta taulukon kahteen soluun
                        string[] osat = alkio.Split(':');

                        // Saadaan selville tilin omistaja ja tilin nimi, mihin halutaan tallettaa. 
                        // Selvitetään molemmat, koska samalla henkilöllä voi olla useita tilejä.
                        tilinOmistaja = osat[0];
                        tiliName = osat[1];

                        // jos vastaus ei ole numero, talletettavaMaara = 0, muulloin käyttäjän syöte
                        Console.Write("Kuinka paljon haluat tallettaa? Syötä numeroina: ");
                        double.TryParse(Console.ReadLine(), out talletettavaMaara);

                        pankki.LataaKayttotilit();
                        pankki.LataaSaastotilit();

                        // etsitään käyttäjän valitsema tili ja tehdään sinne haluttu talletus
                        foreach (var item in pankki.ListaKayttotileista)
                        {
                            if (item.Omistaja == tilinOmistaja && item.tilinNimi == tiliName)
                            {
                                item.TalletaRahaa(talletettavaMaara);
                                pankki.TallennaKayttotili();
                            }
                        }
                        foreach (var item in pankki.ListaSaastotileista)
                        {
                            if (item.Omistaja == tilinOmistaja && item.tilinNimi == tiliName)
                            {
                                item.TalletaRahaa(talletettavaMaara);

                                pankki.TallennaSaastotili();

                                if (item.Saldo >= item.SaastoTavoite)
                                {
                                    Console.WriteLine("Onnea, olet saavuttanut säästötavoitteesi " + item.SaastoTavoite + " e. \n");
                                }
                            }
                        }
                    }

                    else
                    {
                        Console.WriteLine("Kyseistä tiliä ei ole olemassa!");
                    }
                }

                else if (vastaus == "N")
                {
                    // Luetaan tiedostosta ja tulostetaan olemassa olevat tilit.
                    // Käyttäjä valitsee numerolla, miltä tililä haluaa nostaa.
                    // Vain käyttötileiltä saa nostaa rahaa.

                    Pankki pankki = new Pankki();
                    pankki.NaytaTilit();
                    
                    // kysymystä toistetaan kunnes saadaan numerovastaus, joka tallennetaan muuttujaan numerovastaus
                    do
                    {
                        Console.Write("Syötä numero, mistä tililtä haluat nostaa rahaa:");

                    } while (! int.TryParse(Console.ReadLine(), out numerovastaus));


                    List<string> tiliLista = pankki.LueTilit();
                    tilienLkm = tiliLista.Count;

                    if (numerovastaus==0)
                    {
                        break;
                    }

                    else if (numerovastaus <= tilienLkm)
                    {
                        // käsitellään vastauksen mukaista riviä
                        string alkio = tiliLista[numerovastaus - 1];

                        // jaetaan rivin osat kaksoispisteen molemmilta puolilta taulukon kahteen soluun
                        string[] osat = alkio.Split(':');

                        tilinOmistaja = osat[0];
                        tiliName = osat[1];

                        // Ladataan ensin kaikki tilioliot tiedostosta
                        pankki.LataaKayttotilit();
                        pankki.LataaSaastotilit();

                        // käydään kaikki tilit läpi sekä käyttö- että säästötililistoista ja etsitään
                        // ensin oikea omistaja + tili
                        foreach (var item in pankki.ListaKayttotileista)
                        {
                            if (item.Omistaja == tilinOmistaja && item.tilinNimi == tiliName)
                            {
                                // mikäli saadaan numerovastaus, tallennetaan se muuttujaan, muuten muuttujan arvoksi tulee nolla
                                Console.Write("Kuinka paljon haluat nostaa? Syötä numeroina: ");
                                double.TryParse(Console.ReadLine(), out nostettavaMaara);

                                // jos tilillä on tarpeeksi rahaa, tehdään nosto ja tallennetaan
                                if ((item.Saldo - nostettavaMaara) < 0)
                                {
                                    Console.WriteLine("Tilillä on vain " + item.Saldo + " e rahaa, nostoa ei voida suorittaa! \n");
                                }
                                else
                                {
                                    item.NostaRahaa(nostettavaMaara);
                                    pankki.TallennaKayttotili();
                                }
                            }
                        }
                        foreach (var item in pankki.ListaSaastotileista)
                        {
                            if (item.Omistaja == tilinOmistaja && item.tilinNimi == tiliName)
                            {
                                item.NostaRahaa(); // tämä metodi tuottaa tulosteen ettei säästötililtä saa nostaa rahaa.
                            }
                        }
                    }

                    else
                    {
                        Console.WriteLine("Kyseistä tiliä ei ole olemassa!");
                    }

                }

                else if (vastaus == "K")
                {
                    //tulostetaan näytölle kaikki tiedot oliodatasta: tilin omistaja, nimi ja saldo
                    Pankki pankki = new Pankki();
                    pankki.LataaKayttotilit();
                    pankki.LataaSaastotilit();

                    foreach (var item in pankki.ListaKayttotileista)
                    {
                        Console.WriteLine(item.Omistaja + ", " + item.tilinNimi + ", " + item.Saldo);
                    }
                    foreach (var item in pankki.ListaSaastotileista)
                    {
                        Console.WriteLine(item.Omistaja + ", " + item.tilinNimi + ", " + item.Saldo);
                    }
                }

                else if (vastaus == "P")
                {
                    ISailyttava possu = new Saastopossu();
                    possu.SailytaRahaa();  
                }

                else if (vastaus == "E")
                {
                    break;
                }

                else
                {
                    Console.WriteLine("Virheellinen syöte!");
                }

            } while (true);

        }
    }
}
