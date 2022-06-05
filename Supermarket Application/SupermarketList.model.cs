using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket_Application
{
    public class SupermarketList: INotifyPropertyChanged
    {
        private string ProductName;
        private int AmountProduct;
        private int TotalPrice;
        
        public SupermarketList()
        {

        }

        public SupermarketList(string ProductName,int AmountProduct,int TotalPrice)
        {
            this.ProductName = ProductName;
            this.AmountProduct = AmountProduct;
            this.TotalPrice = TotalPrice;
        }


        public string productName
        {
            get { return this.ProductName; }
            set { this.ProductName = value; OnPropertyChanged("ProductName"); }
        }

        public int amountProduct
        {
            get { return this.AmountProduct; }
            set { this.AmountProduct = value; OnPropertyChanged("AmountProduct"); }
        }

        public int totalPrice
        {
            get { return this.TotalPrice; }
            set { this.TotalPrice = value; OnPropertyChanged("TotalPrice"); }
        }

        private void OnPropertyChanged(string property)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
        
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
