using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

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
    }

    public class Wady : ObservableCollection<Wada>
    {
    }
}
