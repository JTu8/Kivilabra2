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
using Microsoft.Win32;
using System.Diagnostics;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace KiviMassaApp
{
    /// <summary>
    /// Interaction logic for Kiviohjelma.xaml
    /// </summary>
    public partial class Kiviohjelma : Window
    {
        

        MainWindow _main;
        public List<Seulakirjasto> seulalista = new List<Seulakirjasto>();
        public void GetSetSeulalista(List<Seulakirjasto> seli)
        {
            seulalista = seli;
        }
        public List<Seulakirjasto> GetSetSeulalista()
        {
            return seulalista;
        }
        List<Osoitteet> _osoitteet = new List<Osoitteet>();
        //private Window _pdfAsetukset;
        public Kiviohjelma(Window main)
        {
            InitializeComponent();
            _main = (MainWindow)main; //Otetaan aloitusikkuna talteen
            SeulaJsonLataus(); //Ladataan Seulat.json tiedostosta kaikki seula-arvot talteen seulalistaan
            OsoiteJsonLataus(); //Ladataan Osoitetiedot.json tiedostosta kaikki osoitetiedot listaan talteen
            GetOsoitteetToTextBoxes(); //Luetaan osoitetiedot listasta tekstikenttiin
            

            foreach (Control c in seulaArvot.Children)
            {
                if (c.GetType() == typeof(ComboBox)) //jos esineen tyyppi on combobox
                {
                    if (((ComboBox)c).Tag.ToString() != null) //Jos comboboxin tagi ei ole tyhjä
                    {
                        if (((ComboBox)c).Tag.ToString() == "seula") //jos comboboxin tagi on "seula" tai "jakoseula", eli kaikki seuladropdown-valikot
                        {
                            foreach (Seulakirjasto s in seulalista)
                            {
                                ((ComboBox)c).Items.Add(s.seula);//Listätään seulavaihtoehdot seulavalikkoihin
                                
                            }
                            ((ComboBox)c).Items.Add(String.Empty);
                        }
                    }

                }
            }
            //Asettaa joitain default arvoja seulakenttiin
            int jar = 0;
            foreach (Control c in seulaArvot.Children)
            {
                if (c.GetType() == typeof(ComboBox)) //jos esineen tyyppi on combobox
                {
                    if (((ComboBox)c).Tag.ToString() != null) //Jos comboboxin tagi ei ole tyhjä
                    {
                        if (((ComboBox)c).Tag.ToString() == "seula") //jos comboboxin tagi on "seula" tai "jakoseula", eli kaikki seuladropdown-valikot
                        {
                            if(((ComboBox)c).SelectedIndex == -1)
                            {
                                ((ComboBox)c).SelectedIndex = jar;
                                if ((seulalista.Count - 1 ) >= jar)
                                {
                                    jar++;
                                }
                                else
                                {
                                    ((ComboBox)c).Text = "Tyhjä";
                                }
                               
                            }
                        }
                    }

                }
            }

            /*Seula1.SelectedIndex = 0;
            Seula2.SelectedIndex = 1;
            Seula3.SelectedIndex = 4;
            Seula4.SelectedIndex = 6;
            Seula5.SelectedIndex = 7;
            Seula6.SelectedIndex = 9;
            Seula7.SelectedIndex = 11;
            Seula8.SelectedIndex = 12;
            Seula9.SelectedIndex = 14;
            Seula10.SelectedIndex = 16;
            Seula11.SelectedIndex = 19;
            Seula12.SelectedIndex = 20;
            Seula13.SelectedIndex = 22;
            Seula14.SelectedIndex = 23;
            Seula15.SelectedIndex = 25;
            Seula16.SelectedIndex = 28;
            Seula17.SelectedIndex = 29;
            Seula18.SelectedIndex = 30;*/

            AsetaJakoSeulaValikonArvot();
            SeulaArvotOhjeArvoihin(); //Laitetaan valitut seula-arvot Ohjealue-ruudun seulakenttiin
            rbKuivaseulonta.IsChecked = true;
        }
        
        private void Kiviohjelma_Closed(object sender, EventArgs e)
        {
            //kertoo aloitusikkunalle että ohjelma on suljettu
            //käytetään siihen että ei voi olla useampi kiviohjelma käynnissä yhtä aikaa
            _main.SuljeIkkuna("kivi");
        }

        public void EmptyFields() //Funktio mikä tyhjentää kaikki tekstikentät seulalaskenta-ruudusta
        {
            foreach (Control c in seulaArvot.Children)
            {
                if (c.GetType() == typeof(TextBox))
                {
                    ((TextBox)c).Text = String.Empty;
                }
            }
        }

        private void btnTyhjennaOhjealueet_Click(object sender, RoutedEventArgs e)
        {
            //Tyhjentää Ohjealue-ruudusta kaikki tekstikentät
            foreach (Control c in ohjeArvot.Children)
            {
                if (c.GetType() == typeof(TextBox))
                {
                    ((TextBox)c).Text = String.Empty;
                }
            }
            //Laittaa seulat takaisin Ohjealue-ruutuun
            SeulaArvotOhjeArvoihin();
        }

        KayraInitializer kayrainitializer = new KayraInitializer();
        private void btnNaytaKaavio_Click(object sender, RoutedEventArgs e) // Avataan käyräikkuna
        {
            kayrainitializer.KayraPiirto(this);
        }

        public void SuljeKayraIkkuna()
        {
            kayrainitializer.SuljeKayraIkkuna(); ;
        }



        private void btnLaske_Click(object sender, RoutedEventArgs e)
        {
            //Seulalaskurin Laske-napin toiminto
            //Ottaa syötetyt tiedot talteen ja suorittaa laskutoimitukset niille
            EmptyResultFields();//tyhjentää tuloskentät
            
            
            int jakoseulaindex = JakoSeula.SelectedIndex;
            int pyoristys = Convert.ToInt32(dbDesimaali.Text);//Otetaan valittu pyöristysarvo ohjelmasta
            List<SyotetytArvot> syotetytarvot = new List<SyotetytArvot>(); //Luo listan johon syötetyt arvot tallennetaan
            syotetytarvot = LuetaanSyotetytArvot(); //Luetaan syotetyt arvot luotuun listaan

            if (jakoseulaindex != 0)
            {
                double kerroin = 1;
                if (tbJakoKerroin.Text != String.Empty && Double.TryParse(tbJakoKerroin.Text, out double r) == true)
                {
                    kerroin = Convert.ToDouble(tbJakoKerroin.Text);
                }
                else
                {
                    kerroin = 1;
                }
                double kokomassa = LaskeKokonaisMassa(syotetytarvot, pyoristys, jakoseulaindex, kerroin);
                TulostenLasku_SeulalleJai(syotetytarvot, kokomassa, pyoristys, jakoseulaindex, kerroin);
                TulostenLasku_LapaisyProsentti(syotetytarvot, kokomassa, pyoristys, jakoseulaindex, kerroin);
                foreach (SyotetytArvot s in syotetytarvot)
                {
                    if (s.index >= jakoseulaindex)
                    {
                        switch (s.index)
                        {
                            case 1:
                                jaettuG0.Text = Math.Round((Convert.ToDouble(s.syote) * kerroin), pyoristys).ToString();
                                break;
                            case 2:
                                jaettuG1.Text = Math.Round((Convert.ToDouble(s.syote) * kerroin), pyoristys).ToString();
                                break;
                            case 3:
                                jaettuG2.Text = Math.Round((Convert.ToDouble(s.syote) * kerroin), pyoristys).ToString();
                                break;
                            case 4:
                                jaettuG3.Text = Math.Round((Convert.ToDouble(s.syote) * kerroin), pyoristys).ToString();
                                break;
                            case 5:
                                jaettuG4.Text = Math.Round((Convert.ToDouble(s.syote) * kerroin), pyoristys).ToString();
                                break;
                            case 6:
                                jaettuG5.Text = Math.Round((Convert.ToDouble(s.syote) * kerroin), pyoristys).ToString();
                                break;
                            case 7:
                                jaettuG6.Text = Math.Round((Convert.ToDouble(s.syote) * kerroin), pyoristys).ToString();
                                break;
                            case 8:
                                jaettuG7.Text = Math.Round((Convert.ToDouble(s.syote) * kerroin), pyoristys).ToString();
                                break;
                            case 9:
                                jaettuG8.Text = Math.Round((Convert.ToDouble(s.syote) * kerroin), pyoristys).ToString();
                                break;
                            case 10:
                                jaettuG9.Text = Math.Round((Convert.ToDouble(s.syote) * kerroin), pyoristys).ToString();
                                break;
                            case 11:
                                jaettuG10.Text = Math.Round((Convert.ToDouble(s.syote) * kerroin), pyoristys).ToString();
                                break;
                            case 12:
                                jaettuG11.Text = Math.Round((Convert.ToDouble(s.syote) * kerroin), pyoristys).ToString();
                                break;
                            case 13:
                                jaettuG12.Text = Math.Round((Convert.ToDouble(s.syote) * kerroin), pyoristys).ToString();
                                break;
                            case 14:
                                jaettuG13.Text = Math.Round((Convert.ToDouble(s.syote) * kerroin), pyoristys).ToString();
                                break;
                            case 15:
                                jaettuG14.Text = Math.Round((Convert.ToDouble(s.syote) * kerroin), pyoristys).ToString();
                                break;
                            case 16:
                                jaettuG15.Text = Math.Round((Convert.ToDouble(s.syote) * kerroin), pyoristys).ToString();
                                break;
                            case 17:
                                jaettuG16.Text = Math.Round((Convert.ToDouble(s.syote) * kerroin), pyoristys).ToString();
                                break;
                            case 18:
                                jaettuG17.Text = Math.Round((Convert.ToDouble(s.syote) * kerroin), pyoristys).ToString();
                                break;
                            case 19:
                                jaettuG18.Text = Math.Round((Convert.ToDouble(s.syote) * kerroin), pyoristys).ToString();
                                break;
                            default:
                                {
                                    Console.WriteLine("VIRHE Kiviohjelma.xaml.cs tiedostossa: kerrotun jaetun näytteen sijoittelussa tuli virhe");
                                    break;
                                }
                        }
                    }
                }
                Laskut l = new Laskut();
                if (markapaino.Text != String.Empty)
                {
                    //Lasketaan kosteusprosentti jos märkäpaino-kentään on syötetty arvo
                    tbKosteuspros.Text = Math.Round(l.KosteusProsentti(kokomassa, Convert.ToDouble(markapaino.Text)), pyoristys).ToString();
                }
                if (rbKuivaseulonta.IsChecked == true)
                {
                    tbPesutappio.Text = String.Empty;
                }
                else if (rbPesuseulonta.IsChecked == true)
                {
                    double? pesupaino = null, kuivapaino = null;
                    double pesutappio;
                    if (tbPesupaino.Text != String.Empty && Double.TryParse(tbPesupaino.Text, out r) == true)
                    {
                        pesupaino = Convert.ToDouble(tbPesupaino.Text);
                    }
                    if (tbKuivapaino.Text != String.Empty && Double.TryParse(tbKuivapaino.Text, out r) == true)
                    {
                        kuivapaino = Convert.ToDouble(tbKuivapaino.Text);
                    }

                    if (kuivapaino.HasValue == true)
                    {
                        if (pesupaino.HasValue == true)
                        {
                            pesutappio = Math.Round(Convert.ToDouble((kuivapaino - pesupaino)), pyoristys);
                        }
                        else
                        {
                            pesutappio = Math.Round(Convert.ToDouble(kuivapaino - Convert.ToDouble(punnittuYhteensa.Text)), pyoristys);
                            tbPesupaino.Text = punnittuYhteensa.Text;
                        }
                        tbPesutappio.Text = pesutappio.ToString();
                        if (jaettuG18.Text == String.Empty)
                        {
                            double sl = 0;
                            if (seulaG18.Text != String.Empty)
                            {
                                sl = Convert.ToDouble(seulaG18.Text);
                            }
                            seulaG18.Text = Math.Round((sl + pesutappio), pyoristys).ToString();
                        }
                        else
                        {
                            double sl = 0;
                            if (jaettuG18.Text != String.Empty)
                            {
                                sl = Convert.ToDouble(jaettuG18.Text);
                            }
                            jaettuG18.Text = Math.Round((sl + pesutappio), pyoristys).ToString();
                        }
                        double py = Convert.ToDouble(punnittuYhteensa.Text);
                        py = Math.Round((py + pesutappio), pyoristys);
                        punnittuYhteensa.Text = py.ToString();
                    }
                }

            }
            else
            {
                double kokomassa = LaskeKokonaisMassa(syotetytarvot, pyoristys, 0, 1); //lasketaan arvojen kokonaismäärä
                TulostenLasku_SeulalleJai(syotetytarvot, kokomassa, pyoristys, 0, 1); //Lasketaan laskutoimituksia ja syötetään tulokset tuloskenttiin
                TulostenLasku_LapaisyProsentti(syotetytarvot, kokomassa, pyoristys, 0, 1);//--------||----------
                Laskut l = new Laskut();
                if (markapaino.Text != String.Empty)
                {
                    //Lasketaan kosteusprosentti jos märkäpaino-kentään on syötetty arvo
                    tbKosteuspros.Text = Math.Round(l.KosteusProsentti(kokomassa, Convert.ToDouble(markapaino.Text)), pyoristys).ToString();
                }
                if (rbKuivaseulonta.IsChecked == true)
                {
                    tbPesutappio.Text = String.Empty;
                }
                else if (rbPesuseulonta.IsChecked == true)
                {
                    double? pesupaino = null, kuivapaino = null;
                    double pesutappio;
                    if(tbPesupaino.Text != String.Empty && Double.TryParse(tbPesupaino.Text, out double r) == true)
                    {
                        pesupaino = Convert.ToDouble(tbPesupaino.Text);
                    }
                    if (tbKuivapaino.Text != String.Empty && Double.TryParse(tbKuivapaino.Text, out r) == true)
                    {
                        kuivapaino = Convert.ToDouble(tbKuivapaino.Text);
                        
                    }
                    
                    if(kuivapaino.HasValue == true)
                    {
                        if(pesupaino.HasValue == true)
                        {
                            pesutappio = Math.Round(Convert.ToDouble((kuivapaino - pesupaino)), pyoristys);
                        }
                        else
                        {
                            pesutappio = Math.Round(Convert.ToDouble(kuivapaino - Convert.ToDouble(punnittuYhteensa.Text)),pyoristys);
                            tbPesupaino.Text = punnittuYhteensa.Text;
                        }
                        tbPesutappio.Text = pesutappio.ToString();
                        if(jaettuG18.Text == String.Empty)
                        {
                            double sl = 0;
                            if (seulaG18.Text != String.Empty)
                            {
                                sl = Convert.ToDouble(seulaG18.Text);
                            }
                            seulaG18.Text = Math.Round((sl + pesutappio),pyoristys).ToString();
                        }
                        else
                        {
                            double sl = 0;
                            if(jaettuG18.Text != String.Empty)
                            {
                                sl = Convert.ToDouble(jaettuG18.Text);
                            }
                            jaettuG18.Text = Math.Round((sl + pesutappio),pyoristys).ToString();
                        }
                        double py = Convert.ToDouble(punnittuYhteensa.Text);
                        py = Math.Round((py + pesutappio), pyoristys);
                        punnittuYhteensa.Text = py.ToString();
                    }
                }
            }
        }

        

        private void TulostenLasku_LapaisyProsentti(List<SyotetytArvot> sa, double kokomassa, int pyoristys, int jakoindex, double kerroin)
        {
            //Luodaan lista jonne tulokset laitetaan
            List<SyotetytArvot> tulos = new List<SyotetytArvot>();
            Laskut laskut = new Laskut();
            //Lasketaan ja syötetään tulokset tulos-listaan
            tulos = laskut.LapaisyProsentti(sa, kokomassa, jakoindex, kerroin);
            //Käydään lista läpi ja tulostetaan tulokset oikeille kentille
            if(jakoindex >= 0)
            {
                foreach (SyotetytArvot s in tulos)
                {
                    switch (s.index)
                    {
                        case 1:
                            lapaisypros1.Text = Math.Round(Convert.ToDouble(s.syote), pyoristys).ToString();
                            break;
                        case 2:
                            lapaisypros2.Text = Math.Round(Convert.ToDouble(s.syote), pyoristys).ToString();
                            break;
                        case 3:
                            lapaisypros3.Text = Math.Round(Convert.ToDouble(s.syote), pyoristys).ToString();
                            break;
                        case 4:
                            lapaisypros4.Text = Math.Round(Convert.ToDouble(s.syote), pyoristys).ToString();
                            break;
                        case 5:
                            lapaisypros5.Text = Math.Round(Convert.ToDouble(s.syote), pyoristys).ToString();
                            break;
                        case 6:
                            lapaisypros6.Text = Math.Round(Convert.ToDouble(s.syote), pyoristys).ToString();
                            break;
                        case 7:
                            lapaisypros7.Text = Math.Round(Convert.ToDouble(s.syote), pyoristys).ToString();
                            break;
                        case 8:
                            lapaisypros8.Text = Math.Round(Convert.ToDouble(s.syote), pyoristys).ToString();
                            break;
                        case 9:
                            lapaisypros9.Text = Math.Round(Convert.ToDouble(s.syote), pyoristys).ToString();
                            break;
                        case 10:
                            lapaisypros10.Text = Math.Round(Convert.ToDouble(s.syote), pyoristys).ToString();
                            break;
                        case 11:
                            lapaisypros11.Text = Math.Round(Convert.ToDouble(s.syote), pyoristys).ToString();
                            break;
                        case 12:
                            lapaisypros12.Text = Math.Round(Convert.ToDouble(s.syote), pyoristys).ToString();
                            break;
                        case 13:
                            lapaisypros13.Text = Math.Round(Convert.ToDouble(s.syote), pyoristys).ToString();
                            break;
                        case 14:
                            lapaisypros14.Text = Math.Round(Convert.ToDouble(s.syote), pyoristys).ToString();
                            break;
                        case 15:
                            lapaisypros15.Text = Math.Round(Convert.ToDouble(s.syote), pyoristys).ToString();
                            break;
                        case 16:
                            lapaisypros16.Text = Math.Round(Convert.ToDouble(s.syote), pyoristys).ToString();
                            break;
                        case 17:
                            lapaisypros17.Text = Math.Round(Convert.ToDouble(s.syote), pyoristys).ToString();
                            break;
                        case 18:
                            lapaisypros18.Text = Math.Round(Convert.ToDouble(s.syote), pyoristys).ToString();
                            break;
                        default:
                            {
                                Console.WriteLine("VIRHE Kiviohjelma.xaml.cs tiedostossa: lapaisypros-kenttiä täydentäessä tuli vastaan outo index luku SyotetytArvot-listasta 'tulos'");
                                break;
                            }
                    }
                }
            }
        }

        private void TulostenLasku_SeulalleJai(List<SyotetytArvot> sa, double kokomassa, int pyoristys, int jakoindex, double kerroin)
        {
            if(jakoindex == 0)
            {
                for (int l = 0; l < sa.Count; l++)
                {
                    //otetaan ja lasketaan yksi tulos
                    Laskut laskut = new Laskut();
                    double tulos = Math.Round(laskut.seulalleJai(Convert.ToDouble(sa[l].syote), kokomassa), pyoristys);

                    //Valitaan oikea paikka tulokselle sen indeksin perusteella
                    switch (sa[l].index)
                    {
                        case 1:
                            seulapros1.Text = tulos.ToString();
                            break;
                        case 2:
                            seulapros2.Text = tulos.ToString();
                            break;
                        case 3:
                            seulapros3.Text = tulos.ToString();
                            break;
                        case 4:
                            seulapros4.Text = tulos.ToString();
                            break;
                        case 5:
                            seulapros5.Text = tulos.ToString();
                            break;
                        case 6:
                            seulapros6.Text = tulos.ToString();
                            break;
                        case 7:
                            seulapros7.Text = tulos.ToString();
                            break;
                        case 8:
                            seulapros8.Text = tulos.ToString();
                            break;
                        case 9:
                            seulapros9.Text = tulos.ToString();
                            break;
                        case 10:
                            seulapros10.Text = tulos.ToString();
                            break;
                        case 11:
                            seulapros11.Text = tulos.ToString();
                            break;
                        case 12:
                            seulapros12.Text = tulos.ToString();
                            break;
                        case 13:
                            seulapros13.Text = tulos.ToString();
                            break;
                        case 14:
                            seulapros14.Text = tulos.ToString();
                            break;
                        case 15:
                            seulapros15.Text = tulos.ToString();
                            break;
                        case 16:
                            seulapros16.Text = tulos.ToString();
                            break;
                        case 17:
                            seulapros17.Text = tulos.ToString();
                            break;
                        case 18:
                            seulapros18.Text = tulos.ToString();
                            break;
                        case 19:
                            seulapros19.Text = tulos.ToString();
                            break;
                        default:
                            {
                                Console.WriteLine("VIRHE Kiviohjelma.xaml.cs tiedostossa: seulapros-kenttiä täydentäessä tuli vastaan outo index luku SyotetytArvot-listasta");
                                break;
                            }
                    }
                }
            }
            else
            {
                for (int l = 0; l < sa.Count; l++)
                {
                    //otetaan ja lasketaan yksi tulos
                    Laskut laskut = new Laskut();
                    double tulos = laskut.seulalleJai(Convert.ToDouble(sa[l].syote), kokomassa);

                    //Valitaan oikea paikka tulokselle sen indeksin perusteella
                    switch (sa[l].index)
                    {
                        case 1:
                            if(sa[l].index >= jakoindex)
                            {
                                seulapros1.Text = Math.Round((tulos * kerroin), pyoristys).ToString();
                            }
                            else
                            {
                                seulapros1.Text = Math.Round(tulos, pyoristys).ToString();
                            }
                            break;
                        case 2:
                            if (sa[l].index >= jakoindex)
                            {
                                seulapros2.Text = Math.Round((tulos * kerroin), pyoristys).ToString();
                            }
                            else
                            {
                                seulapros2.Text = Math.Round(tulos, pyoristys).ToString();
                            }
                            break;
                        case 3:
                            if (sa[l].index >= jakoindex)
                            {
                                seulapros3.Text = Math.Round((tulos * kerroin), pyoristys).ToString();
                            }
                            else
                            {
                                seulapros3.Text = Math.Round(tulos, pyoristys).ToString();
                            }
                            break;
                        case 4:
                            if (sa[l].index >= jakoindex)
                            {
                                seulapros4.Text = Math.Round((tulos * kerroin), pyoristys).ToString();
                            }
                            else
                            {
                                seulapros4.Text = Math.Round(tulos, pyoristys).ToString();
                            }
                            break;
                        case 5:
                            if (sa[l].index >= jakoindex)
                            {
                                seulapros5.Text = Math.Round((tulos * kerroin), pyoristys).ToString();
                            }
                            else
                            {
                                seulapros5.Text = Math.Round(tulos, pyoristys).ToString();
                            }
                            break;
                        case 6:
                            if (sa[l].index >= jakoindex)
                            {
                                seulapros6.Text = Math.Round((tulos * kerroin), pyoristys).ToString();
                            }
                            else
                            {
                                seulapros6.Text = Math.Round(tulos, pyoristys).ToString();
                            }
                            break;
                        case 7:
                            if (sa[l].index >= jakoindex)
                            {
                                seulapros7.Text = Math.Round((tulos * kerroin), pyoristys).ToString();
                            }
                            else
                            {
                                seulapros7.Text = Math.Round(tulos, pyoristys).ToString();
                            }
                            break;
                        case 8:
                            if (sa[l].index >= jakoindex)
                            {
                                seulapros8.Text = Math.Round((tulos * kerroin), pyoristys).ToString();
                            }
                            else
                            {
                                seulapros8.Text = Math.Round(tulos, pyoristys).ToString();
                            }
                            break;
                        case 9:
                            if (sa[l].index >= jakoindex)
                            {
                                seulapros9.Text = Math.Round((tulos * kerroin), pyoristys).ToString();
                            }
                            else
                            {
                                seulapros9.Text = Math.Round(tulos, pyoristys).ToString();
                            }
                            break;
                        case 10:
                            if (sa[l].index >= jakoindex)
                            {
                                seulapros10.Text = Math.Round((tulos * kerroin), pyoristys).ToString();
                            }
                            else
                            {
                                seulapros10.Text = Math.Round(tulos, pyoristys).ToString();
                            }
                            break;
                        case 11:
                            if (sa[l].index >= jakoindex)
                            {
                                seulapros11.Text = Math.Round((tulos * kerroin), pyoristys).ToString();
                            }
                            else
                            {
                                seulapros11.Text = Math.Round(tulos, pyoristys).ToString();
                            }
                            break;
                        case 12:
                            if (sa[l].index >= jakoindex)
                            {
                                seulapros12.Text = Math.Round((tulos * kerroin), pyoristys).ToString();
                            }
                            else
                            {
                                seulapros12.Text = Math.Round(tulos, pyoristys).ToString();
                            }
                            break;
                        case 13:
                            if (sa[l].index >= jakoindex)
                            {
                                seulapros13.Text = Math.Round((tulos * kerroin), pyoristys).ToString();
                            }
                            else
                            {
                                seulapros13.Text = Math.Round(tulos, pyoristys).ToString();
                            }
                            break;
                        case 14:
                            if (sa[l].index >= jakoindex)
                            {
                                seulapros14.Text = Math.Round((tulos * kerroin), pyoristys).ToString();
                            }
                            else
                            {
                                seulapros14.Text = Math.Round(tulos, pyoristys).ToString();
                            }
                            break;
                        case 15:
                            if (sa[l].index >= jakoindex)
                            {
                                seulapros15.Text = Math.Round((tulos * kerroin), pyoristys).ToString();
                            }
                            else
                            {
                                seulapros15.Text = Math.Round(tulos, pyoristys).ToString();
                            }
                            break;
                        case 16:
                            if (sa[l].index >= jakoindex)
                            {
                                seulapros16.Text = Math.Round((tulos * kerroin), pyoristys).ToString();
                            }
                            else
                            {
                                seulapros16.Text = Math.Round(tulos, pyoristys).ToString();
                            }
                            break;
                        case 17:
                            if (sa[l].index >= jakoindex)
                            {
                                seulapros17.Text = Math.Round((tulos * kerroin), pyoristys).ToString();
                            }
                            else
                            {
                                seulapros17.Text = Math.Round(tulos, pyoristys).ToString();
                            }
                            break;
                        case 18:
                            if (sa[l].index >= jakoindex)
                            {
                                seulapros18.Text = Math.Round((tulos * kerroin), pyoristys).ToString();
                            }
                            else
                            {
                                seulapros18.Text = Math.Round(tulos, pyoristys).ToString();
                            }
                            break;
                        case 19:
                            if (sa[l].index >= jakoindex)
                            {
                                seulapros19.Text = Math.Round((tulos * kerroin), pyoristys).ToString();
                            }
                            else
                            {
                                seulapros19.Text = Math.Round(tulos, pyoristys).ToString();
                            }
                            break;
                        default:
                            {
                                Console.WriteLine("VIRHE Kiviohjelma.xaml.cs tiedostossa: seulapros-kenttiä täydentäessä tuli vastaan outo index luku SyotetytArvot-listasta");
                                break;
                            }
                    }
                }
            }
            
        }

        private double LaskeKokonaisMassa(List<SyotetytArvot> sa, int pyoristys, int jakoindex, double kerroin)
        {
            //laskee syotettyjen arvojen kokonaismassan ja palauttaa sen
            //Listassa on arvot jotka lasketaan yhteen, ja pyoristys-parametri kertoo kuinka tarkasti arvo pyöristetään
            double kokomassa = 0;
            if (jakoindex == 0)
            {
                foreach (SyotetytArvot se in sa)
                {
                    kokomassa += Convert.ToDouble(se.syote);
                }
                punnittuYhteensa.Text = Math.Round(kokomassa, pyoristys).ToString();
                return Math.Round(kokomassa, pyoristys);
            }
            else
            {
                foreach (SyotetytArvot se in sa)
                {
                    if (se.index >= jakoindex)
                    {
                        kokomassa += Convert.ToDouble((se.syote * kerroin));
                        
                    }
                    else
                    {
                        kokomassa += Convert.ToDouble(se.syote);
                    }
                }
                punnittuYhteensa.Text = Math.Round(kokomassa, pyoristys).ToString();
                return Math.Round(kokomassa, pyoristys);
            }
           
        }


        private List<SyotetytArvot> LuetaanSyotetytArvot()
        {
            //Lukee käyttäjän syöttämät arvot, lisää ne listaan ja palauttaa listan
            //Syötetyissä arvoissa saattaa olla välejä (kaikkeja rivejä ei täytetty) joten koodi tarkistaa sen myös
            //Koodissa käydään myös läpi se, että käytetäänkö jaettuja näytteitä (eli onko valittu seulaa jakoseulalistasta ja onko asetettu kerrointa)
            List<SyotetytArvot> sa = new List<SyotetytArvot>();
            foreach (Control c in seulaArvot.Children) //Kaikille esineille seulaArvot-canvasissa. Tarkoituksena ottaa syötetyt arvot talteen
            {
                if (c.GetType() == typeof(TextBox)) //jos esineen tyyppi on textbox
                {
                    if (((TextBox)c).Tag.ToString() != null) //Jos textboxin tagi ei ole tyhjä
                    {
                        if (((TextBox)c).Tag.ToString() == "arvo") //jos textboxin tagi on "arvo"
                        {
                            if (((TextBox)c).Text != String.Empty && Double.TryParse(((TextBox)c).Text, out double r) == true)
                            {
                                SyotetytArvot s = new SyotetytArvot();
                                string seulatxt = ((TextBox)c).Text;//otetaan valittu syöte talteen
                                seulatxt = seulatxt.Replace(".", ",");//Korvataan pisteet pilkuilla
                                double g = Convert.ToDouble(seulatxt);
                                string n = ((TextBox)c).Name; //otetaan objektin nimi talteen
                                int index = Convert.ToInt32(Regex.Match(n, @"\d+$").Value);//otetaan objektin järjestysnumero nimestä
                                s.syote = g;
                                s.index = (index + 1);
                                sa.Add(s);
                            }

                        }
                    }

                }
            }
            /*Console.WriteLine("Perus");
            foreach (SyotetytArvot s in sa)
            {
                Console.WriteLine(s.index+", "+s.syote);
            }*/
            return sa;  
        }


        private void rbPesuseulonta_Checked(object sender, RoutedEventArgs e)
        {
            tbKuivapaino.IsEnabled = true;
            tbPesupaino.IsEnabled = true;
            tbPesutappio.IsEnabled = true;
        }
        private void rbKuivaseulonta_Checked(object sender, RoutedEventArgs e)
        {
            tbKuivapaino.IsEnabled = false;
            tbPesupaino.IsEnabled = false;
            tbPesutappio.IsEnabled = false;
        }

        private void SeulaJsonLataus()
        {

            //Tarkistetaan onko Seulat.json tiedostoa ja sen isäntäkansiota olemassa
            //Jos on, luetaan seulatiedot tiedostosta listaan
            //Jos ei, luodaan tiedosto ja kansio tarpeen mukaan ja luetaan seulat sitten.
            string seulajson;
            if (File.Exists(@".\Asetukset\Seulat.json") && Directory.Exists(@".\Asetukset"))
            {
                try
                {
                    StreamReader s = new StreamReader(@".\Asetukset\Seulat.json");
                    seulajson = s.ReadToEnd();
                    s.Close();
                    seulalista = JsonConvert.DeserializeObject<List<Seulakirjasto>>(seulajson);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Virhe Kiviohjelma.xaml.cs tiedostossa: Seulat.json tiedoston haussa virhe.  " + e.Message + ",   " + e.StackTrace);
                }
            }
            else if ((!Directory.Exists(@".\Asetukset")) || (Directory.Exists(@".\Asetukset") && !File.Exists(@".\Asetukset\Seulat.json")))
            {
                if (!Directory.Exists(@".\Asetukset"))
                {
                    Directory.CreateDirectory(@".\Asetukset");
                }
                SeulaArvoJSONLuonti.SeulaJSONLuonti();
                SeulaJsonLataus();
            }
        }

        private void OsoiteJsonLataus()
        {
            //Tarkistetaan onko Osoitetiedot.json tiedostoa ja sen isäntäkansiota olemassa
            //Jos on, luetaan seulatiedot tiedostosta listaan
            //Jos ei, luodaan tiedosto ja kansio tarpeen mukaan ja luetaan seulat sitten.
            string osoitejson;
            if (File.Exists(@".\Asetukset\Osoitetiedot.json") && Directory.Exists(@".\Asetukset"))
            {
                try
                {
                    StreamReader s = new StreamReader(@".\Asetukset\Osoitetiedot.json");
                    osoitejson = s.ReadToEnd();
                    s.Close();
                    _osoitteet = JsonConvert.DeserializeObject<List<Osoitteet>>(osoitejson);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Virhe Kiviohjelma.xaml.cs tiedostossa: Seulat.json tiedoston haussa virhe.  " + e.Message + ",  " + e.StackTrace);
                }
            }
            else if ((!Directory.Exists(@".\Asetukset")) || (Directory.Exists(@".\Asetukset") && !File.Exists(@".\Asetukset\Osoitetiedot.json")))
            {
                if (!Directory.Exists(@".\Asetukset"))
                {
                    Directory.CreateDirectory(@".\Asetukset");
                }
                OsoiteTiedotJSON.OsoiteJSONLuonti();
                OsoiteJsonLataus();
            }


        }

        private void GetOsoitteetToTextBoxes() //Lataa osoitetiedot JSON-tiedostosta tekstikenttiin
        {

            for (int i = 0; i < _osoitteet.Count; i++)
            {
                alempiOtsikko.Text = _osoitteet[0].osoiteTieto;
                lahiosoite.Text = _osoitteet[1].osoiteTieto;
                osoite.Text = _osoitteet[2].osoiteTieto;
                puh.Text = _osoitteet[3].osoiteTieto;
            }


        }

        private void tallennaOsoitteet_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < _osoitteet.Count; i++)
            {
                _osoitteet[0].osoiteTieto = alempiOtsikko.Text;
                _osoitteet[1].osoiteTieto = lahiosoite.Text;
                _osoitteet[2].osoiteTieto = osoite.Text;
                _osoitteet[3].osoiteTieto = puh.Text;
            }

            string json = JsonConvert.SerializeObject(_osoitteet);
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
                MessageBox.Show("Tiedot tallennettu");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Tietojen tallennus epäonnistui" + ex.Message);
            }
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            //Kun syötetään tietoja seulakenttiin, tämä funktio varmistaa että vain numeroita ja pilkkuja voi laittaa kenttiin
            Regex regex = new Regex("^[,][0-9]+$|^[0-9]*[,]{0,1}[0-9]*$");
            e.Handled = !regex.IsMatch(e.Text);
        }
        private void CommandBinding_Open(object sender, RoutedEventArgs e)
        {
            Open(1);
        }

        private void LataaKaikki_Click(object sender, RoutedEventArgs e)
        {
            Open(1);
        }

        private void LataaSeula_Click(object sender, RoutedEventArgs e)
        {
            Open(2);
        }

        private void LataaOhje_Click(object sender, RoutedEventArgs e)
        {
            Open(3);
        }

        private void LataaTiedot_Click(object sender, RoutedEventArgs e)
        {
            Open(4);
        }
        private void Open(int mode) //Avaa File Explorerin, jonka avulla ladataan tiedostoja
        {
            //mode: 1 = lataa kaikki, 2 = lataa seulaAlueen tiedot, 3 = lataa ohjealueet, 4 = lataa tekstitiedot
            Stream stream = null;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.Filter = "Tallennustiedosto (JSON) (*.json)|*.json";
            openFileDialog.Title = "Avaa tallennus";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (openFileDialog.ShowDialog() == true)
            {
                
                try
                {
                    if ((stream = openFileDialog.OpenFile()) != null)
                    {
                        using (stream)
                        {
                            StreamReader sr = new StreamReader(stream);
                            string json = sr.ReadToEnd();
                            SaveLoadFunc load = new SaveLoadFunc();
                            load.LoadAll(this, json, mode);
                            
                        }

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Tiedoston avaaminen epäonnistui" + ex.Message);
                }
            }


        }

        private void SaveFile_Click(object sender, RoutedEventArgs e)
        {
            //Jos painaa tallennusnappia, kutsutaan tallennusfunktiota joka on alla
            Save();
        }

        private void Save()
        {
            //Avataan tallennusdialogi ja tallennetaan tiedosto haluamaan sijaintiin
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Tallennustiedosto (JSON) (*.json)|*.json";
            saveFileDialog.Title = "Tallenna nykyinen sessio";
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            SaveLoadFunc sa = new SaveLoadFunc();
            string json = sa.Save(this, seulalista);

            if (saveFileDialog.ShowDialog() == true)
            {
                if (saveFileDialog.FileName != String.Empty || saveFileDialog.FileName != "")
                {
                    //FileStream fs = (FileStream)saveFileDialog.OpenFile();
                    StreamWriter s = new StreamWriter(saveFileDialog.FileName);
                    s.WriteLine(json);
                    //fs.Close();
                    s.Close();
                }
            }
        }

        private void CommandBinding_Save(object sender, ExecutedRoutedEventArgs e)
        {
            //kun painaa pikanäppäinyhdistelmää (Ctrl + S), avataan tallennusdialogi
            Save();
        }
        private void ExitProgram_Click(object sender, RoutedEventArgs e)
        {
            //Sulkee ikkunan
            this.Close();
        }

        private void CreatePDF_Click(object sender, RoutedEventArgs e) //Luo PDF menupainikkeen Click funktio
        {

            KiviohjelmaPDF pdf = new KiviohjelmaPDF();
            pdf.SavePDF(this);
        }
        private void btnKuvaTesti_Click(object sender, RoutedEventArgs e)
        {
            //Testaa kuvan luontia viivakaaviosta
            /*KayraImage k = new KayraImage();
            MemoryStream mem = k.KayraKuva(this);
            System.Drawing.Image img;
            img = System.Drawing.Image.FromStream(mem);
            img.Save(@".\Asetukset\kuvat\kayrakuva.png",System.Drawing.Imaging.ImageFormat.Png);*/

            //Testaa millainen tallennustiedosto luodaan
            SaveLoadFunc sa = new SaveLoadFunc();
            string json = sa.Save(this, seulalista);
            File.WriteAllText(@".\Asetukset\TestiTallennus.json", json);
        }
        

        private void Seula_DropDownClosed(object sender, EventArgs e)
        {
            //Kun valitsee uuden seulan seuladropdown valikoista, päivitetään seula-arvot ohjearvojen seuloihin
            AsetaJakoSeulaValikonArvot();
            SeulaArvotOhjeArvoihin();
        }
        public void SeulaArvotOhjeArvoihin()
        {
            //Kopioi valitut seula-arvot Ohjealue-ruudun seulakenttiin
            if(Seula1.Text != String.Empty)
            {
                seulaValue1.Text = Seula1.Text.Trim();
            }
            else
            {
                seulaValue1.Text = "Tyhjä";
            }

            if (Seula2.Text != String.Empty)
            {
                seulaValue2.Text = Seula2.Text.Trim();
            }
            else
            {
                seulaValue2.Text = "Tyhjä";
            }
            if(Seula3.Text != String.Empty)
            {
                seulaValue3.Text = Seula3.Text.Trim();
            }
            else
            {
                seulaValue3.Text = "Tyhjä";
            }
            if (Seula4.Text != String.Empty)
            {
                seulaValue4.Text = Seula4.Text.Trim();
            }
            else
            {
                seulaValue4.Text = "Tyhjä";
            }
            if (Seula5.Text != String.Empty)
            {
                seulaValue5.Text = Seula5.Text.Trim();
            }
            else
            {
                seulaValue5.Text = "Tyhjä";
            }
            if (Seula6.Text != String.Empty)
            {
                seulaValue6.Text = Seula6.Text.Trim();
            }
            else
            {
                seulaValue6.Text = "Tyhjä";
            }
            if (Seula7.Text != String.Empty)
            {
                seulaValue7.Text = Seula7.Text.Trim();
            }
            else
            {
                seulaValue7.Text = "Tyhjä";
            }
            if (Seula8.Text != String.Empty)
            {
                seulaValue8.Text = Seula8.Text.Trim();
            }
            else
            {
                seulaValue8.Text = "Tyhjä";
            }
            if (Seula9.Text != String.Empty)
            {
                seulaValue9.Text = Seula9.Text.Trim();
            }
            else
            {
                seulaValue9.Text = "Tyhjä";
            }
            if (Seula10.Text != String.Empty)
            {
                seulaValue10.Text = Seula10.Text.Trim();
            }
            else
            {
                seulaValue10.Text = "Tyhjä";
            }
            if (Seula11.Text != String.Empty)
            {
                seulaValue11.Text = Seula11.Text.Trim();
            }
            else
            {
                seulaValue11.Text = "Tyhjä";
            }
            if (Seula12.Text != String.Empty)
            {
                seulaValue12.Text = Seula12.Text.Trim();
            }
            else
            {
                seulaValue12.Text = "Tyhjä";
            }
            if (Seula13.Text != String.Empty)
            {
                seulaValue13.Text = Seula13.Text.Trim();
            }
            else
            {
                seulaValue13.Text = "Tyhjä";
            }
            if (Seula14.Text != String.Empty)
            {
                seulaValue14.Text = Seula14.Text.Trim();
            }
            else
            {
                seulaValue14.Text = "Tyhjä";
            }
            if (Seula15.Text != String.Empty)
            {
                seulaValue15.Text = Seula15.Text.Trim();
            }
            else
            {
                seulaValue15.Text = "Tyhjä";
            }
            if (Seula16.Text != String.Empty)
            {
                seulaValue16.Text = Seula16.Text.Trim();
            }
            else
            {
                seulaValue16.Text = "Tyhjä";
            }
            if (Seula17.Text != String.Empty)
            {
                seulaValue17.Text = Seula17.Text.Trim();
            }
            else
            {
                seulaValue17.Text = "Tyhjä";
            }
            if (Seula18.Text != String.Empty)
            {
                seulaValue18.Text = Seula18.Text.Trim();
            }
            else
            {
                seulaValue18.Text = "Tyhjä";
            }
        }
        public void AsetaJakoSeulaValikonArvot()
        {
            List<Seulakirjasto> js = new List<Seulakirjasto>(); //Laitetaan jakoseula-dropdown valikkoon seulavalikoissa valitut arvot
            JakoSeula.Items.Clear();
            JakoSeula.Items.Add("Ei jakoa");
            foreach (Control c in seulaArvot.Children)
            {
                if (c.GetType() == typeof(ComboBox)) //jos esineen tyyppi on combobox
                {
                    if (((ComboBox)c).Tag.ToString() != null) //Jos comboboxin tagi ei ole tyhjä
                    {
                        if (((ComboBox)c).Tag.ToString() == "seula") //jos comboboxin tagi on "seula" tai "jakoseula", eli kaikki seuladropdown-valikot
                        {
                            Seulakirjasto j = new Seulakirjasto();
                            string seulatxt = ((ComboBox)c).Text;//otetaan valittu seula talteen
                            seulatxt = seulatxt.Replace(".", ",");//Korvataan pisteet pilkuilla
                            if(Double.TryParse(seulatxt, out double r) == true)
                            {
                                j.seula = Convert.ToDouble(seulatxt);
                                js.Add(j);
                            }
                            /*else
                            {
                                j.seula = 0;
                                js.Add(j);
                            }*/
                            
                        }
                    }

                }
            }
            foreach (Seulakirjasto j in js)
            {
                JakoSeula.Items.Add(j.seula);
            }
            JakoSeula.SelectedIndex = 0;

        }

        private void EmptyFields_Click(object sender, RoutedEventArgs e) //Tyhjennä napin funktio
        {

            EmptyFields();
            //Kutsuu funktiota mikä tyhjentää kaikki tekstikentät seulalaskenta-ruudusta

        }
        private void EmptyResultFields() //Tyhjentää tulosruuduissa olevat arvot
        {

            seulapros1.Text = String.Empty;
            seulapros2.Text = String.Empty;
            seulapros3.Text = String.Empty;
            seulapros4.Text = String.Empty;
            seulapros5.Text = String.Empty;
            seulapros6.Text = String.Empty;
            seulapros7.Text = String.Empty;
            seulapros8.Text = String.Empty;
            seulapros9.Text = String.Empty;
            seulapros10.Text = String.Empty;
            seulapros11.Text = String.Empty;
            seulapros12.Text = String.Empty;
            seulapros13.Text = String.Empty;
            seulapros14.Text = String.Empty;
            seulapros15.Text = String.Empty;
            seulapros16.Text = String.Empty;
            seulapros17.Text = String.Empty;
            seulapros18.Text = String.Empty;
            seulapros19.Text = String.Empty;

            lapaisypros1.Text = String.Empty;
            lapaisypros2.Text = String.Empty;
            lapaisypros3.Text = String.Empty;
            lapaisypros4.Text = String.Empty;
            lapaisypros5.Text = String.Empty;
            lapaisypros6.Text = String.Empty;
            lapaisypros7.Text = String.Empty;
            lapaisypros8.Text = String.Empty;
            lapaisypros9.Text = String.Empty;
            lapaisypros10.Text = String.Empty;
            lapaisypros11.Text = String.Empty;
            lapaisypros12.Text = String.Empty;
            lapaisypros13.Text = String.Empty;
            lapaisypros14.Text = String.Empty;
            lapaisypros15.Text = String.Empty;
            lapaisypros16.Text = String.Empty;
            lapaisypros17.Text = String.Empty;
            lapaisypros18.Text = String.Empty;

            jaettuG0.Text = String.Empty;
            jaettuG1.Text = String.Empty;
            jaettuG2.Text = String.Empty;
            jaettuG3.Text = String.Empty;
            jaettuG4.Text = String.Empty;
            jaettuG5.Text = String.Empty;
            jaettuG6.Text = String.Empty;
            jaettuG7.Text = String.Empty;
            jaettuG8.Text = String.Empty;
            jaettuG9.Text = String.Empty;
            jaettuG10.Text = String.Empty;
            jaettuG11.Text = String.Empty;
            jaettuG12.Text = String.Empty;
            jaettuG13.Text = String.Empty;
            jaettuG14.Text = String.Empty;
            jaettuG15.Text = String.Empty;
            jaettuG16.Text = String.Empty;
            jaettuG17.Text = String.Empty;
            jaettuG18.Text = String.Empty;

            punnittuYhteensa.Text = String.Empty;
            tbKosteuspros.Text = String.Empty;
            tbPesutappio.Text = String.Empty;

        }

        //Funktiot mitkä mahdollistavat liikkumaan TextBoxeissa Enter-napin painalluksella
        //-------------------------------------------------------------------
        private void seulaArvot_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TextBox s = e.Source as TextBox;
                if (s != null)
                {
                    s.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                }

                e.Handled = true;
            }
        }

        private void ohjeArvot_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TextBox s = e.Source as TextBox;
                if (s != null)
                {
                    s.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                }

                e.Handled = true;
            }
        }

        private void tietoArvot_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TextBox s = e.Source as TextBox;
                if (s != null)
                {
                    s.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                }

                e.Handled = true;
            }
        }

        private void osoiteArvot_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TextBox s = e.Source as TextBox;
                if (s != null)
                {
                    s.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                }

                e.Handled = true;

            }
        }
        //------------------------------------------------------------------

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string[] files = Directory.GetFiles(@".\Asetukset\kuvat\logot");

            foreach (string file in files)
            {
                Console.WriteLine(file);
            }
        }


        private void seulaArvot_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            bool textOk = false;

            if (e.DataObject.GetDataPresent(typeof(String)))
            {
                String text = (String)e.DataObject.GetData(typeof(String));
                Regex r = new Regex(@"^[,][0-9]+$|^[0-9]*[,]{0,1}[0-9]*$");
                if (r.IsMatch(text))
                {
                    textOk = true;
                }
            }

            if (!textOk)
            {
                e.CancelCommand();
            }
        }

        private void ohjeArvot_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            bool textOk = false;

            if (e.DataObject.GetDataPresent(typeof(String)))
            {
                String text = (String)e.DataObject.GetData(typeof(String));
                Regex r = new Regex(@"^[,][0-9]+$|^[0-9]*[,]{0,1}[0-9]*$");
                if (r.IsMatch(text))
                {
                    textOk = true;
                }
            }

            if (!textOk)
            {
                e.CancelCommand();
            }
        }
    }
}
