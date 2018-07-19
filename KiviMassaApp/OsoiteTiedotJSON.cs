using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiviMassaApp
{
    class OsoiteTiedotJSON
    {
        public static void OsoiteJSONLuonti()
        {
            List<Osoitteet> _osoitteet = new List<Osoitteet>();

            string json;
            string[] array = new string[] { "Tekniikka", "Opistotie 2", "PL 88, 70101 KUOPIO", "(017) 255 6000" };

            for (int i = 0; i < array.Length; i++)
            {
                Osoitteet o = new Osoitteet();
                o.osoiteTieto = array[i];
                o.index = i;
                _osoitteet.Add(o);

            }
            json = JsonConvert.SerializeObject(_osoitteet);

            try
            {
                if (!Directory.Exists(@".\Asetukset"))
                {
                    Directory.CreateDirectory(@".\Asetukset");
                    File.WriteAllText(@".\Asetukset\Osoitetiedot.json", json);
                }
                else
                {
                    File.WriteAllText(@".\Asetukset\Osoitetiedot.json", json);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Virhe tiedoston luonnissa", ex.Message);
            }
        }
    }
}
