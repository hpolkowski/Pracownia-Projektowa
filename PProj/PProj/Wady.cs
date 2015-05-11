using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace PProj
{
    public class Wada
    {
        public String Nazwa_wady { get; set; }
        public double Prawdopodobienstwo_wady { get; set; }

        public Wada(String nazwa_wady, double prawdopodobienstwo_wady)
        {
            this.Nazwa_wady = nazwa_wady;
            this.Prawdopodobienstwo_wady = prawdopodobienstwo_wady;
        }
        public Wada(Wada_dane wada, int wiek)
        {
            this.Nazwa_wady = wada.Nazwa_wady;
            this.Prawdopodobienstwo_wady = wada.Prawdopodobienstwa_wady[wiek-20];
        }
    }

    public class Wada_dane
    {
        public String Nazwa_wady { get; set; }
        public String Opis_wady { get; set; }
        public List<double> Prawdopodobienstwa_wady { get; set; }

        public Wada_dane(String nazwa_wady, List<double> prawdopodobienstwa_wady)
        {
            this.Nazwa_wady = nazwa_wady;
            this.Prawdopodobienstwa_wady = prawdopodobienstwa_wady;
        }
    }

    public class Wady : ObservableCollection<Wada>
    {
    }
}
