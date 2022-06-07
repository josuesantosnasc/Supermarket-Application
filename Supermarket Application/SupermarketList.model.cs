using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace Supermarket_Application
{
    public  class SupermarketList : ICloneable,INotifyPropertyChanged
    {
        private string ProductName;
        private int AmountProduct;
        private int TotalPrice;
        private object v1;
        private object v2;
        private object v3;

        public SupermarketList()
        {

        }

        public SupermarketList(string ProductName,int AmountProduct,int TotalPrice)
        {
            this.ProductName = ProductName;
            this.AmountProduct = AmountProduct;
            this.TotalPrice = TotalPrice;
        }

        public SupermarketList(object v1, object v2, object v3)
        {
            this.v1 = v1;
            this.v2 = v2;
            this.v3 = v3;
        }

        public string productName
        {
            get { return this.ProductName; }
            set { this.ProductName = value; }// OnPropertyChanged("ProductName"); }
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

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }



        public object Clone()
        {
            return (SupermarketList)this.MemberwiseClone();
        }

        /*PostGre Connection and Methods*/
    /*
        public static void InsertRecord(string ProductName,int AmountProduct,int TotalPrice)
        {
            using (NpgsqlConnection con = GetConnection())
            {
                string query = $@"insert into public.usersupermarketlist(productname,amountproduct,totalprice)values({ProductName},{AmountProduct},{TotalPrice})";
                NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                con.Open();

                int n = cmd.ExecuteNonQuery();
                if (n == 1)
                {
                    Console.WriteLine("Record inserted");
                }

            }
        }

        private static void TestConnection()
        {
            using (NpgsqlConnection con = GetConnection())
            {
                con.Open();
                if (con.State == System.Data.ConnectionState.Open)
                {
                    Console.WriteLine("Connected");
                }
            }
        }

        private static NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection(@"Server=localhost;Port=5432;User Id=postgres;Password=santos17;Database=postgres");
        }*/
    }
}
