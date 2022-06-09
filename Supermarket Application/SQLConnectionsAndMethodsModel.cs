using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket_Application
{
    public  interface SQLConnectionsAndMethodsModel
    {
         ObservableCollection<ProductSupermarket> userProductSupermarket {  get; set; } 
        public void InsertRecord(string ProductName, int AmountProduct, int TotalPrice);
        public void UpdateRecord(string ProductName, int AmountProduct, int TotalPrice, string OldProductName);
        public void DeleteRecord(string OldProductName);
        public ObservableCollection<ProductSupermarket> GetAllRecords();
        public DbConnection GetConnection();

    }
}
