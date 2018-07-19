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
    class KayraInitializer
    {
        private Window _kayra = null;
        public void KayraPiirto(Kiviohjelma kiv)
        {
            Kiviohjelma _kivi = kiv;

            //Näytetään rakeisuuskäyrä syötettyjen arvojen avulla
            List<SeulakirjastoIndex> selist = new List<SeulakirjastoIndex>();//Seulat joita on käytetty laskennassa, eli X-arvot.
            List<SeulaLapPros> tulist = new List<SeulaLapPros>();//Läpäisyprosenttitulokset, eli Y-arvot
            List<Seulakirjasto> selistALL = new List<Seulakirjasto>();//Kaikki seulat mitä on valittuna. Tehdään täysi X-akseli tällä.
            List<SeulaLapPros> sisOhjeAla = new List<SeulaLapPros>();//Sisempi ohjealue, alempi ohje%
            List<SeulaLapPros> sisOhjeYla = new List<SeulaLapPros>();//Sisempi ohjealue, ylempi ohje%
            List<SeulaLapPros> uloOhjeAla = new List<SeulaLapPros>();//Ulompi ohjealue, alempi ohje%
            List<SeulaLapPros> uloOhjeYla = new List<SeulaLapPros>();//Ulompi ohjealue, ylempi ohje%


            //Lukee tarvittavat prosenttiarvot ja lisää ne tulist-listaan
            //Ottaa valitut seulat ohjelmasta, ottaa talteen niiden sijainnin järjestyslukuna ja laittaa ne selist-listaan
            //Tuloksissa saattaa olla välejä (kaikkeja rivejä ei täytetty) joten koodi tarkistaa sen myös

            if (_kivi.lapaisypros1.Text != String.Empty)
            {
                SeulaLapPros sl = new SeulaLapPros
                {
                    index = 0,
                    tulos = Convert.ToDouble(_kivi.lapaisypros1.Text)
                };
                tulist.Add(sl);
                string seulatxt = _kivi.Seula1.Text;
                seulatxt = seulatxt.Replace(".", ",");
                if (Double.TryParse(seulatxt, out double r) == true)
                {
                    SeulakirjastoIndex ke = new SeulakirjastoIndex
                    {
                        index = 17,
                        seula = Convert.ToDouble(seulatxt)
                    };
                    selist.Add(ke);
                }
                else
                {
                    SeulakirjastoIndex ke = new SeulakirjastoIndex
                    {
                        index = 17,
                        seula = 0
                    };
                    selist.Add(ke);
                }

            }
            if (_kivi.lapaisypros2.Text != String.Empty)
            {
                SeulaLapPros sl = new SeulaLapPros
                {
                    index = 1,
                    tulos = Convert.ToDouble(_kivi.lapaisypros2.Text)
                };
                tulist.Add(sl);
                string seulatxt = _kivi.Seula2.Text;
                seulatxt = seulatxt.Replace(".", ",");
                if (Double.TryParse(seulatxt, out double r) == true)
                {
                    SeulakirjastoIndex ke = new SeulakirjastoIndex
                    {
                        index = 16,
                        seula = Convert.ToDouble(seulatxt)
                    };
                    selist.Add(ke);
                }
                else
                {
                    SeulakirjastoIndex ke = new SeulakirjastoIndex
                    {
                        index = 16,
                        seula = 0
                    };
                    selist.Add(ke);
                }
            }
            if (_kivi.lapaisypros3.Text != String.Empty)
            {
                SeulaLapPros sl = new SeulaLapPros
                {
                    index = 2,
                    tulos = Convert.ToDouble(_kivi.lapaisypros3.Text)
                };
                tulist.Add(sl);
                string seulatxt = _kivi.Seula3.Text;
                seulatxt = seulatxt.Replace(".", ",");
                if (Double.TryParse(seulatxt, out double r) == true)
                {
                    SeulakirjastoIndex ke = new SeulakirjastoIndex
                    {
                        index = 15,
                        seula = Convert.ToDouble(seulatxt)
                    };
                    selist.Add(ke);
                }
                else
                {
                    SeulakirjastoIndex ke = new SeulakirjastoIndex
                    {
                        index = 15,
                        seula = 0
                    };
                    selist.Add(ke);
                }
            }
            if (_kivi.lapaisypros4.Text != String.Empty)
            {
                SeulaLapPros sl = new SeulaLapPros
                {
                    index = 3,
                    tulos = Convert.ToDouble(_kivi.lapaisypros4.Text)
                };
                tulist.Add(sl);
                string seulatxt = _kivi.Seula4.Text;
                seulatxt = seulatxt.Replace(".", ",");
                if (Double.TryParse(seulatxt, out double r) == true)
                {
                    SeulakirjastoIndex ke = new SeulakirjastoIndex
                    {
                        index = 14,
                        seula = Convert.ToDouble(seulatxt)
                    };
                    selist.Add(ke);
                }
                else
                {
                    SeulakirjastoIndex ke = new SeulakirjastoIndex
                    {
                        index = 14,
                        seula = 0
                    };
                    selist.Add(ke);
                }
            }
            if (_kivi.lapaisypros5.Text != String.Empty)
            {
                SeulaLapPros sl = new SeulaLapPros
                {
                    index = 4,
                    tulos = Convert.ToDouble(_kivi.lapaisypros5.Text)
                };
                tulist.Add(sl);
                string seulatxt = _kivi.Seula5.Text;
                seulatxt = seulatxt.Replace(".", ",");
                if (Double.TryParse(seulatxt, out double r) == true)
                {
                    SeulakirjastoIndex ke = new SeulakirjastoIndex
                    {
                        index = 13,
                        seula = Convert.ToDouble(seulatxt)
                    };
                    selist.Add(ke);
                }
                else
                {
                    SeulakirjastoIndex ke = new SeulakirjastoIndex
                    {
                        index = 13,
                        seula = 0
                    };
                    selist.Add(ke);
                }
            }
            if (_kivi.lapaisypros6.Text != String.Empty)
            {
                SeulaLapPros sl = new SeulaLapPros
                {
                    index = 5,
                    tulos = Convert.ToDouble(_kivi.lapaisypros6.Text)
                };
                tulist.Add(sl);
                string seulatxt = _kivi.Seula6.Text;
                seulatxt = seulatxt.Replace(".", ",");
                if (Double.TryParse(seulatxt, out double r) == true)
                {
                    SeulakirjastoIndex ke = new SeulakirjastoIndex
                    {
                        index = 12,
                        seula = Convert.ToDouble(seulatxt)
                    };
                    selist.Add(ke);
                }
                else
                {
                    SeulakirjastoIndex ke = new SeulakirjastoIndex
                    {
                        index = 12,
                        seula = 0
                    };
                    selist.Add(ke);
                }
            }
            if (_kivi.lapaisypros7.Text != String.Empty)
            {
                SeulaLapPros sl = new SeulaLapPros
                {
                    index = 6,
                    tulos = Convert.ToDouble(_kivi.lapaisypros7.Text)
                };
                tulist.Add(sl);
                string seulatxt = _kivi.Seula7.Text;
                seulatxt = seulatxt.Replace(".", ",");
                if (Double.TryParse(seulatxt, out double r) == true)
                {
                    SeulakirjastoIndex ke = new SeulakirjastoIndex
                    {
                        index = 11,
                        seula = Convert.ToDouble(seulatxt)
                    };
                    selist.Add(ke);
                }
                else
                {
                    SeulakirjastoIndex ke = new SeulakirjastoIndex
                    {
                        index = 11,
                        seula = 0
                    };
                    selist.Add(ke);
                }
            }
            if (_kivi.lapaisypros8.Text != String.Empty)
            {
                SeulaLapPros sl = new SeulaLapPros
                {
                    index = 7,
                    tulos = Convert.ToDouble(_kivi.lapaisypros8.Text)
                };
                tulist.Add(sl);
                string seulatxt = _kivi.Seula8.Text;
                seulatxt = seulatxt.Replace(".", ",");
                if (Double.TryParse(seulatxt, out double r) == true)
                {
                    SeulakirjastoIndex ke = new SeulakirjastoIndex
                    {
                        index = 10,
                        seula = Convert.ToDouble(seulatxt)
                    };
                    selist.Add(ke);
                }
                else
                {
                    SeulakirjastoIndex ke = new SeulakirjastoIndex
                    {
                        index = 10,
                        seula = 0
                    };
                    selist.Add(ke);
                }
            }
            if (_kivi.lapaisypros9.Text != String.Empty)
            {
                SeulaLapPros sl = new SeulaLapPros
                {
                    index = 8,
                    tulos = Convert.ToDouble(_kivi.lapaisypros9.Text)
                };
                tulist.Add(sl);
                string seulatxt = _kivi.Seula9.Text;
                seulatxt = seulatxt.Replace(".", ",");
                if (Double.TryParse(seulatxt, out double r) == true)
                {
                    SeulakirjastoIndex ke = new SeulakirjastoIndex
                    {
                        index = 9,
                        seula = Convert.ToDouble(seulatxt)
                    };
                    selist.Add(ke);
                }
                else
                {
                    SeulakirjastoIndex ke = new SeulakirjastoIndex
                    {
                        index = 9,
                        seula = 0
                    };
                    selist.Add(ke);
                }
            }
            if (_kivi.lapaisypros10.Text != String.Empty)
            {
                SeulaLapPros sl = new SeulaLapPros
                {
                    index = 9,
                    tulos = Convert.ToDouble(_kivi.lapaisypros10.Text)
                };
                tulist.Add(sl);
                string seulatxt = _kivi.Seula10.Text;
                seulatxt = seulatxt.Replace(".", ",");
                if (Double.TryParse(seulatxt, out double r) == true)
                {
                    SeulakirjastoIndex ke = new SeulakirjastoIndex
                    {
                        index = 8,
                        seula = Convert.ToDouble(seulatxt)
                    };
                    selist.Add(ke);
                }
                else
                {
                    SeulakirjastoIndex ke = new SeulakirjastoIndex
                    {
                        index = 8,
                        seula = 0
                    };
                    selist.Add(ke);
                }
            }
            if (_kivi.lapaisypros11.Text != String.Empty)
            {
                SeulaLapPros sl = new SeulaLapPros
                {
                    index = 10,
                    tulos = Convert.ToDouble(_kivi.lapaisypros11.Text)
                };
                tulist.Add(sl);
                string seulatxt = _kivi.Seula11.Text;
                seulatxt = seulatxt.Replace(".", ",");
                if (Double.TryParse(seulatxt, out double r) == true)
                {
                    SeulakirjastoIndex ke = new SeulakirjastoIndex
                    {
                        index = 7,
                        seula = Convert.ToDouble(seulatxt)
                    };
                    selist.Add(ke);
                }
                else
                {
                    SeulakirjastoIndex ke = new SeulakirjastoIndex
                    {
                        index = 7,
                        seula = 0
                    };
                    selist.Add(ke);
                }
            }
            if (_kivi.lapaisypros12.Text != String.Empty)
            {
                SeulaLapPros sl = new SeulaLapPros
                {
                    index = 11,
                    tulos = Convert.ToDouble(_kivi.lapaisypros12.Text)
                };
                tulist.Add(sl);
                string seulatxt = _kivi.Seula12.Text;
                seulatxt = seulatxt.Replace(".", ",");
                if (Double.TryParse(seulatxt, out double r) == true)
                {
                    SeulakirjastoIndex ke = new SeulakirjastoIndex
                    {
                        index = 6,
                        seula = Convert.ToDouble(seulatxt)
                    };
                    selist.Add(ke);
                }
                else
                {
                    SeulakirjastoIndex ke = new SeulakirjastoIndex
                    {
                        index = 6,
                        seula = 0
                    };
                    selist.Add(ke);
                }
            }
            if (_kivi.lapaisypros13.Text != String.Empty)
            {
                SeulaLapPros sl = new SeulaLapPros
                {
                    index = 12,
                    tulos = Convert.ToDouble(_kivi.lapaisypros13.Text)
                };
                tulist.Add(sl);
                string seulatxt = _kivi.Seula13.Text;
                seulatxt = seulatxt.Replace(".", ",");
                if (Double.TryParse(seulatxt, out double r) == true)
                {
                    SeulakirjastoIndex ke = new SeulakirjastoIndex
                    {
                        index = 5,
                        seula = Convert.ToDouble(seulatxt)
                    };
                    selist.Add(ke);
                }
                else
                {
                    SeulakirjastoIndex ke = new SeulakirjastoIndex
                    {
                        index = 5,
                        seula = 0
                    };
                    selist.Add(ke);
                }
            }
            if (_kivi.lapaisypros14.Text != String.Empty)
            {
                SeulaLapPros sl = new SeulaLapPros
                {
                    index = 13,
                    tulos = Convert.ToDouble(_kivi.lapaisypros14.Text)
                };
                tulist.Add(sl);
                string seulatxt = _kivi.Seula14.Text;
                seulatxt = seulatxt.Replace(".", ",");
                if (Double.TryParse(seulatxt, out double r) == true)
                {
                    SeulakirjastoIndex ke = new SeulakirjastoIndex
                    {
                        index = 4,
                        seula = Convert.ToDouble(seulatxt)
                    };
                    selist.Add(ke);
                }
                else
                {
                    SeulakirjastoIndex ke = new SeulakirjastoIndex
                    {
                        index = 4,
                        seula = 0
                    };
                    selist.Add(ke);
                }
            }
            if (_kivi.lapaisypros15.Text != String.Empty)
            {
                SeulaLapPros sl = new SeulaLapPros
                {
                    index = 14,
                    tulos = Convert.ToDouble(_kivi.lapaisypros15.Text)
                };
                tulist.Add(sl);
                string seulatxt = _kivi.Seula15.Text;
                seulatxt = seulatxt.Replace(".", ",");
                if (Double.TryParse(seulatxt, out double r) == true)
                {
                    SeulakirjastoIndex ke = new SeulakirjastoIndex
                    {
                        index = 3,
                        seula = Convert.ToDouble(seulatxt)
                    };
                    selist.Add(ke);
                }
                else
                {
                    SeulakirjastoIndex ke = new SeulakirjastoIndex
                    {
                        index = 3,
                        seula = 0
                    };
                    selist.Add(ke);
                }
            }
            if (_kivi.lapaisypros16.Text != String.Empty)
            {
                SeulaLapPros sl = new SeulaLapPros
                {
                    index = 15,
                    tulos = Convert.ToDouble(_kivi.lapaisypros16.Text)
                };
                tulist.Add(sl);
                string seulatxt = _kivi.Seula16.Text;
                seulatxt = seulatxt.Replace(".", ",");
                if (Double.TryParse(seulatxt, out double r) == true)
                {
                    SeulakirjastoIndex ke = new SeulakirjastoIndex
                    {
                        index = 2,
                        seula = Convert.ToDouble(seulatxt)
                    };
                    selist.Add(ke);
                }
                else
                {
                    SeulakirjastoIndex ke = new SeulakirjastoIndex
                    {
                        index = 2,
                        seula = 0
                    };
                    selist.Add(ke);
                }
            }
            if (_kivi.lapaisypros17.Text != String.Empty)
            {
                SeulaLapPros sl = new SeulaLapPros
                {
                    index = 16,
                    tulos = Convert.ToDouble(_kivi.lapaisypros17.Text)
                };
                tulist.Add(sl);
                string seulatxt = _kivi.Seula17.Text;
                seulatxt = seulatxt.Replace(".", ",");
                if (Double.TryParse(seulatxt, out double r) == true)
                {
                    SeulakirjastoIndex ke = new SeulakirjastoIndex
                    {
                        index = 1,
                        seula = Convert.ToDouble(seulatxt)
                    };
                    selist.Add(ke);
                }
                else
                {
                    SeulakirjastoIndex ke = new SeulakirjastoIndex
                    {
                        index = 1,
                        seula = 0
                    };
                    selist.Add(ke);
                }
            }
            if (_kivi.lapaisypros18.Text != String.Empty)
            {
                SeulaLapPros sl = new SeulaLapPros
                {
                    index = 17,
                    tulos = Convert.ToDouble(_kivi.lapaisypros18.Text)
                };
                tulist.Add(sl);
                string seulatxt = _kivi.Seula18.Text;
                seulatxt = seulatxt.Replace(".", ",");
                if (Double.TryParse(seulatxt, out double r) == true)
                {
                    SeulakirjastoIndex ke = new SeulakirjastoIndex
                    {
                        index = 0,
                        seula = Convert.ToDouble(seulatxt)
                    };
                    selist.Add(ke);
                }
                else
                {
                    SeulakirjastoIndex ke = new SeulakirjastoIndex
                    {
                        index = 0,
                        seula = 0
                    };
                    selist.Add(ke);
                }
            }

            foreach (Control c in _kivi.seulaArvot.Children) //Kaikille esineille seulaArvot-canvasissa. Tarkoituksena ottaa kaikki valitut seulat dropdown-valikoista talteen
            {
                if (c.GetType() == typeof(ComboBox)) //jos esineen tyyppi on combobox
                {
                    //Console.WriteLine("Combobox text: " + ((ComboBox)c).Text+",  tag: "+ ((ComboBox)c).Tag);
                    if (((ComboBox)c).Tag.ToString() != null) //Jos comboboxin tagi on tyhjä
                    {
                        if (((ComboBox)c).Tag.ToString() == "seula") //jos comboboxin tagi on "seula", eli kaikki seuladropdown-valikot
                        {
                            //Console.WriteLine(((ComboBox)c).Text);
                            string seulatxt = ((ComboBox)c).Text;
                            seulatxt = seulatxt.Replace(".", ",");
                            if (seulatxt != String.Empty && Double.TryParse(seulatxt, out double r) == true)
                            {
                                Seulakirjasto ke = new Seulakirjasto
                                {
                                    seula = Convert.ToDouble(seulatxt)
                                };
                                selistALL.Add(ke);
                            }
                            /*else
                            {
                                Seulakirjasto ke = new Seulakirjasto
                                {
                                    seula = 0
                                };
                                selistALL.Add(ke);
                            }*/
                        }
                    }

                }
            }

            for (int i = 0; i < 4; i++)//Otetaan ohjealueet talteen yksi kolumni kerrallaan
            {
                switch (i)
                {
                    case 0:
                        foreach (Control c in _kivi.ohjeArvot.Children)
                        {
                            if (c.GetType() == typeof(TextBox))
                            {
                                if (((TextBox)c).Tag.ToString() != null)
                                {
                                    if (((TextBox)c).Tag.ToString() == "sisAla")
                                    {
                                        if (((TextBox)c).Text != String.Empty)
                                        {
                                            string prostxt = ((TextBox)c).Text;
                                            prostxt = prostxt.Replace(".", ",");
                                            string name = ((TextBox)c).Name;
                                            int ind = Convert.ToInt32(Regex.Match(name, @"\d+$").Value);//otetaan objektin järjestysnumero nimestä,
                                            SeulaLapPros ohj = new SeulaLapPros
                                            {
                                                index = ind, 
                                                tulos = Convert.ToDouble(prostxt)
                                            };
                                            sisOhjeAla.Add(ohj);

                                            /*string seulatxt = ((TextBox)c).Text;
                                            seulatxt = seulatxt.Replace(".", ",");
                                            SeulaLapPros ohj = new SeulaLapPros
                                            {
                                                index = j,
                                                tulos = Convert.ToDouble(seulatxt)
                                            };
                                            sisOhjeAla.Add(ohj);*/

                                        }
                                        

                                    }
                                }

                            }
                        }
                        break;
                    case 1:
                        foreach (Control c in _kivi.ohjeArvot.Children)
                        {
                            if (c.GetType() == typeof(TextBox))
                            {
                                if (((TextBox)c).Tag.ToString() != null)
                                {
                                    if (((TextBox)c).Tag.ToString() == "sisYla")
                                    {
                                        if (((TextBox)c).Text != String.Empty)
                                        {
                                            string prostxt = ((TextBox)c).Text;
                                            prostxt = prostxt.Replace(".", ",");
                                            string name = ((TextBox)c).Name;
                                            int ind = Convert.ToInt32(Regex.Match(name, @"\d+$").Value);//otetaan objektin järjestysnumero nimestä,
                                            SeulaLapPros ohj = new SeulaLapPros
                                            {
                                                index = ind,
                                                tulos = Convert.ToDouble(prostxt)
                                            };
                                            sisOhjeYla.Add(ohj);

                                        }
                                        
                                    }
                                }

                            }
                        }
                        break;
                    case 2:
                        foreach (Control c in _kivi.ohjeArvot.Children)
                        {
                            if (c.GetType() == typeof(TextBox))
                            {
                                if (((TextBox)c).Tag.ToString() != null)
                                {
                                    if (((TextBox)c).Tag.ToString() == "uloAla")
                                    {
                                        if (((TextBox)c).Text != String.Empty)
                                        {
                                            string prostxt = ((TextBox)c).Text;
                                            prostxt = prostxt.Replace(".", ",");
                                            string name = ((TextBox)c).Name;
                                            int ind = Convert.ToInt32(Regex.Match(name, @"\d+$").Value);//otetaan objektin järjestysnumero nimestä,
                                            SeulaLapPros ohj = new SeulaLapPros
                                            {
                                                index = ind,
                                                tulos = Convert.ToDouble(prostxt)
                                            };
                                            uloOhjeAla.Add(ohj);

                                        }
                                        
                                    }
                                }

                            }
                        }
                        break;
                    case 3:
                        foreach (Control c in _kivi.ohjeArvot.Children)
                        {
                            if (c.GetType() == typeof(TextBox))
                            {
                                if (((TextBox)c).Tag.ToString() != null)
                                {
                                    if (((TextBox)c).Tag.ToString() == "uloYla")
                                    {
                                        if (((TextBox)c).Text != String.Empty)
                                        {
                                            string prostxt = ((TextBox)c).Text;
                                            prostxt = prostxt.Replace(".", ",");
                                            string name = ((TextBox)c).Name;
                                            int ind = Convert.ToInt32(Regex.Match(name, @"\d+$").Value);//otetaan objektin järjestysnumero nimestä,
                                            SeulaLapPros ohj = new SeulaLapPros
                                            {
                                                index = ind,
                                                tulos = Convert.ToDouble(prostxt)
                                            };
                                            uloOhjeYla.Add(ohj);

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
            /*foreach (SeulaLapPros s in sisOhjeAla)
            {
                Console.WriteLine(s.index+", "+s.tulos);
            }
            Console.WriteLine("---------------------");
            foreach (SeulaLapPros s in sisOhjeYla)
            {
                Console.WriteLine(s.index + ", " + s.tulos);
            }
            Console.WriteLine("---------------------");
            foreach (SeulaLapPros s in uloOhjeAla)
            {
                Console.WriteLine(s.index + ", " + s.tulos);
            }
            Console.WriteLine("---------------------");
            foreach (SeulaLapPros s in uloOhjeYla)
            {
                Console.WriteLine(s.index + ", " + s.tulos);
            }*/

            if (_kayra == null)
            {
                _kayra = new KiviKayra(_kivi, selist, tulist, selistALL, sisOhjeAla, sisOhjeYla, uloOhjeAla, uloOhjeYla);
                _kayra.Show();
            }
            else
            {
                _kayra.Close();
                _kayra = null;
                KayraPiirto(_kivi);
            }
        }
        public void SuljeKayraIkkuna()
        {
            _kayra = null;
        }
    }
}
