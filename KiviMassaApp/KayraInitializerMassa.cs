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
    class KayraInitializerMassa
    {
        private Window _kayra = null;
        public void KayraPiirto(Massaohjelma mas)
        {
            Massaohjelma _massa = mas;
            //Näytetään rakeisuuskäyrä syötettyjen arvojen avulla

            List<SeulakirjastoIndex> selist = new List<SeulakirjastoIndex>();//Seulat joita on käytetty laskennassa, eli X-arvot.
            List<SeulaLapPros> tulist = new List<SeulaLapPros>();//Läpäisyprosenttitulokset, eli Y-arvot
            List<Seulakirjasto> selistALL = new List<Seulakirjasto>();//Kaikki seulat mitä on valittuna. Tehdään täysi X-akseli tällä.
            List<SeulaLapPros> sisOhjeAla = new List<SeulaLapPros>();//Sisempi ohjealue, alempi ohje%
            List<SeulaLapPros> sisOhjeYla = new List<SeulaLapPros>();//Sisempi ohjealue, ylempi ohje%



            //Lukee tarvittavat prosenttiarvot ja lisää ne tulist-listaan
            //Ottaa valitut seulat ohjelmasta, ottaa talteen niiden sijainnin järjestyslukuna ja laittaa ne selist-listaan
            //Tuloksissa saattaa olla välejä (kaikkeja rivejä ei täytetty) joten koodi tarkistaa sen myös

            if (_massa.lapaisypros1.Text != String.Empty)
            {
                SeulaLapPros sl = new SeulaLapPros
                {
                    index = 0,
                    tulos = Convert.ToDouble(_massa.lapaisypros1.Text)
                };
                tulist.Add(sl);
                string seulatxt = _massa.Seula1.Text;
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
                /*SeulaLapPros sl = new SeulaLapPros
                {
                    index = 0,
                    tulos = Convert.ToDouble(_massa.lapaisypros1.Text)
                };
                tulist.Add(sl);
                string seulatxt = _massa.Seula1.Text;
                seulatxt = seulatxt.Replace(".", ",");
                SeulakirjastoIndex ke = new SeulakirjastoIndex
                {
                    index = 17,
                    seula = Convert.ToDouble(seulatxt)
                };
                selist.Add(ke);*/
            }
            if (_massa.lapaisypros2.Text != String.Empty)
            {
                SeulaLapPros sl = new SeulaLapPros
                {
                    index = 1,
                    tulos = Convert.ToDouble(_massa.lapaisypros1.Text)
                };
                tulist.Add(sl);
                string seulatxt = _massa.Seula1.Text;
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
            if (_massa.lapaisypros3.Text != String.Empty)
            {
                SeulaLapPros sl = new SeulaLapPros
                {
                    index = 2,
                    tulos = Convert.ToDouble(_massa.lapaisypros3.Text)
                };
                tulist.Add(sl);
                string seulatxt = _massa.Seula3.Text;
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
            if (_massa.lapaisypros4.Text != String.Empty)
            {
                SeulaLapPros sl = new SeulaLapPros
                {
                    index = 3,
                    tulos = Convert.ToDouble(_massa.lapaisypros4.Text)
                };
                tulist.Add(sl);
                string seulatxt = _massa.Seula4.Text;
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
            if (_massa.lapaisypros5.Text != String.Empty)
            {
                SeulaLapPros sl = new SeulaLapPros
                {
                    index = 4,
                    tulos = Convert.ToDouble(_massa.lapaisypros5.Text)
                };
                tulist.Add(sl);
                string seulatxt = _massa.Seula5.Text;
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
            if (_massa.lapaisypros6.Text != String.Empty)
            {
                SeulaLapPros sl = new SeulaLapPros
                {
                    index = 5,
                    tulos = Convert.ToDouble(_massa.lapaisypros6.Text)
                };
                tulist.Add(sl);
                string seulatxt = _massa.Seula6.Text;
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
            if (_massa.lapaisypros7.Text != String.Empty)
            {
                SeulaLapPros sl = new SeulaLapPros
                {
                    index = 6,
                    tulos = Convert.ToDouble(_massa.lapaisypros7.Text)
                };
                tulist.Add(sl);
                string seulatxt = _massa.Seula7.Text;
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
            if (_massa.lapaisypros8.Text != String.Empty)
            {
                SeulaLapPros sl = new SeulaLapPros
                {
                    index = 7,
                    tulos = Convert.ToDouble(_massa.lapaisypros8.Text)
                };
                tulist.Add(sl);
                string seulatxt = _massa.Seula8.Text;
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
            if (_massa.lapaisypros9.Text != String.Empty)
            {
                SeulaLapPros sl = new SeulaLapPros
                {
                    index = 8,
                    tulos = Convert.ToDouble(_massa.lapaisypros9.Text)
                };
                tulist.Add(sl);
                string seulatxt = _massa.Seula9.Text;
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
            if (_massa.lapaisypros10.Text != String.Empty)
            {
                SeulaLapPros sl = new SeulaLapPros
                {
                    index = 9,
                    tulos = Convert.ToDouble(_massa.lapaisypros10.Text)
                };
                tulist.Add(sl);
                string seulatxt = _massa.Seula10.Text;
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
            if (_massa.lapaisypros11.Text != String.Empty)
            {
                SeulaLapPros sl = new SeulaLapPros
                {
                    index = 10,
                    tulos = Convert.ToDouble(_massa.lapaisypros11.Text)
                };
                tulist.Add(sl);
                string seulatxt = _massa.Seula11.Text;
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
            if (_massa.lapaisypros12.Text != String.Empty)
            {
                SeulaLapPros sl = new SeulaLapPros
                {
                    index = 11,
                    tulos = Convert.ToDouble(_massa.lapaisypros12.Text)
                };
                tulist.Add(sl);
                string seulatxt = _massa.Seula12.Text;
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
            if (_massa.lapaisypros13.Text != String.Empty)
            {
                SeulaLapPros sl = new SeulaLapPros
                {
                    index = 12,
                    tulos = Convert.ToDouble(_massa.lapaisypros13.Text)
                };
                tulist.Add(sl);
                string seulatxt = _massa.Seula13.Text;
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
            if (_massa.lapaisypros14.Text != String.Empty)
            {
                SeulaLapPros sl = new SeulaLapPros
                {
                    index = 13,
                    tulos = Convert.ToDouble(_massa.lapaisypros14.Text)
                };
                tulist.Add(sl);
                string seulatxt = _massa.Seula14.Text;
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
            if (_massa.lapaisypros15.Text != String.Empty)
            {
                SeulaLapPros sl = new SeulaLapPros
                {
                    index = 14,
                    tulos = Convert.ToDouble(_massa.lapaisypros15.Text)
                };
                tulist.Add(sl);
                string seulatxt = _massa.Seula15.Text;
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
            if (_massa.lapaisypros16.Text != String.Empty)
            {
                SeulaLapPros sl = new SeulaLapPros
                {
                    index = 15,
                    tulos = Convert.ToDouble(_massa.lapaisypros16.Text)
                };
                tulist.Add(sl);
                string seulatxt = _massa.Seula16.Text;
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
            


            foreach (Control c in _massa.seulaArvot.Children) //Kaikille esineille seulaArvot-canvasissa. Tarkoituksena ottaa kaikki valitut seulat dropdown-valikoista talteen
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
                        }
                    }

                }
            }

            for (int i = 0; i < 2; i++)//Otetaan ohjealueet talteen yksi kolumni kerrallaan
            {
                switch (i)
                {
                    case 0:
                        foreach (Control c in _massa.ohjeAlue.Children)
                        {
                            if (c.GetType() == typeof(TextBox))
                            {
                                if (((TextBox)c).Tag.ToString() != null)
                                {
                                    if (((TextBox)c).Tag.ToString() == "Ala")
                                    {
                                        if (((TextBox)c).Text != String.Empty)
                                        {
                                            string prostxt = ((TextBox)c).Text;
                                            prostxt = prostxt.Replace(".", ",");
                                            string name = ((TextBox)c).Name;
                                            int ind = Convert.ToInt32(Regex.Match(name, @"\d+$").Value);//otetaan objektin järjestysnumero nimestä,
                                            SeulaLapPros ohj = new SeulaLapPros
                                            {
                                                index = ind, //1-16
                                                tulos = Convert.ToDouble(prostxt)
                                            };
                                            sisOhjeAla.Add(ohj);

                                        }
                                    }
                                }

                            }
                        }
                        break;
                    case 1:
                        foreach (Control c in _massa.ohjeAlue.Children)
                        {
                            if (c.GetType() == typeof(TextBox))
                            {
                                if (((TextBox)c).Tag.ToString() != null)
                                {
                                    if (((TextBox)c).Tag.ToString() == "Yla")
                                    {
                                        if (((TextBox)c).Text != String.Empty)
                                        {
                                            string prostxt = ((TextBox)c).Text;
                                            prostxt = prostxt.Replace(".", ",");
                                            string name = ((TextBox)c).Name;
                                            int ind = Convert.ToInt32(Regex.Match(name, @"\d+$").Value);//otetaan objektin järjestysnumero nimestä,
                                            SeulaLapPros ohj = new SeulaLapPros
                                            {
                                                index = ind, //1-16
                                                tulos = Convert.ToDouble(prostxt)
                                            };
                                            sisOhjeYla.Add(ohj);

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
            if(_kayra == null)
            {
                _kayra = new MassaKayra(_massa, selist, tulist, selistALL, sisOhjeAla, sisOhjeYla);
                _kayra.Show();
            }
            else
            {
                _kayra.Close();
                _kayra = null;
                KayraPiirto(_massa);
            } 
        }
 
        public void SuljeKayraIkkuna()
        {
            _kayra = null;
        }
    }
}
