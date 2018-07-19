using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class KiviKayra : Window
    {

        //HUOMIO: Suurin osa kommenteissa olevat koodinpätkät ovat vanhaa toimintoa ja hyödyttömiä. Ne säilytettiin kaiken varalta jos ne olisi saatu toimimaan
        //Tarkoituksena oli saada tehtyä logaritminen akseli jossa näkyy seulat arvoina akselia pitkin. Tämä ei onnistunut, joten kokeilimme categoryAxisia.
        //Saimme seulat näkyville, mutta akseli ei ollut logaritminen. Joten palautimme normaalin base 10 logaritmiakselin ja laitoimme categoryaxisin koodin kommentteihin

        public PlotController customController { get; private set; }
        Kiviohjelma _kivi;
        List<SeulakirjastoIndex> seulat;
        List<Seulakirjasto> kaikkiseulat;
        List<SeulaLapPros> prosentit;
        List<SeulaLapPros> sisOhjeAla, sisOhjeYla, uloOhjeAla, uloOhjeYla;
        public KiviKayra(Window kiv, List<SeulakirjastoIndex> s, List<SeulaLapPros> p, List<Seulakirjasto> alls, List<SeulaLapPros> sisAla, List<SeulaLapPros> sisYla, List<SeulaLapPros> uloAla, List<SeulaLapPros> uloYla)
        {
            InitializeComponent();
            
            _kivi = (Kiviohjelma)kiv;
            seulat = s;
            prosentit = p;
            kaikkiseulat = alls;
            sisOhjeAla = sisAla;
            sisOhjeYla = sisYla;
            uloOhjeAla = uloAla;
            uloOhjeYla = uloYla;

            //Tällä saadaan osoitin toimimaan niin, että ei tarvitse muuta tehdä kuin laittaa hiiren osoitin koordinaattipisteen päälle 
            //niin se näyttää koordinaattitiedot automaattisesti
            customController = new PlotController();
            customController.UnbindMouseDown(OxyMouseButton.Left);
            customController.BindMouseEnter(PlotCommands.HoverSnapTrack);

            var model = new PlotModel { Title = "Rakeisuuskäyrä", Subtitle = "Vie hiiri pisteiden lähelle nähdäksesi arvot" };
            //model.PlotType = PlotType.XY;
            model.LegendPosition = LegendPosition.TopRight;
            model.LegendOrientation = LegendOrientation.Horizontal;
            model.LegendPlacement = LegendPlacement.Outside;
            /*Collection<Item> items = new Collection<Item>();
            for (int i = kaikkiseulat.Count - 1; i >= 0; i--)
            {
                items.Add(new Item(kaikkiseulat[i].seula.ToString(), kaikkiseulat[i].seula));
            }*/
            LinearAxis yaxis = new LinearAxis //Y-akseli
            {
                Maximum = 100,
                Minimum = 0,
                Title = "Prosentti",
                TickStyle = TickStyle.Inside,
                MinorTickSize = 4,
                Position = AxisPosition.Left,
                MinorStep = 5,
                MinorGridlineStyle = LineStyle.Dot,
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

            if (seulat == null || prosentit == null || seulat.Count == 0 || prosentit.Count == 0)
            {
                xaxis.Title = "Seula";
                xaxis.TickStyle = TickStyle.Inside;
                xaxis.Position = AxisPosition.Bottom;
                xaxis.MajorGridlineStyle = LineStyle.Solid;
                //xaxis.MajorStep = 2;
                //xaxis.MinorGridlineStyle = LineStyle.Dash;
                //xaxis.Maximum = seulat[0].seula;
                xaxis.Minimum = 0.001;
                xaxis.IsZoomEnabled = false;
                xaxis.IsPanEnabled = false;
            }
            else
            {
                xaxis.Title = "Seula";
                xaxis.TickStyle = TickStyle.Inside;
                xaxis.Position = AxisPosition.Bottom;
                xaxis.MajorGridlineStyle = LineStyle.Solid;
                //xaxis.Base = logbase;
                //xaxis.MajorStep =
                //xaxis.MinorGridlineStyle = LineStyle.Dash;
                xaxis.Maximum = kaikkiseulat[0].seula;
                //xaxis.Minimum = 0.001;
                if(kaikkiseulat[(kaikkiseulat.Count - 1)].seula > 0)
                {
                    xaxis.Minimum = (kaikkiseulat[(kaikkiseulat.Count - 1)].seula);
                }
                else
                {
                    xaxis.Minimum = 0.001;
                }
                xaxis.AbsoluteMinimum = 0;
                xaxis.IsZoomEnabled = false;
                xaxis.IsPanEnabled = false;
                xaxis.StartPosition = 0;

            }
            /*CategoryAxis caxis = new CategoryAxis();//X-akseli
            
            if (seulat == null || prosentit == null || seulat.Count == 0 || prosentit.Count == 0)
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
            for (int i = kaikkiseulat.Count-1; i >= 0; i--) //Laittaa Y-akselille otsikot, eli seulat jotka on käytössä tällä hetkellä
            {
                caxis.ActualLabels.Add(kaikkiseulat[i].seula.ToString());
            }*/

            LineSeries l1 = new LineSeries //Tuloskäyrä/viiva
            {
                Title = "Rakeisuuskäyrä",
                MarkerType = MarkerType.Circle,
                CanTrackerInterpolatePoints = false,
                MarkerSize = 5
                //LabelFormatString = "Läp%: {1:0.0} %"
                


            };
            LineSeries ohje1 = new LineSeries
            {
                Title = "Sisäiset ohjealueet",
                MarkerType = MarkerType.None,
                CanTrackerInterpolatePoints = false,
                MarkerSize = 0,
                Color = OxyColors.CadetBlue,
                RenderInLegend = true
            };
            LineSeries ohje2 = new LineSeries
            {
                //Title = "Sisäinen ylempi ohjealue",
                MarkerType = MarkerType.None,
                CanTrackerInterpolatePoints = false,
                MarkerSize = 0,
                Color = OxyColors.CadetBlue,
                RenderInLegend = false
            };
            LineSeries ohje3 = new LineSeries
            {
                Title = "Ulkoiset ohjealueet",
                MarkerType = MarkerType.None,
                CanTrackerInterpolatePoints = false,
                MarkerSize = 0,
                Color = OxyColors.Indigo,
                RenderInLegend = true
            };
            LineSeries ohje4 = new LineSeries
            {
                //Title = "Ulkoinen ylempi ohjealue",
                MarkerType = MarkerType.None,
                CanTrackerInterpolatePoints = false,
                MarkerSize = 0,
                Color = OxyColors.Indigo,
                RenderInLegend = false
            };
            //seulat.Reverse();
            /*sisOhjeAla.Reverse();
            sisOhjeYla.Reverse();
            uloOhjeAla.Reverse();
            uloOhjeYla.Reverse();*/
            List<Pisteet> la = new List<Pisteet>(); //Pääviiva
            List<Pisteet> o1 = new List<Pisteet>();
            List<Pisteet> o2 = new List<Pisteet>();
            List<Pisteet> o3 = new List<Pisteet>();
            List<Pisteet> o4 = new List<Pisteet>();
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
            for (int i = 0; i < prosentit.Count; i++)  
            {
                Pisteet l = new Pisteet();
                l.X = seulat[i].seula;//seulat[i].seula kun käytetään LogarithmAxisia
                l.Y = prosentit[i].tulos;
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
            for (int i = sisOhjeAla.Count-1; i >= 0; i--)
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

            model.Axes.Add(yaxis);
            model.Axes.Add(xaxis);
            //model.Axes.Add(caxis);
            model.Series.Add(l1);
            model.Series.Add(ohje1);
            model.Series.Add(ohje2);
            model.Series.Add(ohje3);
            model.Series.Add(ohje4);
            KiviModel = model;
            this.DataContext = this;

        }

        private void Window_Closed(object sender, EventArgs e)
        {
            //kertoo Kivi-ikkunalle että käyrä on suljettu
            //käytetään siihen että ei voi olla useampi käyrä auki yhtä aikaa
            seulat = null;
            prosentit = null;
            _kivi.SuljeKayraIkkuna();
        }

        public PlotModel KiviModel { get; set; }

        private class Pisteet
        {
            public double X { get; set; }
            public double Y { get; set; }
        }
        /*private class Item //Käytettiin caxiksen kanssa, ei enää käytössä. 
        {
            public Item(string v, double seula)
            {
                Label = v;
                Value = seula;
            }

            public string Label { get; set; }
            public double Value { get; set; }
        }*/

        private void btnSulje_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
