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
    public class Pankki
    {
        public List<Kayttotili> ListaKayttotileista { get; set; }
        public List<Saastotili> ListaSaastotileista { get; set; }
        

        public List<string> LueTilit() // luetaan tekstitiedostosta olemassa olevat tilit ja palautetaan lista tileistä
        {
            List<string> tilitiedot = new List<string>(File.ReadAllLines("tilitiedot.txt"));
            tilitiedot.Sort(); // järjestetään, jotta on helpompi löytää oikea tili
            return tilitiedot;
        }

        public void LuoTili(string omistaja, string tili) // luodaan tili = lisätään tilin tiedot tekstitiedostoon
        {
            List<string> tilitiedot = LueTilit();
            tilitiedot.Add(omistaja + ":" + tili);
            File.WriteAllLines("tilitiedot.txt", tilitiedot);
        }

        
        public void NaytaTilit() // tulostetaan olemassa olevat tilit näytölle
        {
            Console.WriteLine("Olemassa olevat tilit: \n");

            List<string> tilitiedot = LueTilit();
            for (int i = 0; i < tilitiedot.Count; i++)
            {
                Console.WriteLine("[ " + (i + 1) + " ] " + tilitiedot[i]);
            }
            Console.WriteLine("[ 0 ] Lopeta ohjelman käyttö\n");
        }


        // alla neljä metodia, joilla tallennetaan ja luetaan oliodataa tiedostoon ja tiedostosta
        public void TallennaKayttotili()
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = new FileStream("kayttotilit.bin", FileMode.Create, FileAccess.Write, FileShare.None);

            try
            {
                binaryFormatter.Serialize(fileStream, ListaKayttotileista);
            }
            catch
            {
                Console.WriteLine("Virhe tallennuksessa!");
            }
            finally
            {
                fileStream.Close();
            }
        }

        public void LataaKayttotilit()
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = new FileStream("kayttotilit.bin", FileMode.Open, FileAccess.Read, FileShare.None);

            try
            {
                ListaKayttotileista = (List<Kayttotili>)binaryFormatter.Deserialize(fileStream);
            }
            catch
            {
                Console.WriteLine("Virhe latauksessa!");
            }
            finally
            {
                fileStream.Close();
            }
        }

        public void TallennaSaastotili()
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = new FileStream("saastotilit.bin", FileMode.Create, FileAccess.Write, FileShare.None);

            try
            {
                binaryFormatter.Serialize(fileStream, ListaSaastotileista);
            }
            catch
            {
                Console.WriteLine("Virhe tallennuksessa!");
            }
            finally
            {
                fileStream.Close();
            }
        }

        public void LataaSaastotilit()
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = new FileStream("saastotilit.bin", FileMode.Open, FileAccess.Read, FileShare.None);

            try
            {
                ListaSaastotileista = (List<Saastotili>)binaryFormatter.Deserialize(fileStream);
            }
            catch
            {
                Console.WriteLine("Virhe latauksessa!");
            }
            finally
            {
                fileStream.Close();
            }
        }

  



    }
}
