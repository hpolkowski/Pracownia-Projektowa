using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PProj
{
    /// <summary>
    /// Interaction logic for Nowy_test.xaml
    /// </summary>
    public partial class Nowy_test : Window
    {
        public Nowy_test()
        {
            InitializeComponent();
        }

        private void button_Anuluj_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void button_OK_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
