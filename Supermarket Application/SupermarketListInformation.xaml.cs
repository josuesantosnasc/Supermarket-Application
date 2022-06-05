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

namespace Supermarket_Application
{
    
    public partial class SupermarketListInformation : Window
    {
        public SupermarketListInformation()
        {
            InitializeComponent();
        }

        public void AddNewProduct(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        public void EditProduct(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

    }
}
