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
    /// Logique d'interaction pour QtsPrice.xaml
    /// </summary>
    public partial class QtsPrice : Window
    {
        public QtsPrice()
        {
            InitializeComponent();
            //NameTextBox.Focus();
        }

        private void price_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Grid_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.RightShift)
            {
                this.Close();
            }
        }
    }
}
