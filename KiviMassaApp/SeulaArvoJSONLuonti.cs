using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiviMassaApp
{
    public static class SeulaArvoJSONLuonti
    {
        public static void SeulaJSONLuonti()
        {
            List<Seulakirjasto> sl = new List<Seulakirjasto>();
            string json;
            double[] ar = new double[] { 200, 150, 125, 100, 90, 80, 63, 56, 50, 45, 40, 31.5, 25, 22.4, 20, 18, 16, 12.5, 11.5, 10, 8, 6.3, 5.6, 5, 4, 2, 1, 0.5, 0.25, 0.125, 0.063 };
            for (int i = 0; i < ar.Length; i++)
            {
                Seulakirjasto s = new Seulakirjasto();
                s.seula = ar[i];
                sl.Add(s);
            }
            json = JsonConvert.SerializeObject(sl);



            /*foreach (Seula e in sl)
            {
                Console.WriteLine(e.seula);
            }*/
            try
            {
                if (!Directory.Exists(@".\Asetukset"))
                {
                    Directory.CreateDirectory(@".\Asetukset");
                    File.WriteAllText(@".\Asetukset\Seulat.json", json);
                }
                else
                {
                    File.WriteAllText(@".\Asetukset\Seulat.json", json);
                }
                
            }catch(Exception e)
            {
                Console.WriteLine("VIRHE SeulaArvoJSONLuonti.cs TIEDOSTOSSA: KANSIORAKENTEESSA ONGELMA: "+e.Message+",   "+e.StackTrace);
            }
            
        }
        
    }
}
