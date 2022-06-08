using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace Supermarket_Application
{
    public  class ProductSupermarket : ICloneable,INotifyPropertyChanged
    {
        private string productName;
        private int amountProduct;
        private int totalPrice;

        public event PropertyChangedEventHandler? PropertyChanged;
        public ProductSupermarket()
        {

        }

        public ProductSupermarket(string ProductName,int AmountProduct,int TotalPrice)
        {
            this.productName = ProductName;
            this.amountProduct = AmountProduct;
            this.totalPrice = TotalPrice;
        }

      

        public string ProductName
        {
            get { return this.productName; }
            set { this.productName = value; OnPropertyChanged("ProductName"); }
        }

        public int AmountProduct
        {
            get { return this.amountProduct; }
            set { this.amountProduct = value; OnPropertyChanged("AmountProduct"); }
        }

        public int TotalPrice
        {
            get { return this.totalPrice; }
            set { this.totalPrice = value; OnPropertyChanged("TotalPrice"); }
        }

       

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }



        public object Clone()
        {
            return (ProductSupermarket)this.MemberwiseClone();
        }

    
    }
}
