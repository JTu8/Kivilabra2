
using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Luokka sisältää kaikki tarvittavat laskut sovelluksen käyttämiseen
/// Ei tietääkseni tarvitse muuttaa näitä
/// </summary>
namespace KiviMassaApp
{
    public class Laskut
    {
        //-------------------KIVI-OHJELMAN LASKUT---------------------
        public double seulalleJai(double r, double m)
        {
            //Monta prosenttia jäi seulalle materiaalia
            //R * 100 / M
            //R = seulalle jääneen materiaalin massa
            //M = Koko näytemäärä grammoina
            return (r * (100 / m));
	    }
        public double PunnitusYhteensa(double[] arvot)
        {
            //Punnitusmäärä yhteensä grammoina
            //r1+r2+r3...
            //r1 jne = seulalle jääneen materiaalin massa
            //arvot tulee arrayna funktioon

            double tulos = 0;
            for (int i = 0; i < arvot.Length; i++)
            {
                tulos += arvot[i];
            }
            return tulos;
        }
        public List<SyotetytArvot> LapaisyProsentti(List<SyotetytArvot> r, double m, int jakoindex, double kerroin)
        {
            //prosenttimäärä massasta mikä meni seulasta läpi
            //100-SUM(100*r/m)
            //r = seulalle jääneen materiaalin massa
            //m = koko näytemäärä grammoina

            List<SyotetytArvot> tulos = new List<SyotetytArvot>();
            int l = 0;
            foreach(SyotetytArvot ra in r)
            {
                //Console.WriteLine("ra arvot: "+ra.index+", "+ra.syote);
                //Console.WriteLine("tulosCount: "+tulos.Count+",  l count: "+l);
                
                if(l == 0)
                {
                    if (ra.syote.HasValue && ra.syote.ToString() != "")
                    {
                        //Lisää ensimmäisen tuloksen tuloslistaan
                        if(ra.index >= jakoindex)
                        {
                            tulos.Add(new SyotetytArvot() { index = ra.index, syote = (100 - (100 * (ra.syote * kerroin)/ m)) });
                        }
                        else
                        {
                            tulos.Add(new SyotetytArvot() { index = ra.index, syote = (100 - (100 * ra.syote / m)) });
                        }
                        
                        //Console.WriteLine("Lisätään ensimmäinen luku");
                    }
                }
                else
                {
                    if (ra.syote.HasValue && ra.syote.ToString() != "")
                    {
                        if (l+1 < r.Count)
                        {
                            if (ra.index >= jakoindex)
                            {
                                tulos.Add(new SyotetytArvot() { index = ra.index, syote = (tulos[tulos.Count - 1].syote - (100 * (ra.syote * kerroin) / m))  });
                            }
                            else
                            {
                                tulos.Add(new SyotetytArvot() { index = ra.index, syote = (tulos[tulos.Count - 1].syote - (100 * ra.syote / m)) });
                            }
                            //Lisää kaikki välissä olevat tulokset tuloslistaan
                            
                        }
                        else
                        {
                            //Lisää viimeisen tuloksen tuloslistaan, joka on aina 0 %
                            tulos.Add(new SyotetytArvot() { index = r[tulos.Count].index, syote = 0.0f });
                            //Console.WriteLine("Lisätään viimeinen luku");
                        }
                    }
                }
                l++;

            }
            return tulos;
        
        }
        public double KosteusProsentti(double kuv, double kos)
        {
            //Kosteusprosentti w (%)
            //w = [(kos-kuv)/kuv]*100
            //kos = kostean näytteen massa grammoina
            //kuv = kuivan näytteen massa grammoina
            return ((kos-kuv)/kuv)*100;
        }
        //-------------------MASSA-OHJELMAN LASKUT--------------------
        public double FillerinMaara(double sf1, double sf2)
        {
            //Lasketaan fillerin määrä
            //f = sf2 - sf1
            //sf1 = sentrifuugi + paperi (g)
            //sf2 = sentrifuugi + paperi + filleri (g)
            return sf2 - sf1;
        }
        public double Sideainepitoisuus(double M1, double M2, double R, double f)
        {
            //Lasketaan sideaineen määrä prosentteina
            //s = [M1 - (M2 - R + f)] / M1 * 100
            //M1 = näytemäärä ennen testiä (g)
            //M2 = Rummun ja näytteen yhteismassa testin jälkeen (g)
            //R = rummun paino (g)
            //f = filleri (g)
            return (M1 - (M2 - R + f)) / M1 * 100;
        }
        public double Sideainemaara(double M1, double M2, double R, double f)
        {
            //Lasketaan sideaineen määrä grammoina
            //s = M1 - (M2 - R + f)
            //M1 = näytemäärä ennen testiä (g)
            //M2 = Rummun ja näytteen yhteismassa testin jälkeen (g)
            //R = rummun paino (g)
            //f = filleri (g)
            return M1 - (M2 - R + f);
        }
    }
}
