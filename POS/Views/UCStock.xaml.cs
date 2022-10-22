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
using POS.Models;
using System.Collections.ObjectModel;
using POS.Services;
using System.Text.RegularExpressions;

namespace POS.Views
{
    /// <summary>
    /// Logique d'interaction pour UCStock.xaml
    /// </summary>
    public partial class UCStock : UserControl
    {
        public UCStock()
        {
            InitializeComponent();
            
            //this.Loaded += UCStock_Loaded;

        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Views.DataEntry.QtsPrice qtsPrice = new DataEntry.QtsPrice();
            qtsPrice.ShowDialog();
        }

        private void date_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\\d\\d$");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
