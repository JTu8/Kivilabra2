using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace KiviMassaApp
{
    class SaveLoadFunc
    {
        List<Seulakirjasto> seulalista = new List<Seulakirjasto>();
        public string Save(Window w, List<Seulakirjasto> seulist)
        {
            seulalista = seulist;
            string json = null;
            //--------------------------Tarkoituksena ottaa ikkunasta tiedot talteen ja muuttaa ne JSON-stringiksi ja palauttaa se------------------------------------
            if (w.Name == "kiviohjelma")//Jos ikkuna mistä funktiota kutsuttiin on Kiviohjelma, mennään tästä sisälle
            {

                Kiviohjelma kiv = (Kiviohjelma)w; //Otetaan kiviohjelma tähän. Tämän avulla otetaan tallennettavat tiedot
                List<SyotetytArvot> syotteet = new List<SyotetytArvot>(); //Kaikki käyttäjän syöttämät tiedot kentistä "Seulalle Jäi (g)" 
                List<SeulaTallennus> seulacombot = new List<SeulaTallennus>();//Seulacomboboksin arvot laitetaan tänne (mikä seula valittu, mones seula listasta se on ja mikä on comboboksin sijainti)
                bool pesuseulValinta = false;
                JakoAsetukset jako = new JakoAsetukset();//Laitetaan jaetun näytteen asetukset tänne talteen
                List<TekstiTiedot> tekstitiedot = new List<TekstiTiedot>();//Tähän laitetaan talteen muut tiedot ohjelman yläosasta (nimi, työmaa, lajite jne...)
                List<SeulaLapPros> sisOhjeAla = new List<SeulaLapPros>();//Sisempi ohjealue, alempi ohje%
                List<SeulaLapPros> sisOhjeYla = new List<SeulaLapPros>();//Sisempi ohjealue, ylempi ohje%
                List<SeulaLapPros> uloOhjeAla = new List<SeulaLapPros>();//Ulompi ohjealue, alempi ohje%
                List<SeulaLapPros> uloOhjeYla = new List<SeulaLapPros>();//Ulompi ohjealue, ylempi ohje%
                List<TekstiTiedot> muutTiedot = new List<TekstiTiedot>();//sisältää märkäpainon, kuiva-ja pesupainon

                //Otetaan talteen käyttäjän syöttämät tiedot kentistä "Seulalle Jäi (g)"
                foreach (Control c in kiv.seulaArvot.Children) //Katsoo läpi kaikki objektit seulaArvot-canvasista
                {
                    if (c.GetType() == typeof(TextBox)) //jos objektin tyyppi on TextBox
                    {
                        if (((TextBox)c).Tag.ToString() != null) //tarkistetaan ettei tag ole null. vaatii että kaikilla textbokseilla on jokin tagi
                        {
                            if (((TextBox)c).Tag.ToString() == "arvo") //tarkistaa onko tagi "arvo"
                            {
                                if (((TextBox)c).Text != String.Empty)
                                {
                                    SyotetytArvot s = new SyotetytArvot();
                                    double g = Convert.ToDouble(((TextBox)c).Text);
                                    string n = ((TextBox)c).Name; //otetaan objektin nimi talteen
                                    int index = Convert.ToInt32(Regex.Match(n, @"\d+$").Value);//otetaan objektin järjestysnumero nimestä
                                    s.syote = g;
                                    s.index = index;
                                    syotteet.Add(s);
                                }

                            }
                        }

                    }
                }

                //Otetaan talteen käyttäjän valitsemat seulat "Seula"-combobokseista
                foreach (Control c in kiv.seulaArvot.Children)
                {
                    if (c.GetType() == typeof(ComboBox))
                    {
                        if (((ComboBox)c).Tag.ToString() != null)
                        {
                            if (((ComboBox)c).Tag.ToString() == "seula")
                            {
                                if (((ComboBox)c).Text != String.Empty && Double.TryParse(((ComboBox)c).Text, out double o) == true)
                                {
                                    SeulaTallennus s = new SeulaTallennus();
                                    double se = Convert.ToDouble(((ComboBox)c).Text);
                                    string n = ((ComboBox)c).Name; //otetaan objektin nimi talteen
                                    int si = Convert.ToInt32(Regex.Match(n, @"\d+$").Value);//otetaan objektin järjestysnumero nimestä
                                    s.seularvo = se;//Tallennetaan mikä seula on valittuna
                                    s.sijainti = si;//tallennetaan valitun comboboksin sijainti/järjestysluku
                                    s.seulansijainti = ((ComboBox)c).SelectedIndex;//Tallennetaan mones seula valittu seula on seulalistasta
                                    seulacombot.Add(s);
                                }

                            }
                        }

                    }
                }

                //Onko valittuna kuiva- vai pesuseulonta
                if (kiv.rbPesuseulonta.IsChecked == true)
                {
                    pesuseulValinta = true;
                }
                else
                {
                    pesuseulValinta = false;
                }
                //Muut syotteet
                muutTiedot.Add(new TekstiTiedot { otsikko = kiv.markapaino.Name, tieto = kiv.markapaino.Text });
                muutTiedot.Add(new TekstiTiedot { otsikko = kiv.tbKuivapaino.Name, tieto = kiv.tbKuivapaino.Text });
                muutTiedot.Add(new TekstiTiedot { otsikko = kiv.tbPesupaino.Name, tieto = kiv.tbPesupaino.Text });


                //Jakoseulonta-asetukset
                double? jakokerroin = null;
                int jakoseula = kiv.JakoSeula.SelectedIndex;
                string jakoseularvo = kiv.JakoSeula.Text;
                if (kiv.tbJakoKerroin.Text != String.Empty && Double.TryParse(kiv.tbJakoKerroin.Text, out double r) == true)
                {
                    jakokerroin = Convert.ToDouble(kiv.tbJakoKerroin.Text);
                }
                jako.jakokerroin = jakokerroin;
                jako.jakoseula = jakoseula;
                jako.jakoseularvo = jakoseularvo;

                //Otetaan talteen tekstitiedot ohjelman yläosasta
                foreach (Control c in kiv.tietoarvot.Children)
                {
                    if (c.GetType() == typeof(TextBox))
                    {
                        TekstiTiedot t = new TekstiTiedot();
                        t.otsikko = ((TextBox)c).Name;
                        if (((TextBox)c).Text != String.Empty)
                        {
                            t.tieto = ((TextBox)c).Text;
                        }
                        else
                        {
                            t.tieto = null;
                        }
                        tekstitiedot.Add(t);
                    }
                }
                foreach (Control c in kiv.osoitearvot.Children)
                {
                    if (c.GetType() == typeof(TextBox))
                    {
                        TekstiTiedot t = new TekstiTiedot();
                        t.otsikko = ((TextBox)c).Name;
                        if (((TextBox)c).Text != String.Empty)
                        {
                            t.tieto = ((TextBox)c).Text;
                        }
                        else
                        {
                            t.tieto = null;
                        }
                        tekstitiedot.Add(t);
                    }
                }

                //Otetaan talteen ohjealueet
                for (int i = 0; i < 4; i++)//Otetaan ohjealueet talteen yksi kolumni kerrallaan
                {
                    switch (i)
                    {
                        case 0:
                            int j = 1;
                            foreach (Control c in kiv.ohjeArvot.Children)
                            {
                                if (c.GetType() == typeof(TextBox))
                                {
                                    if (((TextBox)c).Tag.ToString() != null)
                                    {
                                        if (((TextBox)c).Tag.ToString() == "sisAla")
                                        {
                                            if (((TextBox)c).Text != String.Empty)
                                            {
                                                string seulatxt = ((TextBox)c).Text;
                                                seulatxt = seulatxt.Replace(".", ",");
                                                SeulaLapPros ohj = new SeulaLapPros
                                                {
                                                    index = j,
                                                    tulos = Convert.ToDouble(seulatxt)
                                                };
                                                sisOhjeAla.Add(ohj);

                                            }
                                            j++;

                                        }
                                    }

                                }
                            }
                            break;
                        case 1:
                            int k = 1;
                            foreach (Control c in kiv.ohjeArvot.Children)
                            {
                                if (c.GetType() == typeof(TextBox))
                                {
                                    if (((TextBox)c).Tag.ToString() != null)
                                    {
                                        if (((TextBox)c).Tag.ToString() == "sisYla")
                                        {
                                            if (((TextBox)c).Text != String.Empty)
                                            {
                                                string seulatxt = ((TextBox)c).Text;
                                                seulatxt = seulatxt.Replace(".", ",");
                                                SeulaLapPros ohj = new SeulaLapPros
                                                {
                                                    index = k,
                                                    tulos = Convert.ToDouble(seulatxt)
                                                };
                                                sisOhjeYla.Add(ohj);

                                            }
                                            k++;
                                        }
                                    }

                                }
                            }
                            break;
                        case 2:
                            int l = 1;
                            foreach (Control c in kiv.ohjeArvot.Children)
                            {
                                if (c.GetType() == typeof(TextBox))
                                {
                                    if (((TextBox)c).Tag.ToString() != null)
                                    {
                                        if (((TextBox)c).Tag.ToString() == "uloAla")
                                        {
                                            if (((TextBox)c).Text != String.Empty)
                                            {
                                                string seulatxt = ((TextBox)c).Text;
                                                seulatxt = seulatxt.Replace(".", ",");
                                                SeulaLapPros ohj = new SeulaLapPros
                                                {
                                                    index = l,
                                                    tulos = Convert.ToDouble(seulatxt)
                                                };
                                                uloOhjeAla.Add(ohj);

                                            }
                                            l++;
                                        }
                                    }

                                }
                            }
                            break;
                        case 3:
                            int m = 1;
                            foreach (Control c in kiv.ohjeArvot.Children)
                            {
                                if (c.GetType() == typeof(TextBox))
                                {
                                    if (((TextBox)c).Tag.ToString() != null)
                                    {
                                        if (((TextBox)c).Tag.ToString() == "uloYla")
                                        {
                                            if (((TextBox)c).Text != String.Empty)
                                            {
                                                string seulatxt = ((TextBox)c).Text;
                                                seulatxt = seulatxt.Replace(".", ",");
                                                SeulaLapPros ohj = new SeulaLapPros
                                                {
                                                    index = m,
                                                    tulos = Convert.ToDouble(seulatxt)
                                                };
                                                uloOhjeYla.Add(ohj);

                                            }
                                            m++;
                                        }
                                    }

                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                /*var kaikkilistat = new { nimi = "Actual", data = new List<object>() };
                //HUOMIOI TÄMÄ! tässä näkyy se järjestys mistä nämä listat löytyy tiedostosta
                kaikkilistat.data.Add(seulalista);
                kaikkilistat.data.Add(syotteet);
                kaikkilistat.data.Add(seulacombot);
                kaikkilistat.data.Add(pesuseulValinta);
                kaikkilistat.data.Add(jako);
                kaikkilistat.data.Add(tekstitiedot);
                kaikkilistat.data.Add(sisOhjeAla);
                kaikkilistat.data.Add(sisOhjeYla);
                kaikkilistat.data.Add(uloOhjeAla);
                kaikkilistat.data.Add(uloOhjeYla);*/
                KaikkiListat lkl = new KaikkiListat();
                lkl.savetype = 1;
                lkl.kaikkiseulat = seulalista;
                lkl.syotteet = syotteet;
                lkl.muutTiedot = muutTiedot;
                lkl.seulcmb = seulacombot;
                lkl.pesuseul = pesuseulValinta;
                lkl.jako = jako;
                lkl.txtiedot = tekstitiedot;
                lkl.sisOhjeAla = sisOhjeAla;
                lkl.sisOhjeYla = sisOhjeYla;
                lkl.uloOhjeAla = uloOhjeAla;
                lkl.uloOhjeYla = uloOhjeYla;

                json = JsonConvert.SerializeObject(lkl);

            }
            //----------------------------------------------------
            //-------------erottelijakommentti, erottelee eri ohjelmien tallennusosat
            //----------------------------------------------------
            else if (w.Name == "massaohjelma")
            {
                Massaohjelma mas = (Massaohjelma)w; //Otetaan kiviohjelma tähän. Tämän avulla otetaan tallennettavat tiedot
                List<SyotetytArvot> syotteet = new List<SyotetytArvot>(); //Kaikki käyttäjän syöttämät tiedot kentistä "Seulalle Jäi (g)" 
                List<SeulaTallennus> seulacombot = new List<SeulaTallennus>();//Seulacomboboksin arvot laitetaan tänne (mikä seula valittu, mones seula listasta se on ja mikä on comboboksin sijainti)
                TekstiTiedot markapaino = new TekstiTiedot();
                List<TekstiTiedot> tekstitiedot = new List<TekstiTiedot>();//Tähän laitetaan talteen muut tiedot ohjelman yläosasta (nimi, työmaa, lajite jne...)
                List<TekstiTiedot> sideainelaskuri = new List<TekstiTiedot>();//Tähän tallennetaan sideainelaskurin syötteet
                List<SeulaLapPros> OhjeAla = new List<SeulaLapPros>();// alempi ohje%
                List<SeulaLapPros> OhjeYla = new List<SeulaLapPros>();//ylempi ohje%
                

                //Otetaan talteen käyttäjän syöttämät tiedot kentistä "Seulalle Jäi (g)"
                foreach (Control c in mas.seulaArvot.Children) //Katsoo läpi kaikki objektit seulaArvot-canvasista
                {
                    if (c.GetType() == typeof(TextBox)) //jos objektin tyyppi on TextBox
                    {
                        if (((TextBox)c).Tag.ToString() != null) //tarkistetaan ettei tag ole null. vaatii että kaikilla textbokseilla on jokin tagi
                        {
                            if (((TextBox)c).Tag.ToString() == "arvo") //tarkistaa onko tagi "arvo"
                            {
                                if (((TextBox)c).Text != String.Empty)
                                {
                                    SyotetytArvot s = new SyotetytArvot();
                                    double g = Convert.ToDouble(((TextBox)c).Text);
                                    string n = ((TextBox)c).Name; //otetaan objektin nimi talteen
                                    int index = Convert.ToInt32(Regex.Match(n, @"\d+$").Value);//otetaan objektin järjestysnumero nimestä
                                    s.syote = g;
                                    s.index = index;
                                    syotteet.Add(s);
                                }

                            }
                        }

                    }
                }
                //Otetaan märkäpaino talteen
                markapaino.otsikko = mas.tbMarkaPaino.Name;
                markapaino.tieto = mas.tbMarkaPaino.Text;
                //Otetaan talteen käyttäjän valitsemat seulat "Seula"-combobokseista
                foreach (Control c in mas.seulaArvot.Children)
                {
                    if (c.GetType() == typeof(ComboBox))
                    {
                        if (((ComboBox)c).Tag.ToString() != null)
                        {
                            if (((ComboBox)c).Tag.ToString() == "seula")
                            {
                                if (((ComboBox)c).Text != String.Empty && Double.TryParse(((ComboBox)c).Text, out double o) == true)
                                {
                                    SeulaTallennus s = new SeulaTallennus();
                                    double se = Convert.ToDouble(((ComboBox)c).Text);
                                    string n = ((ComboBox)c).Name; //otetaan objektin nimi talteen
                                    int si = Convert.ToInt32(Regex.Match(n, @"\d+$").Value);//otetaan objektin järjestysnumero nimestä
                                    s.seularvo = se;//Tallennetaan mikä seula on valittuna
                                    s.sijainti = si;//tallennetaan valitun comboboksin sijainti/järjestysluku
                                    s.seulansijainti = ((ComboBox)c).SelectedIndex;//Tallennetaan mones seula valittu seula on seulalistasta
                                    seulacombot.Add(s);
                                }

                            }
                        }

                    }
                }

                //Otetaan talteen tekstitiedot ohjelman yläosasta
                foreach (Control c in mas.tietoArvot.Children)
                {
                    if (c.GetType() == typeof(TextBox))
                    {
                        TekstiTiedot t = new TekstiTiedot();
                        t.otsikko = ((TextBox)c).Name;
                        if (((TextBox)c).Text != String.Empty)
                        {
                            t.tieto = ((TextBox)c).Text;
                        }
                        else
                        {
                            t.tieto = null;
                        }
                        tekstitiedot.Add(t);
                    }
                }
                //Otetaan talteen sideainelaskurin syotteet
                foreach (Control c in mas.sideainelaskuri.Children)
                {
                    if (c.GetType() == typeof(TextBox))
                    {
                        TekstiTiedot t = new TekstiTiedot();
                        t.otsikko = ((TextBox)c).Name;
                        if (((TextBox)c).Text != String.Empty)
                        {
                            t.tieto = ((TextBox)c).Text;
                        }
                        else
                        {
                            t.tieto = null;
                        }
                        sideainelaskuri.Add(t);
                    }
                }

                //Otetaan talteen ohjealueet
                for (int i = 0; i < 2; i++)//Otetaan ohjealueet talteen yksi kolumni kerrallaan
                {
                    switch (i)
                    {
                        case 0:
                            int j = 1;
                            foreach (Control c in mas.ohjeAlue.Children)
                            {
                                if (c.GetType() == typeof(TextBox))
                                {
                                    if (((TextBox)c).Tag.ToString() != null)
                                    {
                                        if (((TextBox)c).Tag.ToString() == "Ala")
                                        {
                                            if (((TextBox)c).Text != String.Empty)
                                            {
                                                string seulatxt = ((TextBox)c).Text;
                                                seulatxt = seulatxt.Replace(".", ",");
                                                SeulaLapPros ohj = new SeulaLapPros
                                                {
                                                    index = j,
                                                    tulos = Convert.ToDouble(seulatxt)
                                                };
                                                OhjeAla.Add(ohj);

                                            }
                                            j++;

                                        }
                                    }

                                }
                            }
                            break;
                        case 1:
                            int k = 1;
                            foreach (Control c in mas.ohjeAlue.Children)
                            {
                                if (c.GetType() == typeof(TextBox))
                                {
                                    if (((TextBox)c).Tag.ToString() != null)
                                    {
                                        if (((TextBox)c).Tag.ToString() == "Yla")
                                        {
                                            if (((TextBox)c).Text != String.Empty)
                                            {
                                                string seulatxt = ((TextBox)c).Text;
                                                seulatxt = seulatxt.Replace(".", ",");
                                                SeulaLapPros ohj = new SeulaLapPros
                                                {
                                                    index = k,
                                                    tulos = Convert.ToDouble(seulatxt)
                                                };
                                                OhjeYla.Add(ohj);

                                            }
                                            k++;
                                        }
                                    }

                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                //HUOMIOI TÄMÄ! tässä näkyy se järjestys mistä nämä listat löytyy tiedostosta

                KaikkiListat lkl = new KaikkiListat();
                lkl.savetype = 2;
                lkl.kaikkiseulat = seulalista;
                lkl.syotteet = syotteet;
                lkl.markapaino = markapaino;
                lkl.seulcmb = seulacombot;
                lkl.txtiedot = tekstitiedot;
                lkl.sideainelaskuri = sideainelaskuri;
                lkl.sisOhjeAla = OhjeAla;
                lkl.sisOhjeYla = OhjeYla;

                json = JsonConvert.SerializeObject(lkl);
            }
            return json;
        }


        public void LoadAll(Window w, string json, int mode)
        {
            //w kertoo mistä ikkunasta funktiota kutsutaan, json on ladattu tiedosto stringinä ja mode kertoo mitä tietoja laitetaan ohjelmaan
            /*mode:
             * 1: kaikki tiedot
             * 2: vain seulaAlueen tiedot
             * 3: vain ohjealueet
             * 4: vain tekstitiedot
             */
            if (w.Name == "kiviohjelma")
            {
                Kiviohjelma kiv = (Kiviohjelma)w;
                if (mode == 1)
                {
                    kiv.EmptyFields();
                }
                List<Seulakirjasto> vanhaseulist = kiv.GetSetSeulalista();
                //List<KaikkiListat> kaikkilistat = new List<KaikkiListat>();
                int savemode = 0;
                List<Seulakirjasto> seulist = new List<Seulakirjasto>(); //Kaikki seulat mitä ohjelma voi käyttää
                List<SyotetytArvot> syotteet = new List<SyotetytArvot>(); //Kaikki käyttäjän syöttämät tiedot kentistä "Seulalle Jäi (g)" 
                List<SeulaTallennus> seulacombot = new List<SeulaTallennus>();//Seulacomboboksin arvot laitetaan tänne (mikä seula valittu, mones seula listasta se on ja mikä on comboboksin sijainti)
                bool pesuseulValinta = false;
                List<TekstiTiedot> muutTiedot = new List<TekstiTiedot>();
                JakoAsetukset jako = new JakoAsetukset();//Laitetaan jaetun näytteen asetukset tänne talteen
                List<TekstiTiedot> tekstitiedot = new List<TekstiTiedot>();//Tähän laitetaan talteen muut tiedot ohjelman yläosasta (nimi, työmaa, lajite jne...)
                List<SeulaLapPros> sisOhjeAla = new List<SeulaLapPros>();//Sisempi ohjealue, alempi ohje%
                List<SeulaLapPros> sisOhjeYla = new List<SeulaLapPros>();//Sisempi ohjealue, ylempi ohje%
                List<SeulaLapPros> uloOhjeAla = new List<SeulaLapPros>();//Ulompi ohjealue, alempi ohje%
                List<SeulaLapPros> uloOhjeYla = new List<SeulaLapPros>();//Ulompi ohjealue, ylempi ohje%

                //Jaotellaan tallennustiedoston tiedot omiin listoihinsa
                try
                {
                    KaikkiListat kaikki = JsonConvert.DeserializeObject<KaikkiListat>(json);
                    savemode = kaikki.savetype;
                    if (savemode == 1)
                    {
                        seulist = kaikki.kaikkiseulat;
                        syotteet = kaikki.syotteet;
                        seulacombot = kaikki.seulcmb;
                        muutTiedot = kaikki.muutTiedot;
                        pesuseulValinta = kaikki.pesuseul;
                        jako = kaikki.jako;
                        tekstitiedot = kaikki.txtiedot;
                        sisOhjeAla = kaikki.sisOhjeAla;
                        sisOhjeYla = kaikki.sisOhjeYla;
                        uloOhjeAla = kaikki.uloOhjeAla;
                        uloOhjeYla = kaikki.uloOhjeYla;
                        //Tarkistetaan onko seulist sama kuin vanhaseulist
                        //Jos tiedostosta ladattu seulist on erilainen kuin vanhaseulist, laitetaan kiviohjelmaan uusi seulist käyttöön
                        if (seulist != vanhaseulist)
                        {
                            kiv.GetSetSeulalista(seulist);

                        }
                        //Laitetaan ja asetetaan seulacomboboksit 
                        foreach (Control c in kiv.seulaArvot.Children)
                        {
                            if (c.GetType() == typeof(ComboBox))
                            {
                                if (((ComboBox)c).Tag.ToString() != null)
                                {
                                    if (((ComboBox)c).Tag.ToString() == "seula")
                                    {
                                        //Otetaan tekstilaatikon indeksi nimestä, verrataan syotteet-listassa olevaan indeksiin, ja laitetaan arvo jos tarvitsee
                                        int ni = Convert.ToInt32(Regex.Match(((ComboBox)c).Name, @"\d+$").Value);
                                        ((ComboBox)c).Items.Clear();
                                        foreach (Seulakirjasto s in kiv.seulalista)
                                        {
                                            ((ComboBox)c).Items.Add(s.seula);//Listätään seulavaihtoehdot seulavalikkoihin
                                        }
                                        ((ComboBox)c).Items.Add(String.Empty);
                                        foreach (SeulaTallennus s in seulacombot)
                                        {
                                            if (ni == s.sijainti)
                                            {
                                                ((ComboBox)c).SelectedIndex = s.seulansijainti;
                                            }
                                        }
                                        
                                        
                                        
                                    }
                                }

                            }
                        }
                        // Laitetaan seulaAlueelle arvot
                        if (mode == 1 || mode == 2)
                        {
                            foreach (Control c in kiv.seulaArvot.Children)
                            {
                                if (c.GetType() == typeof(TextBox))
                                {
                                    if (((TextBox)c).Tag.ToString() != null)
                                    {
                                        if (((TextBox)c).Tag.ToString() == "arvo")
                                        {
                                            //Otetaan tekstilaatikon indeksi nimestä, verrataan syotteet-listassa olevaan indeksiin, ja laitetaan arvo jos tarvitsee
                                            int ni = Convert.ToInt32(Regex.Match(((TextBox)c).Name, @"\d+$").Value);
                                            foreach (SyotetytArvot s in syotteet)
                                            {
                                                if (ni == s.index)
                                                {
                                                    ((TextBox)c).Text = s.syote.ToString();
                                                }
                                            }
                                           
                                        }
                                    }

                                }
                            }


                            //Asetetaan pesuseulonta-asetus
                            if (pesuseulValinta == false)
                            {
                                kiv.rbPesuseulonta.IsChecked = false;
                                kiv.rbKuivaseulonta.IsChecked = true;
                            }
                            else
                            {
                                kiv.rbPesuseulonta.IsChecked = true;
                                kiv.rbKuivaseulonta.IsChecked = false;
                            }
                            //Asetetaan muut tiedot (märkäpaino-, kuivaseulonta- ja pesuseulontakentät)
                            foreach (TekstiTiedot t in muutTiedot)
                            {
                                switch (t.otsikko)
                                {
                                    case "markapaino":
                                        kiv.markapaino.Text = t.tieto;
                                        break;
                                    case "tbPesupaino":
                                        kiv.tbPesupaino.Text = t.tieto;
                                        break;
                                    case "tbKuivapaino":
                                        kiv.tbKuivapaino.Text = t.tieto;
                                        break;
                                    default:
                                        break;
                                }
                            }

                            //Jaetun näytteen asetukset
                            kiv.JakoSeula.Items.Clear();
                            kiv.JakoSeula.Items.Add("Ei jakoa");
                            foreach (Seulakirjasto s in kiv.seulalista)
                            {
                                kiv.JakoSeula.Items.Add(s.seula);
                            }
                            if (jako.jakokerroin != null)
                            {
                                kiv.JakoSeula.SelectedIndex = Convert.ToInt32(jako.jakoseula);
                                kiv.tbJakoKerroin.Text = jako.jakokerroin.ToString();
                            }
                            else
                            {
                                kiv.JakoSeula.SelectedIndex = 0;
                            }
                        }


                        //Asetetaan tekstitiedot omille kentilleen
                        if (mode == 1 || mode == 4)
                        {
                            foreach (TekstiTiedot t in tekstitiedot)
                            {
                                switch (t.otsikko)
                                {
                                    case "tyomaa":
                                        kiv.tyomaa.Text = t.tieto;
                                        break;
                                    case "lajite":
                                        kiv.lajite.Text = t.tieto;
                                        break;
                                    case "naytteenOttaja":
                                        kiv.naytteenOttaja.Text = t.tieto;
                                        break;
                                    case "nayteNro":
                                        kiv.nayteNro.Text = t.tieto;
                                        break;
                                    case "date":
                                        kiv.date.Text = t.tieto;
                                        break;
                                    case "tutkija":
                                        kiv.tutkija.Text = t.tieto;
                                        break;
                                    case "lisatieto":
                                        kiv.lisatieto.Text = t.tieto;
                                        break;
                                    case "ylempiOtsikko":
                                        kiv.ylempiOtsikko.Text = t.tieto;
                                        break;
                                    case "alempiOtsikko":
                                        kiv.alempiOtsikko.Text = t.tieto;
                                        break;
                                    case "lahiosoite":
                                        kiv.lahiosoite.Text = t.tieto;
                                        break;
                                    case "osoite":
                                        kiv.osoite.Text = t.tieto;
                                        break;
                                    case "puh":
                                        kiv.puh.Text = t.tieto;
                                        break;
                                }
                            }
                        }




                        //Asetetaan syötetyt ohjealueet paikoilleen
                        if (mode == 1 || mode == 3)
                        {
                            for (int k = 0; k < 4; k++)
                            {
                                switch (k)
                                {
                                    case 0:
                                        foreach (Control c in kiv.ohjeArvot.Children)
                                        {
                                            if (c.GetType() == typeof(TextBox))
                                            {
                                                if (((TextBox)c).Tag.ToString() != null)
                                                {
                                                    if (((TextBox)c).Tag.ToString() == "sisAla")
                                                    {
                                                        string tbnimi = ((TextBox)c).Name;
                                                        int jnro = Convert.ToInt32(Regex.Match(tbnimi, @"\d+$").Value);//otetaan objektin järjestysnumero nimestä
                                                        foreach (SeulaLapPros s in sisOhjeAla)
                                                        {
                                                            if (jnro == s.index)
                                                            {
                                                                ((TextBox)c).Text = s.tulos.ToString();
                                                            }
                                                        }
                                                    }
                                                }

                                            }
                                        }
                                        break;
                                    case 1:
                                        foreach (Control c in kiv.ohjeArvot.Children)
                                        {
                                            if (c.GetType() == typeof(TextBox))
                                            {
                                                if (((TextBox)c).Tag.ToString() != null)
                                                {
                                                    if (((TextBox)c).Tag.ToString() == "sisYla")
                                                    {
                                                        string tbnimi = ((TextBox)c).Name;
                                                        int jnro = Convert.ToInt32(Regex.Match(tbnimi, @"\d+$").Value);//otetaan objektin järjestysnumero nimestä
                                                        foreach (SeulaLapPros s in sisOhjeYla)
                                                        {
                                                            if (jnro == s.index)
                                                            {
                                                                ((TextBox)c).Text = s.tulos.ToString();
                                                            }
                                                        }

                                                    }
                                                }

                                            }
                                        }
                                        break;
                                    case 2:
                                        foreach (Control c in kiv.ohjeArvot.Children)
                                        {
                                            if (c.GetType() == typeof(TextBox))
                                            {
                                                if (((TextBox)c).Tag.ToString() != null)
                                                {
                                                    if (((TextBox)c).Tag.ToString() == "uloAla")
                                                    {
                                                        string tbnimi = ((TextBox)c).Name;
                                                        int jnro = Convert.ToInt32(Regex.Match(tbnimi, @"\d+$").Value);//otetaan objektin järjestysnumero nimestä
                                                        foreach (SeulaLapPros s in uloOhjeAla)
                                                        {
                                                            if (jnro == s.index)
                                                            {
                                                                ((TextBox)c).Text = s.tulos.ToString();
                                                            }
                                                        }
                                                    }
                                                }

                                            }
                                        }
                                        break;
                                    case 3:
                                        foreach (Control c in kiv.ohjeArvot.Children)
                                        {
                                            if (c.GetType() == typeof(TextBox))
                                            {
                                                if (((TextBox)c).Tag.ToString() != null)
                                                {
                                                    if (((TextBox)c).Tag.ToString() == "uloYla")
                                                    {
                                                        string tbnimi = ((TextBox)c).Name;
                                                        int jnro = Convert.ToInt32(Regex.Match(tbnimi, @"\d+$").Value);//otetaan objektin järjestysnumero nimestä
                                                        foreach (SeulaLapPros s in uloOhjeYla)
                                                        {
                                                            if (jnro == s.index)
                                                            {
                                                                ((TextBox)c).Text = s.tulos.ToString();
                                                            }
                                                        }
                                                    }
                                                }

                                            }
                                        }
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }

                        kiv.AsetaJakoSeulaValikonArvot();
                        kiv.SeulaArvotOhjeArvoihin();
                    }
                    else
                    {
                        MessageBox.Show("Valitsemaasi tiedostoa ei voi avata Kiviohjelmassa!");
                    }
                }
                catch
                {
                    MessageBox.Show("Valitsemasi tiedosto ei ole luotu tällä ohjelmalla!");
                }
                
            }
            //----------------------------------------------------------------------------
            //--------------erottelija erottelemaan kivi-ja massaohjelman funktiot--------
            //----------------------------------------------------------------------------
            else if(w.Name == "massaohjelma")
            {
                Massaohjelma mas = (Massaohjelma)w;
                if (mode == 1)
                {
                    mas.EmptyFields();
                }
                List<Seulakirjasto> vanhaseulist = mas.GetSetSeulalista();
                //List<KaikkiListat> kaikkilistat = new List<KaikkiListat>();
                int savemode = 0;
                List<Seulakirjasto> seulist = new List<Seulakirjasto>(); //Kaikki seulat mitä ohjelma voi käyttää
                List<SyotetytArvot> syotteet = new List<SyotetytArvot>(); //Kaikki käyttäjän syöttämät tiedot kentistä "Seulalle Jäi (g)" 
                List<SeulaTallennus> seulacombot = new List<SeulaTallennus>();//Seulacomboboksin arvot laitetaan tänne (mikä seula valittu, mones seula listasta se on ja mikä on comboboksin sijainti)
                List<TekstiTiedot> tekstitiedot = new List<TekstiTiedot>();//Tähän laitetaan talteen muut tiedot ohjelman yläosasta (nimi, työmaa, lajite jne...)
                List<TekstiTiedot> sideainelaskuri = new List<TekstiTiedot>();
                List<SeulaLapPros> OhjeAla = new List<SeulaLapPros>();//alempi ohje%
                List<SeulaLapPros> OhjeYla = new List<SeulaLapPros>();//ylempi ohje%
                TekstiTiedot markapaino = new TekstiTiedot();

                //Jaotellaan tallennustiedoston tiedot omiin listoihinsa
                try
                {
                    KaikkiListat kaikki = JsonConvert.DeserializeObject<KaikkiListat>(json);
                    savemode = kaikki.savetype;
                    if (savemode == 2)
                    {
                        seulist = kaikki.kaikkiseulat;
                        syotteet = kaikki.syotteet;
                        seulacombot = kaikki.seulcmb;
                        tekstitiedot = kaikki.txtiedot;
                        sideainelaskuri = kaikki.sideainelaskuri;
                        OhjeAla = kaikki.sisOhjeAla;
                        OhjeYla = kaikki.sisOhjeYla;
                        markapaino = kaikki.markapaino;
                        //Tarkistetaan onko seulist sama kuin vanhaseulist
                        //Jos tiedostosta ladattu seulist on erilainen kuin vanhaseulist, laitetaan kiviohjelmaan uusi seulist käyttöön
                        if (seulist != vanhaseulist)
                        {
                            mas.GetSetSeulalista(seulist);

                        }
                        //Laitetaan ja asetetaan seulacomboboksit 
                        foreach (Control c in mas.seulaArvot.Children)
                        {
                            if (c.GetType() == typeof(ComboBox))
                            {
                                if (((ComboBox)c).Tag.ToString() != null)
                                {
                                    if (((ComboBox)c).Tag.ToString() == "seula")
                                    {
                                        //Otetaan tekstilaatikon indeksi nimestä, verrataan syotteet-listassa olevaan indeksiin, ja laitetaan arvo jos tarvitsee
                                        int ni = Convert.ToInt32(Regex.Match(((ComboBox)c).Name, @"\d+$").Value);
                                        ((ComboBox)c).Items.Clear();
                                        foreach (Seulakirjasto s in mas.seulalista)
                                        {
                                            ((ComboBox)c).Items.Add(s.seula);//Listätään seulavaihtoehdot seulavalikkoihin
                                        }
                                        ((ComboBox)c).Items.Add(String.Empty);
                                        foreach (SeulaTallennus s in seulacombot)
                                        {
                                            if (ni == s.sijainti)
                                            {
                                                ((ComboBox)c).SelectedIndex = s.seulansijainti;
                                            }
                                        }
                                    }
                                }

                            }
                        }
                        // Laitetaan seulaAlueelle arvot
                        if (mode == 1 || mode == 2)
                        {
                            foreach (Control c in mas.seulaArvot.Children)
                            {
                                if (c.GetType() == typeof(TextBox))
                                {
                                    if (((TextBox)c).Tag.ToString() != null)
                                    {
                                        if (((TextBox)c).Tag.ToString() == "arvo")
                                        {
                                            //Otetaan tekstilaatikon indeksi nimestä, verrataan syotteet-listassa olevaan indeksiin, ja laitetaan arvo jos tarvitsee
                                            int ni = Convert.ToInt32(Regex.Match(((TextBox)c).Name, @"\d+$").Value);
                                            foreach (SyotetytArvot s in syotteet)
                                            {
                                                if (ni == s.index)
                                                {
                                                    ((TextBox)c).Text = s.syote.ToString();
                                                }
                                            }
                                        }
                                    }

                                }
                            }

                            mas.tbMarkaPaino.Text = markapaino.tieto;
                        }


                        //Asetetaan tekstitiedot omille kentilleen
                        if (mode == 1 || mode == 4)
                        {
                            foreach (TekstiTiedot t in tekstitiedot)
                            {
                                switch (t.otsikko)
                                {
                                    case "sekoitusAsema":
                                        mas.sekoitusAsema.Text = t.tieto;
                                        break;
                                    case "lisatietoja":
                                        mas.lisatietoja.Text = t.tieto;
                                        break;
                                    case "alempiOtsikko":
                                        mas.alempiOtsikko.Text = t.tieto;
                                        break;
                                    case "lahiOsoite":
                                        mas.lahiOsoite.Text = t.tieto;
                                        break;
                                    case "osoite":
                                        mas.osoite.Text = t.tieto;
                                        break;
                                    case "puh":
                                        mas.puh.Text = t.tieto;
                                        break;
                                    case "paallyste":
                                        mas.paallyste.Text = t.tieto;
                                        break;
                                    case "paivays":
                                        mas.paivays.Text = t.tieto;
                                        break;
                                    case "urakka":
                                        mas.urakka.Text = t.tieto;
                                        break;
                                    case "tyokohde":
                                        mas.tyokohde.Text = t.tieto;
                                        break;
                                    case "nayteNro":
                                        mas.nayteNro.Text = t.tieto;
                                        break;
                                    case "klo":
                                        mas.klo.Text = t.tieto;
                                        break;
                                    case "paaluKaista":
                                        mas.paaluKaista.Text = t.tieto;
                                        break;
                                    case "naytteenOttaja":
                                        mas.naytteenOttaja.Text = t.tieto;
                                        break;
                                    case "tutkija":
                                        mas.tutkija.Text = t.tieto;
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                        if (mode == 1 || mode == 2)
                        {
                            foreach (TekstiTiedot t in sideainelaskuri)
                            {
                                switch (t.otsikko)
                                {
                                    case "naytteenPaino":
                                        mas.naytteenPaino.Text = t.tieto;
                                        break;
                                    case "rumpujanayte":
                                        mas.rumpujanayte.Text = t.tieto;
                                        break;
                                    case "rummunPaino":
                                        mas.rummunPaino.Text = t.tieto;
                                        break;
                                    case "sentrifuugipaperi":
                                        mas.sentrifuugipaperi.Text = t.tieto;
                                        break;
                                    case "sentrifuugipaperirilleri":
                                        mas.sentrifuugipaperirilleri.Text = t.tieto;
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }



                        //Asetetaan syötetyt ohjealueet paikoilleen
                        if (mode == 1 || mode == 3)
                        {
                            for (int k = 0; k < 2; k++)
                            {
                                switch (k)
                                {
                                    case 0:
                                        foreach (Control c in mas.ohjeAlue.Children)
                                        {
                                            if (c.GetType() == typeof(TextBox))
                                            {
                                                if (((TextBox)c).Tag.ToString() != null)
                                                {
                                                    if (((TextBox)c).Tag.ToString() == "Ala")
                                                    {
                                                        string tbnimi = ((TextBox)c).Name;
                                                        int jnro = Convert.ToInt32(Regex.Match(tbnimi, @"\d+$").Value);//otetaan objektin järjestysnumero nimestä
                                                        foreach (SeulaLapPros s in OhjeAla)
                                                        {
                                                            if (jnro == s.index)
                                                            {
                                                                ((TextBox)c).Text = s.tulos.ToString();
                                                            }
                                                        }
                                                    }
                                                }

                                            }
                                        }
                                        break;
                                    case 1:
                                        foreach (Control c in mas.ohjeAlue.Children)
                                        {
                                            if (c.GetType() == typeof(TextBox))
                                            {
                                                if (((TextBox)c).Tag.ToString() != null)
                                                {
                                                    if (((TextBox)c).Tag.ToString() == "Yla")
                                                    {
                                                        string tbnimi = ((TextBox)c).Name;
                                                        int jnro = Convert.ToInt32(Regex.Match(tbnimi, @"\d+$").Value);//otetaan objektin järjestysnumero nimestä
                                                        foreach (SeulaLapPros s in OhjeYla)
                                                        {
                                                            if (jnro == s.index)
                                                            {
                                                                ((TextBox)c).Text = s.tulos.ToString();
                                                            }
                                                        }

                                                    }
                                                }

                                            }
                                        }
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                        mas.SeulaArvotOhjeArvoihin();
                    }
                    else
                    {
                        MessageBox.Show("Valitsemaasi tiedostoa ei voi avata Massaohjelmassa!");
                    }
                }
                catch
                {
                    MessageBox.Show("Valitsemasi tiedosto ei ole luotu tällä ohjelmalla!");
                }
            }
            

        }



        private class SeulaTallennus
        {
            public double seularvo { get; set; }//Seulan arvo, käytetään vertailussa jos pääseulalista muuttuu tallennuksen ja latauksen välillä
            public int sijainti { get; set; }//Mistä seulacomboboxissa tämä on otettu, pitäisi olla 1-18
            public int seulansijainti { get; set; }//Mones seula Comboboxissa tämä on, pitäisi olla 0-(pääseulalistan maksimikoko), käytetään vertailussa jos pääseulalista muuttuu tallennuksen ja latauksen välillä
        }
        private class JakoAsetukset
        {
            public double? jakokerroin { get; set; }//Käyttäjän syöttämä jakokerroin
            public int? jakoseula { get; set; }//valitun jakoseulan indeksi, arvot väliltä 0-[seulalistan koko]. 0 tarkoittaa että jakoa ei tehdä
            public string jakoseularvo { get; set; } //valitun jakoseulan teksti, käytetään vertailussa jos pääseulalista muuttuu tallennuksen ja latauksen välillä
        }
        private class TekstiTiedot
        {
            public string otsikko { get; set; }//Tekstikentän nimi
            public string tieto { get; set; }//Tekstikentän syöte
        }
        private class KaikkiListat
        {
            public int savetype { get; set; }//Tallennustyyppi. Jos = 1, tallennus on kiviohjelmalle. Jos = 2, tallennus on massaohjelmalle
            public List<Seulakirjasto> kaikkiseulat { get; set; }
            public List<SyotetytArvot> syotteet { get; set; }
            public List<SeulaTallennus> seulcmb { get; set; }
            public JakoAsetukset jako { get; set; }
            public bool pesuseul { get; set; }
            public List<TekstiTiedot> muutTiedot { get; set; }//sisältää märkäpainon, pesupainon ja kuivapainon, käytetään kiviohjelmassa
            public TekstiTiedot markapaino { get; set; }//sisältää märkäpainon, käytetään massaohjelmassa
            public List<TekstiTiedot> txtiedot { get; set; }
            public List<TekstiTiedot> sideainelaskuri { get; set; }
            public List<SeulaLapPros> sisOhjeAla { get; set; }
            public List<SeulaLapPros> sisOhjeYla { get; set; }
            public List<SeulaLapPros> uloOhjeAla { get; set; }
            public List<SeulaLapPros> uloOhjeYla { get; set; }
        }
    }

}
