using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace PProj
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Wady lista = new Wady();
        List<Wada_dane> dane= new List<Wada_dane>();
        FileDownloader downloader;


        //to będzie zmienione, dodamy pliki xml z serwera, stworzone do testów aplikacji
        public void uzupelnij()
        {/*
            String[] wady = { "Zespół Downa", "Zespół Edwardsa", "Zespół Patau", "Nieprawidłowości chromosomów płciowych", "Zespół Turnera", "Zespół Klinefeltera" };
            double[][] prawdopodobienstwa_temp = new double[][] { 
            new double[] { 0.0654, 0.0654, 0.0654,  0.0654,  0.0654, 0.0740, 0.0740, 0.0740, 0.0740, 0.0740, 0.1100, 0.1256, 0.1464, 0.1742, 0.2110, 0.2604, 0.3257, 0.4132, 0.5291, 0.6849, 0.8929, 1.1765, 1.5385, 2.0408, 2.7027, 3.5714, -1, -1, -1, -1, -1 }, 
            new double[] { -1, -1, -1, -1, -1, 0.07, 0.07, 0.07, 0.07, 0.07, 0.1, 0.11, 0.12, 0.14, 0.17, 0.21, 0.26, 0.35, 0.43, 0.6, 0.8, 1.3, 1.401, -1, -1, -1, -1, -1, -1, -1, -1 }, 
            new double[] { 0.01, 0.01, 0.01, 0.01, 0.01, 0.02, 0.02, 0.02, 0.02, 0.02, 0.03, 0.035, 0.036, 0.04, 0.048, 0.052, 0.055, 0.12, 0.14, 0.18, 0.25, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 }, 
            new double[] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 }, 
            new double[] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 }, 
            new double[] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 }
            };
         */
            List<List<double>> oko;
            double[][] prawdopodobienstwa = new double[100][];
            XDocument xml = XDocument.Load("baza_temp.xml");
            int k = 0;
            foreach (XElement element in xml.Root.Elements("wada"))
            {
                int j = 0;
                double[] prawdopodobienstwa_temp = new double[32];
                foreach (XElement element2 in element.Element("prawdopodobienstwa").Elements("prawd"))
                {
                    prawdopodobienstwa_temp[j] = double.Parse(element2.Value.ToString(), System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture);
                    
                    j++;
                }
                prawdopodobienstwa[k] = prawdopodobienstwa_temp;
                dane.Add(
                    new Wada_dane(
                        element.Attribute("nazwa").Value.ToString(),
                        element.Element("opis").Value.ToString(),
                        prawdopodobienstwa[k].ToList()
                    )
                );
                k++;
            }
            /* Zapisywanie pliku. Może się przydać.
            XDocument xml2 = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XComment("Plik testowy"),
                new XElement("wady",
                from wada in dane
                select new XElement("wada",
                    new XAttribute("nazwa", wada.Nazwa_wady),
                    new XElement("opis", "opis wady"),
                    new XElement("prawdopodobienstwa", wada.Prawdopodobienstwa_wady.Select(x => new XElement("prawd", x)))
                    )
                )
            );
            xml2.Save("Osoby.xml");
            */
        }

        public MainWindow()
        {
            InitializeComponent();
            listBox.ItemsSource = lista;
            downloader = new FileDownloader("212.33.90.100", "wi90302", "KRtq97Cr");
            uzupelnij();
// +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
//                            Change first param to date readed from local file
// +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++            
            //if (downloader.shouldDownloadUpdate("2015-04-20", "plody/baza.xml"))
                //downloader.download("plody/baza.xml", "baza.xml");
        }

        private void button_Nowy_Click(object sender, RoutedEventArgs e)
        {
            Nowy_test okno = new Nowy_test();
            okno.ShowDialog();
        }

        private void button_Zamknij_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void textBox_wiek_TextChanged(object sender, TextChangedEventArgs e)
        {
            int wiek;
            List<Wada> lista_tmp = new List<Wada>();
            Wada wada_tmp;
            lista.Clear();
            if (!int.TryParse(textBox_wiek.Text, out wiek))
                return;
            if (wiek < 20 || wiek > 45)
                return;
            for (int i = 0; i < dane.Count; i++ )
                lista_tmp.Add(new Wada(dane[i], wiek));
            for (int j = 1; j < lista_tmp.Count; j++)
                for (int i = 1; i < lista_tmp.Count; i++)
                if((lista_tmp[i].Prawdopodobienstwo_wady) > (lista_tmp[i-1].Prawdopodobienstwo_wady))
                {
                    wada_tmp = lista_tmp[i];
                    lista_tmp[i] = lista_tmp[i - 1];
                    lista_tmp[i - 1] = wada_tmp;
                }
            for (int i = 0; i < lista_tmp.Count; i++)
                lista.Add(lista_tmp[i]);
        }
    }
}
