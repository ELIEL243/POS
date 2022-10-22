using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace POS.Views.DataEntry
{
    /// <summary>
    /// Logique d'interaction pour ItemDataEntry.xaml
    /// </summary>
    public partial class ItemDataEntry : Window
    {
        public bool validate { get; set; }
        public ItemDataEntry()
        {
            InitializeComponent();
        }

        private void price_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            validate = true;
            this.Close();
        }
    }
}
