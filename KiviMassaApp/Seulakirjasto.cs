using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiviMassaApp
{
    public class Seulakirjasto : IComparable<Seulakirjasto>//Käytetään seulalistan luomiseen
    {
        public double seula { get; set; }

        public int CompareTo(Seulakirjasto s)
        {
            //Käytetään listan järjestämiseen
            if (s == null)
                return 1;

            else
                return s.seula.CompareTo(this.seula);
        }
    }
    public class SeulakirjastoIndex : IComparable<SeulakirjastoIndex> //Käytetään jos tarvitaan seulojen järjestys tietää
    {
        public int index { get; set; }
        public double seula { get; set; }
        

        public int CompareTo(SeulakirjastoIndex s)
        {
            //Käytetään listan järjestämiseen
            if (s == null)
                return 1;

            else
                return s.seula.CompareTo(this.seula);
        }
    }
    public class SeulaLapPros
    {
        public int index { get; set; }
        public double seulaArvo { get; set; }
        public double tulos { get; set; }
        
    }
}
