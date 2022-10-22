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
using System.Windows.Shapes;

namespace POS.Views.DataEntry
{
    /// <summary>
    /// Logique d'interaction pour CategoryDataEntry.xaml
    /// </summary>
    public partial class CategoryDataEntry : Window
    {
        public bool validate { get; set; }
        public CategoryDataEntry()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            validate = true;
            this.Close();
        }
    }
}
