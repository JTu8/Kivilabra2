using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KiviMassaApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private Window _kivi = null, _massa = null, _seula = null;

        private void OpenKivi(object sender, RoutedEventArgs e) //Avaa kiviohjelman
        {
            if (_kivi == null)
            {
                _kivi = new Kiviohjelma(this);
                _kivi.Show();
            }
            else
            {
                _kivi.Activate();
            }
        }

        private void OpenMassa(object sender, RoutedEventArgs e) //Avaa massaohjelman
        {
            if (_massa == null)
            {
                _massa = new Massaohjelma(this);
                _massa.Show();
            }
            else
            {
                _massa.Activate();
            }
           
        }

        private void OpenSeula(object sender, RoutedEventArgs e)
        {
            if(_seula == null)
            {
                _seula = new SeulaMuokkausIkkuna(this);
                _seula.Show();
            }
            else
            {
                _seula.Activate();
            }
        }

        public void SuljeIkkuna(string nimi)
        {
            switch (nimi) {
                case "kivi":
                    _kivi = null;
                    break;
                case "massa":
                    _massa = null;
                    break;
                case "seula":
                    _seula = null;
                    break;
                default:
                    break;
            }
        }
    }
}
