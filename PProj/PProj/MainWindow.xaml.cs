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


        //to będzie zmienione, dodamy pliki xml z serwera, stworzone do testów aplikacji
        public void uzupelnij()
        {
            double[][] prawdopodobienstwa = new double[100][];
            int k = 0;
            XDocument xml = XDocument.Load("baza.xml");
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

        String pobierz_date()
        {
            String dataPliku = "2015-04-20";
            XDocument xml = XDocument.Load("baza.xml");
            XElement element_data = xml.Root;
            dataPliku = element_data.Attribute("data").Value.ToString();
            return dataPliku;
        }
        public MainWindow()
        {
            InitializeComponent();
            listBox.ItemsSource = lista;
            FileDownloader downloader;
            downloader = new FileDownloader("212.33.90.100", "wi90302", "KRtq97Cr");
// +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
//                            Change first param to date readed from local file
// +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++            
            if (downloader.shouldDownloadUpdate(pobierz_date(), "plody/baza.xml")) {
                //downloader.download("plody/baza.xml", "baza.xml");
                Debug.Print("--Pobieram nowy plik z danymi. Stary plik z dnia: ");
                Debug.Print(pobierz_date());
            }
            uzupelnij();
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
            if (wiek < 15 || wiek > 45)
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
