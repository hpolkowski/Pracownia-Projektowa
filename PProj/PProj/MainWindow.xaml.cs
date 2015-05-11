using System;
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

namespace PProj
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Wady lista = new Wady();


        //to będzie zmienione, dodamy pliki xml z serwera, stworzone do testów aplikacji
        String[] wady = { "Zespół Downa", "Zespół Edwardsa", "Zespół Patau", "Nieprawidłowości chromosomów płciowych", "Zespół Turnera", "Zespół Klinefeltera" };
        double[][] prawdopodobienstwa = new double[][] { 
            new double[] { 0.0654, 0.0654, 0.0654,  0.0654,  0.0654, 0.0740, 0.0740, 0.0740, 0.0740, 0.0740, 0.1100, 0.1256, 0.1464, 0.1742, 0.2110, 0.2604, 0.3257, 0.4132, 0.5291, 0.6849, 0.8929, 1.1765, 1.5385, 2.0408, 2.7027, 3.5714, -1, -1, -1, -1, -1 }, 
            new double[] { -1, -1, -1, -1, -1, 0.07, 0.07, 0.07, 0.07, 0.07, 0.1, 0.11, 0.12, 0.14, 0.17, 0.21, 0.26, 0.35, 0.43, 0.6, 0.8, 1.3, 1.401, -1, -1, -1, -1, -1, -1, -1, -1 }, 
            new double[] { 0.01, 0.01, 0.01, 0.01, 0.01, 0.02, 0.02, 0.02, 0.02, 0.02, 0.03, 0.035, 0.036, 0.04, 0.048, 0.052, 0.055, 0.12, 0.14, 0.18, 0.25, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 }, 
            new double[] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 }, 
            new double[] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 }, 
            new double[] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 }
        };

        public MainWindow()
        {
            InitializeComponent();
            listBox.ItemsSource = lista;
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
            lista.Clear();
            if (!int.TryParse(textBox_wiek.Text, out wiek))
                return;
            if (wiek < 20 || wiek > 45)
                return;
            lista.Add(new Wada(wady[0], prawdopodobienstwa[0][wiek - 20]));
            lista.Add(new Wada(wady[1], prawdopodobienstwa[1][wiek - 20]));
            lista.Add(new Wada(wady[2], prawdopodobienstwa[2][wiek - 20]));
            lista.Add(new Wada(wady[3], prawdopodobienstwa[3][wiek - 20]));
            lista.Add(new Wada(wady[4], prawdopodobienstwa[4][wiek - 20]));
            lista.Add(new Wada(wady[5], prawdopodobienstwa[5][wiek - 20]));
        }
    }
}
