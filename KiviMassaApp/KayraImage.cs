using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace KiviMassaApp
{
    class KayraImage
    {
        public MemoryStream KayraKuva(Window win)
        {
            if (win.Name == "kiviohjelma")
            {

                Kiviohjelma _kivi = (Kiviohjelma)win;
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
                //----------------------------------------------------------------------
                //Luodaan listoista käyrä ja otetaan kuva
                //----------------------------------------------------------------------
                PlotModel plotModel = new PlotModel();
                //plotModel.PlotType = PlotType.XY;
                plotModel.IsLegendVisible = false;
                plotModel.PlotMargins = new OxyThickness(15, 15, 15, 15);
                //asetetaan legendan asetukset
                //plotModel.LegendPosition = LegendPosition.TopRight;
                //plotModel.LegendOrientation = LegendOrientation.Horizontal;
                //plotModel.LegendPlacement = LegendPlacement.Outside;
                //Tehdään kokoelma, jossa on kaikki käytössä olevat seulat. Nämä sidotaan X-akseliin
                /*Collection<Item> items = new Collection<Item>();
                for (int i = selistALL.Count - 1; i >= 0; i--)
                {
                    items.Add(new Item(selistALL[i].seula.ToString(), selistALL[i].seula));
                }*/
                //Luodaan Y-akseli
                LinearAxis yaxis = new LinearAxis //Y-akseli
                {
                    Maximum = 100,
                    Minimum = 0,
                    //Title = "Prosentti",
                    TickStyle = TickStyle.Inside,
                    MinorStep = 5,
                    MinorGridlineStyle = LineStyle.Dot,
                    Position = AxisPosition.Left,
                    //AbsoluteMaximum = 100,
                    //AbsoluteMinimum = 0,
                    MajorStep = 10,
                    //MinorStep = 5,
                    MajorGridlineStyle = LineStyle.Dash,
                    //MinorGridlineStyle = LineStyle.Dash,
                    IsZoomEnabled = false,
                    IsPanEnabled = false
                };
                //------------------------------Logaritmiakseli-------------------------
                LogarithmicAxis xaxis = new LogarithmicAxis(); //X-akseli
                if (selist == null || tulist == null || selist.Count == 0 || tulist.Count == 0)
                {
                    //xaxis.Title = "Seula";
                    xaxis.TickStyle = TickStyle.Inside;
                    xaxis.Position = AxisPosition.Bottom;
                    xaxis.MajorGridlineStyle = LineStyle.Solid;
                    //xaxis.Base = logbase;
                    //xaxis.MajorStep = 2;
                    //xaxis.MinorGridlineStyle = LineStyle.Dash;
                    xaxis.Minimum = 0.001;
                    //xaxis.Minimum = seulat[(seulat.Count - 1)].seula;
                    xaxis.IsZoomEnabled = false;
                    xaxis.IsPanEnabled = false;
                }
                else
                {
                    //xaxis.Title = "Seula";
                    xaxis.TickStyle = TickStyle.Inside;
                    xaxis.Position = AxisPosition.Bottom;
                    xaxis.MajorGridlineStyle = LineStyle.Solid;
                    //xaxis.Base = logbase;
                    //xaxis.MajorStep =
                    //xaxis.MinorGridlineStyle = LineStyle.Dash;
                    xaxis.Maximum = selistALL[0].seula;
                    if (selistALL[(selistALL.Count - 1)].seula > 0)
                    {
                        xaxis.Minimum = (selistALL[(selistALL.Count - 1)].seula);
                    }
                    else
                    {
                        xaxis.Minimum = 0.001;
                    }
                    xaxis.IsZoomEnabled = false;
                    xaxis.IsPanEnabled = false;
                    xaxis.AbsoluteMinimum = 0;
                    xaxis.IsZoomEnabled = false;
                    xaxis.IsPanEnabled = false;
                    xaxis.StartPosition = 0;
                }
                //-------------------------------------------------------------------
                //Luodaan X-akseli
                /*CategoryAxis caxis = new CategoryAxis();//X-akseli

                if (selist == null || tulist == null || selist.Count == 0 || tulist.Count == 0) //Jos jokin lista on tyhjä, luodaan tyhjä perusakseli
                {
                    caxis.Title = "Seula";
                    caxis.TickStyle = TickStyle.Inside;
                    caxis.Position = AxisPosition.Bottom;
                    caxis.IsTickCentered = true;
                    caxis.MajorGridlineStyle = LineStyle.Solid;
                    caxis.MajorGridlineColor = OxyColors.DarkSlateGray;
                    caxis.MajorTickSize = 7;
                    //caxis.Maximum = seulat[0].seula;
                    //caxis.Minimum = seulat[(seulat.Count - 1)].seula;
                    caxis.IsZoomEnabled = false;
                    caxis.IsPanEnabled = false;
                    caxis.MinorStep = 0.5;
                    caxis.MajorStep = 1;
                    caxis.ItemsSource = items;
                    caxis.LabelField = "Label";
                }
                else
                {
                    caxis.Title = "Seula";
                    caxis.TickStyle = TickStyle.Inside;
                    caxis.Position = AxisPosition.Bottom;
                    caxis.IsTickCentered = true;
                    caxis.MajorGridlineStyle = LineStyle.Solid;
                    caxis.MajorGridlineColor = OxyColors.DarkSlateGray;
                    caxis.MajorTickSize = 7;
                    //caxis.Maximum = seulat[0].seula;
                    //caxis.Minimum = seulat[(seulat.Count-1)].seula;
                    caxis.IsZoomEnabled = false;
                    caxis.IsPanEnabled = false;
                    caxis.MinorStep = 0.5;
                    caxis.MajorStep = 1;
                    caxis.ItemsSource = items;
                    caxis.LabelField = "Label";
                }
                for (int i = selistALL.Count - 1; i >= 0; i--) //Laittaa Y-akselille otsikot, eli seulat jotka on käytössä tällä hetkellä
                {
                    caxis.ActualLabels.Add(selistALL[i].seula.ToString());
                }*/

                LineSeries l1 = new LineSeries //Tuloskäyrä/viiva
                {
                    Title = "Rakeisuuskäyrä",
                    MarkerType = MarkerType.Circle,
                    CanTrackerInterpolatePoints = false,
                    MarkerSize = 3
                    //LabelFormatString = "Läp%: {1:0.0} %"



                };
                //Luodaan itse viivat
                LineSeries ohje1 = new LineSeries
                {
                    MarkerType = MarkerType.None,
                    CanTrackerInterpolatePoints = false,
                    MarkerSize = 1,
                    Color = OxyColors.CadetBlue
                };
                LineSeries ohje2 = new LineSeries
                {
                    MarkerType = MarkerType.None,
                    CanTrackerInterpolatePoints = false,
                    MarkerSize = 0,
                    Color = OxyColors.CadetBlue
                };
                LineSeries ohje3 = new LineSeries
                {
                    MarkerType = MarkerType.None,
                    CanTrackerInterpolatePoints = false,
                    MarkerSize = 0,
                    Color = OxyColors.Indigo
                };
                LineSeries ohje4 = new LineSeries
                {
                    MarkerType = MarkerType.None,
                    CanTrackerInterpolatePoints = false,
                    MarkerSize = 0,
                    Color = OxyColors.Indigo
                };
                //selist.Reverse();
                //Luodaan listat joihin tulee viivojen pisteet
                List<Pisteet> la = new List<Pisteet>(); //Pääviiva
                List<Pisteet> o1 = new List<Pisteet>();
                List<Pisteet> o2 = new List<Pisteet>();
                List<Pisteet> o3 = new List<Pisteet>();
                List<Pisteet> o4 = new List<Pisteet>();
                //------------------Käytetään CategoryAxisin kanssa--------------------
                /*int j = 0;
                for (int i = tulist.Count - 1; i >= 0; i--)
                {
                    //Syötetään yhden pisteen koordinaatit listaan esineeksi
                    Pisteet l = new Pisteet();
                    l.X = selist[i].index;
                    l.Y = tulist[j].tulos;
                    la.Add(l);
                    j++;
                }//--------------------------------------------------------------------*/
                //---------------Käytetään LogarithmAxisin kanssa---------------------
                for (int i = 0; i < tulist.Count; i++)
                {
                    Pisteet l = new Pisteet();
                    l.X = selist[i].seula;//seulat[i].seula kun käytetään LogarithmAxisia
                    l.Y = tulist[i].tulos;
                    la.Add(l);
                }//--------------------------------------------------------------------*/
                foreach (Control c in _kivi.ohjeArvot.Children)
                {
                    if (c.GetType() == typeof(TextBox))
                    {
                        if (((TextBox)c).Tag.ToString() != null)
                        {
                            if (((TextBox)c).Tag.ToString() == "seulaValue")
                            {
                                if (((TextBox)c).Text != String.Empty)
                                {
                                    string seulatxt = ((TextBox)c).Text;
                                    seulatxt = seulatxt.Replace(".", ",");
                                    string name = ((TextBox)c).Name;
                                    int ind = Convert.ToInt32(Regex.Match(name, @"\d+$").Value);//otetaan objektin järjestysnumero nimestä,

                                    foreach (SeulaLapPros sl in sisOhjeAla)
                                    {
                                        if (ind == sl.index)
                                        {
                                            if (Double.TryParse(seulatxt, out double r) == true)
                                            {
                                                sl.seulaArvo = Convert.ToDouble(seulatxt);
                                            }
                                        }
                                    }
                                    foreach (SeulaLapPros sl in sisOhjeYla)
                                    {
                                        if (ind == sl.index)
                                        {
                                            if (Double.TryParse(seulatxt, out double r) == true)
                                            {
                                                sl.seulaArvo = Convert.ToDouble(seulatxt);
                                            }
                                        }
                                    }
                                    foreach (SeulaLapPros sl in uloOhjeAla)
                                    {
                                        if (ind == sl.index)
                                        {
                                            if (Double.TryParse(seulatxt, out double r) == true)
                                            {
                                                sl.seulaArvo = Convert.ToDouble(seulatxt);
                                            }
                                        }
                                    }
                                    foreach (SeulaLapPros sl in uloOhjeYla)
                                    {
                                        if (ind == sl.index)
                                        {
                                            if (Double.TryParse(seulatxt, out double r) == true)
                                            {
                                                sl.seulaArvo = Convert.ToDouble(seulatxt);
                                            }
                                        }
                                    }

                                }
                            }
                        }

                    }
                }
                for (int i = sisOhjeAla.Count - 1; i >= 0; i--)
                {
                    Pisteet l = new Pisteet();
                    l.X = sisOhjeAla[i].seulaArvo;
                    l.Y = sisOhjeAla[i].tulos;
                    o1.Add(l);
                }
                for (int i = sisOhjeYla.Count - 1; i >= 0; i--)
                {
                    Pisteet l = new Pisteet();
                    l.X = sisOhjeYla[i].seulaArvo;
                    l.Y = sisOhjeYla[i].tulos;
                    o2.Add(l);
                }
                for (int i = uloOhjeAla.Count - 1; i >= 0; i--)
                {
                    Pisteet l = new Pisteet();
                    l.X = uloOhjeAla[i].seulaArvo;
                    l.Y = uloOhjeAla[i].tulos;
                    o3.Add(l);
                }
                for (int i = uloOhjeYla.Count - 1; i >= 0; i--)
                {
                    Pisteet l = new Pisteet();
                    l.X = uloOhjeYla[i].seulaArvo;
                    l.Y = uloOhjeYla[i].tulos;
                    o4.Add(l);
                }

                //Laitetaan luodut pistelistat viivoihinsa
                foreach (Pisteet e in la)
                {
                    l1.Points.Add(new DataPoint(e.X, e.Y));
                }
                foreach (Pisteet e in o1)
                {
                    ohje1.Points.Add(new DataPoint(e.X, e.Y));
                }
                foreach (Pisteet e in o2)
                {
                    ohje2.Points.Add(new DataPoint(e.X, e.Y));
                }
                foreach (Pisteet e in o3)
                {
                    ohje3.Points.Add(new DataPoint(e.X, e.Y));
                }
                foreach (Pisteet e in o4)
                {
                    ohje4.Points.Add(new DataPoint(e.X, e.Y));
                }
                //----------------------Kovakoodatut arvot, testitapaus-----------------------------
                /*double[] ar = new double[] { 0.063, 0.125, 0.25, 0.5, 1, 2, 4, 6, 8, 12, 16, 18, 20, 25, 30, 64, 100, 200 };
                double[] er = new double[] { 1.8, 3, 4.5, 5.6, 6.5, 8.3, 9.0, 9.9, 13.8, 15.6, 16.5, 17.4, 18.6, 20.4, 30.8, 31.4, 50.5, 62.7 };
                List<Pisteet> la = new List<Pisteet>();
                for (int i = 0; i < ar.Length; i++)//prosentit.Count
                {
                    Pisteet l = new Pisteet();
                    l.X = ar[i];
                    l.Y = er[i];
                    la.Add(l);
                }
                foreach (Pisteet e in la)
                {
                    l1.Points.Add(new DataPoint(e.X, e.Y));
                }*///-----------------------------------------------------------------------------------
                   //Syötetään kaikki luodut viivat ja akselit kaavioon
                plotModel.Axes.Add(yaxis);
                plotModel.Axes.Add(xaxis);
                //plotModel.Axes.Add(caxis);
                plotModel.Series.Add(l1);
                plotModel.Series.Add(ohje1);
                plotModel.Series.Add(ohje2);
                plotModel.Series.Add(ohje3);
                plotModel.Series.Add(ohje4);


                //Palautetaan kuva viivakaaviosta
                var kuvastream = new MemoryStream();
                var pngExporter = new OxyPlot.Wpf.PngExporter { Width = 750, Height = 500, Background = OxyColors.White };
                pngExporter.Export(plotModel, kuvastream);
                return kuvastream;


            }
            else if(win.Name == "massaohjelma")
            {
                Massaohjelma _massa = (Massaohjelma)win;
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

                //----------------------------------------------------------------------
                //Luodaan listoista käyrä ja otetaan kuva
                //----------------------------------------------------------------------
                PlotModel plotModel = new PlotModel();
                //plotModel.PlotType = PlotType.XY;
                plotModel.IsLegendVisible = false;
                plotModel.PlotMargins = new OxyThickness(15, 15, 15, 15);

                LinearAxis yaxis = new LinearAxis //Y-akseli
                {
                    Maximum = 100,
                    Minimum = 0,
                    Title = "Prosentti",
                    TickStyle = TickStyle.Inside,
                    MinorTickSize = 4,
                    Position = AxisPosition.Left,
                    //AbsoluteMaximum = 100,
                    //AbsoluteMinimum = 0,
                    MajorStep = 10,
                    MinorStep = 5,
                    MajorGridlineStyle = LineStyle.Dash,
                    MinorGridlineStyle = LineStyle.Dot,
                    IsZoomEnabled = false,
                    IsPanEnabled = false
                };
                //------------------------------Logaritmiakseli-------------------------
                LogarithmicAxis xaxis = new LogarithmicAxis(); //X-akseli

                if (selist == null || tulist == null || selist.Count == 0 || tulist.Count == 0)
                {
                    //xaxis.Title = "Seula";
                    xaxis.TickStyle = TickStyle.Inside;
                    xaxis.Position = AxisPosition.Bottom;
                    xaxis.MajorGridlineStyle = LineStyle.Solid;
                    xaxis.MajorStep = 2;
                    xaxis.Minimum = 0.001;
                    //xaxis.MinorGridlineStyle = LineStyle.Dash;
                    //xaxis.Maximum = seulat[0].seula;
                    //xaxis.Minimum = seulat[(seulat.Count - 1)].seula;
                    xaxis.IsZoomEnabled = false;
                    xaxis.IsPanEnabled = false;
                }
                else
                {
                    //xaxis.Title = "Seula";
                    xaxis.TickStyle = TickStyle.Inside;
                    xaxis.Position = AxisPosition.Bottom;
                    xaxis.MajorGridlineStyle = LineStyle.Solid;
                    //xaxis.Base = logbase;
                    //xaxis.MajorStep =
                    //xaxis.MinorGridlineStyle = LineStyle.Dash;
                    xaxis.Maximum = selistALL[0].seula;
                    //xaxis.Minimum = 0.0001;
                    if (selistALL[(selistALL.Count - 1)].seula > 0)
                    {
                        xaxis.Minimum = (selistALL[(selistALL.Count - 1)].seula);
                    }
                    else
                    {
                        xaxis.Minimum = 0.001;
                    }
                    xaxis.AbsoluteMinimum = 0;
                    xaxis.StartPosition = 0;
                    xaxis.IsZoomEnabled = false;
                    xaxis.IsPanEnabled = false;

                }
                /*CategoryAxis caxis = new CategoryAxis();//X-akseli

                if (seulat == null || prosentit == null || seulat.Count == 0 || prosentit.Count == 0)
                {
                    caxis.Title = "Seula";
                    caxis.TickStyle = TickStyle.Inside;
                    caxis.Position = AxisPosition.Bottom;
                    caxis.MajorGridlineStyle = LineStyle.Solid;
                    caxis.MinorGridlineStyle = LineStyle.Dash;
                    //caxis.Maximum = seulat[0].seula;
                    //caxis.Minimum = seulat[(seulat.Count - 1)].seula;
                    caxis.IsZoomEnabled = false;
                    caxis.IsPanEnabled = false;
                }
                else
                {
                    caxis.Title = "Seula";
                    caxis.TickStyle = TickStyle.Inside;
                    caxis.Position = AxisPosition.Bottom;
                    //caxis.MajorGridlineStyle = LineStyle.Solid;
                    //caxis.MinorGridlineStyle = LineStyle.Dash;
                    //caxis.Maximum = seulat[0].seula;
                    //caxis.Minimum = seulat[(seulat.Count-1)].seula;
                    caxis.IsZoomEnabled = true;
                    caxis.IsPanEnabled = true;
                    caxis.AxislineStyle = LineStyle.Solid;
                    caxis.MinorStep = 1;
                }
                for (int i = kaikkiseulat.Count - 1; i >= 0; i--) //Laittaa Y-akselille otsikot, eli seulat jotka on käytössä tällä hetkellä
                {
                    caxis.Labels.Add(kaikkiseulat[i].seula.ToString());
                }*/

                LineSeries l1 = new LineSeries //Tuloskäyrä/viiva
                {
                    Title = "Rakeisuuskäyrä",
                    MarkerType = MarkerType.Circle,
                    CanTrackerInterpolatePoints = false,
                    MarkerSize = 5

                };
                LineSeries ohje1 = new LineSeries
                {
                    MarkerType = MarkerType.None,
                    CanTrackerInterpolatePoints = false,
                    MarkerSize = 0,
                    Color = OxyColors.CadetBlue
                };
                LineSeries ohje2 = new LineSeries
                {
                    MarkerType = MarkerType.None,
                    CanTrackerInterpolatePoints = false,
                    MarkerSize = 0,
                    Color = OxyColors.Indigo
                };
                //seulat.Reverse();
                /*sisOhjeAla.Reverse();
                sisOhjeYla.Reverse();
                uloOhjeAla.Reverse();
                uloOhjeYla.Reverse();*/
                List<Pisteet> la = new List<Pisteet>(); //Pääviiva
                List<Pisteet> o1 = new List<Pisteet>();
                List<Pisteet> o2 = new List<Pisteet>();
                //------------------Käytetään CategoryAxisin kanssa--------------------
                /*int j = 0;
                for (int i = prosentit.Count - 1; i >= 0; i--)
                {
                    Pisteet l = new Pisteet();
                    l.X = seulat[i].index;
                    l.Y = prosentit[j].tulos;
                    la.Add(l);
                    j++;
                }//--------------------------------------------------------------------*/

                //---------------Käytetään LogarithmAxisin kanssa---------------------
                for (int i = 0; i < tulist.Count; i++)
                {
                    Pisteet l = new Pisteet();
                    l.X = selist[i].seula;//seulat[i].seula kun käytetään LogarithmAxisia
                    l.Y = tulist[i].tulos;
                    la.Add(l);
                }//--------------------------------------------------------------------
                foreach (Control c in _massa.ohjeAlue.Children)
                {
                    if (c.GetType() == typeof(TextBox))
                    {
                        if (((TextBox)c).Tag.ToString() != null)
                        {
                            if (((TextBox)c).Tag.ToString() == "seulaValue")
                            {
                                if (((TextBox)c).Text != String.Empty)
                                {
                                    string seulatxt = ((TextBox)c).Text;
                                    seulatxt = seulatxt.Replace(".", ",");
                                    string name = ((TextBox)c).Name;
                                    int ind = Convert.ToInt32(Regex.Match(name, @"\d+$").Value);//otetaan objektin järjestysnumero nimestä,

                                    foreach (SeulaLapPros sl in sisOhjeAla)
                                    {
                                        if (ind == sl.index)
                                        {
                                            if (Double.TryParse(seulatxt, out double r) == true)
                                            {
                                                sl.seulaArvo = Convert.ToDouble(seulatxt);
                                            }
                                        }
                                    }
                                    foreach (SeulaLapPros sl in sisOhjeYla)
                                    {
                                        if (ind == sl.index)
                                        {
                                            if (Double.TryParse(seulatxt, out double r) == true)
                                            {
                                                sl.seulaArvo = Convert.ToDouble(seulatxt);
                                            }
                                        }
                                    }

                                }
                            }
                        }
                    }
                }
                for (int i = sisOhjeAla.Count - 1; i >= 0; i--)
                {
                    Pisteet l = new Pisteet();
                    l.X = sisOhjeAla[i].seulaArvo;
                    l.Y = sisOhjeAla[i].tulos;
                    o1.Add(l);
                }
                for (int i = sisOhjeYla.Count - 1; i >= 0; i--)
                {
                    Pisteet l = new Pisteet();
                    l.X = sisOhjeYla[i].seulaArvo;
                    l.Y = sisOhjeYla[i].tulos;
                    o2.Add(l);
                }


                foreach (Pisteet e in la)
                {
                    l1.Points.Add(new DataPoint(e.X, e.Y));
                }
                foreach (Pisteet e in o1)
                {
                    ohje1.Points.Add(new DataPoint(e.X, e.Y));
                }
                foreach (Pisteet e in o2)
                {
                    ohje2.Points.Add(new DataPoint(e.X, e.Y));
                }
                //----------------------Kovakoodatut arvot, testitapaus-----------------------------
                /*double[] ar = new double[] { 0.063, 0.125, 0.25, 0.5, 1, 2, 4, 6, 8, 12, 16, 18, 20, 25, 30, 64, 100, 200 };
                double[] er = new double[] { 1.8, 3, 4.5, 5.6, 6.5, 8.3, 9.0, 9.9, 13.8, 15.6, 16.5, 17.4, 18.6, 20.4, 30.8, 31.4, 50.5, 62.7 };
                List<Pisteet> la = new List<Pisteet>();
                for (int i = 0; i < ar.Length; i++)//prosentit.Count
                {
                    Pisteet l = new Pisteet();
                    l.X = ar[i];
                    l.Y = er[i];
                    la.Add(l);
                }
                foreach (Pisteet e in la)
                {
                    l1.Points.Add(new DataPoint(e.X, e.Y));
                }*///-----------------------------------------------------------------------------------

                plotModel.Axes.Add(yaxis);
                plotModel.Axes.Add(xaxis);
                //plotModel.Axes.Add(caxis);
                plotModel.Series.Add(l1);
                plotModel.Series.Add(ohje1);
                plotModel.Series.Add(ohje2);


                //Palautetaan kuva viivakaaviosta
                var kuvastream = new MemoryStream();
                var pngExporter = new OxyPlot.Wpf.PngExporter { Width = 750, Height = 500, Background = OxyColors.White };
                pngExporter.Export(plotModel, kuvastream);
                return kuvastream;
                
            }
            else
            {
                return null;
            }
        }
        private class Pisteet
        {
            public double X { get; set; }
            public double Y { get; set; }
        }
        /*private class Item
        {
            public Item(string v, double seula)
            {
                Label = v;
                Value = seula;
            }

            public string Label { get; set; }
            public double Value { get; set; }
        }*/

    }
}
