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
using POS.Views;
using POS.ViewModel;

namespace POS
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UCStock uCStock = new UCStock();
            uCStock.DataContext = new PurchaseViewModel();
            container.Content = uCStock;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            UCUnit uCUnit = new UCUnit();
            uCUnit.DataContext = new UnitViewModel();
            container.Content = uCUnit;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            UCCategory uCCategory = new UCCategory();
            uCCategory.DataContext = new CategoryViewModel();
            container.Content = uCCategory;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            UCItem uCItem = new UCItem();
            uCItem.DataContext = new ItemViewModel();
            container.Content = uCItem;
        }
    }
}
