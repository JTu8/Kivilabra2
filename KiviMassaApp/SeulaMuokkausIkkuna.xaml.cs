using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace KiviMassaApp
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class SeulaMuokkausIkkuna : Window
    {
        List<Seulakirjasto> seulalista = new List<Seulakirjasto>();
        MainWindow _main;
        public SeulaMuokkausIkkuna(Window main)
        {
            InitializeComponent();
            _main = (MainWindow)main;
            seulalista = SeulaJsonLataus();
            if (seulalista != null)
            {
                foreach (Seulakirjasto s in seulalista)
                {
                    //Console.WriteLine(s.seula);
                    lbSeulaLista.Items.Add(s.seula);
                }
            }
            else
            {
                SeulaArvoJSONLuonti.SeulaJSONLuonti();
                seulalista = SeulaJsonLataus();
                foreach (Seulakirjasto s in seulalista)
                {
                    //Console.WriteLine(s.seula);
                    lbSeulaLista.Items.Add(s.seula);
                }

            }
        }

        private void btnUusiSeula_Click(object sender, RoutedEventArgs e)
        {
            lbltallennusViestiMsg.Text = String.Empty;
            //Lisää syötetyn uuden seulan seulalistaan
            if(tbUusiSeula.Text != String.Empty)
            {
                string syote = tbUusiSeula.Text;
                double? uusi;
                try
                {
                    uusi = Convert.ToDouble(syote);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    uusi = null;
                }
                if (uusi.HasValue)
                {
                    seulalista.Add(new Seulakirjasto {seula = Convert.ToDouble(uusi)});
                    seulalista.Sort();
                    lbSeulaLista.Items.Clear();
                    foreach (Seulakirjasto s in seulalista)
                    {
                        //Console.WriteLine(s.seula);
                        
                        lbSeulaLista.Items.Add(s.seula);
                    }
                }

            }
        }

        private void btnPoistaSeula_Click(object sender, RoutedEventArgs e)
        {
            //Poistaa valitun seulan seulalistasta
            var valinta = lbSeulaLista.SelectedIndex;
            lbltallennusViestiMsg.Text = String.Empty;
            //Console.WriteLine("Poistovalinta: " + valinta);
            if (valinta != -1)
            {
                if(seulalista.Count > 1)
                {
                    seulalista.RemoveAt(valinta);
                    seulalista.Sort();
                    lbSeulaLista.Items.Clear();
                    foreach (Seulakirjasto s in seulalista)
                    {
                        //Console.WriteLine(s.seula);

                        lbSeulaLista.Items.Add(s.seula);
                    }
                }
                else
                {
                    lbltallennusViestiMsg.Text = "Listassa pitää olla\nvähintään yksi arvo!";
                }
               
            }
            else
            {
                lbltallennusViestiMsg.Text = "Valitse poistettava\narvo ensin!";
            }
            

        }        

        private void Window_Closed(object sender, EventArgs e)
        {
            _main.SuljeIkkuna("seula");
        }

        private List<Seulakirjasto> SeulaJsonLataus()
        {
            //Tarkistetaan onko Seulat.json tiedostoa ja sen isäntäkansiota olemassa
            //Jos on, luetaan seulatiedot tiedostosta
            //Jos ei, luodaan tiedosto ja kansio tarpeen mukaan ja luetaan seulat sitten.
            string seulajson;
            if (File.Exists(@".\Asetukset\Seulat.json") && Directory.Exists(@".\Asetukset"))
            {
                try
                {
                    StreamReader s = new StreamReader(@".\Asetukset\Seulat.json");
                    seulajson = s.ReadToEnd();
                    s.Close();
                    return JsonConvert.DeserializeObject<List<Seulakirjasto>>(seulajson);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Virhe Kiviohjelma.xaml.cs tiedostossa: Seulat.json tiedoston haussa virhe.  " + e.Message + ",   " + e.StackTrace);
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            //Kun syötetään arvo kenttään, tämä funktio varmistaa että vain numeroita ja pilkkuja voi laittaa kenttiin
            Regex regex = new Regex("^[,][0-9]+$|^[0-9]*[,]{0,1}[0-9]*$");
            e.Handled = !regex.IsMatch(e.Text);
        }

        private void btnTallenna_Click(object sender, RoutedEventArgs e)
        {
            //Tallennetaan seulalista Seulat.json tiedostoon
            //sijaitsee sijainnissa .\Asetukset\Seulat.json
            string json = JsonConvert.SerializeObject(seulalista);
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
                lbltallennusViestiMsg.Text = "Uudet seula-arvot\ntallennettu!";

            }
            catch (Exception ex)
            {
                MessageBox.Show("Virhe SeulaMuokkausIkkuna.xaml.cs tiedostossa: tiedoston kirjoittamisessa virhe:    " + ex.Message + ",  " + ex.StackTrace);
            }
        }

        private void btnPalautaDefault_Click(object sender, RoutedEventArgs e)
        {
            string defjson;
            if (File.Exists(@".\Asetukset\SeulatDefault.json") && Directory.Exists(@".\Asetukset"))
            {
                try
                {
                    StreamReader s = new StreamReader(@".\Asetukset\SeulatDefault.json");
                    defjson = s.ReadToEnd();
                    s.Close();
                    File.WriteAllText(@".\Asetukset\Seulat.json", defjson);

                    seulalista = SeulaJsonLataus();
                    seulalista.Sort();
                    lbSeulaLista.Items.Clear();
                    foreach (Seulakirjasto se in seulalista)
                    {
                        //Console.WriteLine(s.seula);

                        lbSeulaLista.Items.Add(se.seula);
                    }
                    lbltallennusViestiMsg.Text = "Alkuperäisarvot\npalautettu!";

                    //seulalista = JsonConvert.DeserializeObject<List<Seulakirjasto>>(json);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Virhe SeulaMuokkausIkkuna.xaml.cs tiedostossa: SeulatDefault.json tiedoston haussa virhe.\n  " + ex.Message + ",   " + ex.StackTrace);
                }
            }
            else
            {
                SeulaArvoJSONLuonti.SeulaJSONLuonti();
            }
        }
    }
}
