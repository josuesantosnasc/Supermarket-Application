using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Npgsql;

namespace Supermarket_Application
{
    public class SupermarketAppVM: INotifyPropertyChanged
    {
        
       

        private SQLConnectionsAndMethodsModel sqlDataBase;

        private int totalSupermarketPurchase;

        public ObservableCollection<ProductSupermarket> userProductSupermarket { get; set; }

        public ProductSupermarket selectedProduct { get; set; }

      
        public ICommand Add { get; private set; }

        public ICommand Edit { get; private set; }

        public ICommand Remove { get; private set; }

        public event PropertyChangedEventHandler? PropertyChanged;
        public SupermarketAppVM()
        {
            this.userProductSupermarket = new ObservableCollection<ProductSupermarket>();
             
            sqlDataBase = new MySQLConnection();

            this.userProductSupermarket = sqlDataBase.GetAllRecords();

            this.totalSupermarketPurchase = userProductSupermarket.Sum(x => x.TotalPrice);

            StartUpCommands();

        }

        

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }


        public void StartUpCommands()
        {
            Add = new RelayCommand((object _) =>
            {
                ProductSupermarket userProductSupermarketDataWindow = new ProductSupermarket();


                ProductSupermarketInformation userAddNewProductWindow = new ProductSupermarketInformation();

               userAddNewProductWindow.DataContext = userProductSupermarketDataWindow;

               userAddNewProductWindow.ShowDialog();

                bool IsResultTrue = (bool)userAddNewProductWindow.DialogResult;



                if (IsResultTrue)
                {

                    try
                    {
                        sqlDataBase.InsertRecord(userProductSupermarketDataWindow.ProductName, userProductSupermarketDataWindow.AmountProduct, userProductSupermarketDataWindow.TotalPrice);

                        this.userProductSupermarket.Add(userProductSupermarketDataWindow);

                        this.TotalSupermarketPurchase = userProductSupermarket.Sum(x => x.TotalPrice);
                        
                    }
                    catch(Exception ex)
                    {

                        throw new Exception("Error in connection to database", ex);
                    }
              



                }



            });

            Edit = new RelayCommand((object _) =>
            {
                if (selectedProduct != null)
                {
                    ProductSupermarket copyuserProductSupermarketDataWindow = (ProductSupermarket)selectedProduct.Clone();

                    string oldProductName = copyuserProductSupermarketDataWindow.ProductName;

                    ProductSupermarketInformation userEditProductWindow = new ProductSupermarketInformation();

                    userEditProductWindow.DataContext = copyuserProductSupermarketDataWindow;

                    userEditProductWindow.ShowDialog();

                    bool IsResultTrue = (bool)userEditProductWindow.DialogResult;

                    if (IsResultTrue)
                    {

                        try
                        {
                            sqlDataBase.UpdateRecord(copyuserProductSupermarketDataWindow.ProductName, copyuserProductSupermarketDataWindow.AmountProduct, copyuserProductSupermarketDataWindow.TotalPrice, oldProductName);

                            userProductSupermarket[userProductSupermarket.IndexOf(selectedProduct)] = copyuserProductSupermarketDataWindow;

                            this.TotalSupermarketPurchase = userProductSupermarket.Sum(x => x.TotalPrice);

                            OnPropertyChanged("totalSupermarketPurchase");

                        }
                        catch(Exception ex)
                        {
                            throw new Exception("Error in connection to database", ex);
                        }

                       


                    }


                }
                else
                {
                    MessageBox.Show("Select at least One Product !! ");
                }

            }, (object _) => selectedProduct != null);


            Remove = new RelayCommand((_object) =>
            {
                if (selectedProduct != null)
                {
                    var result = MessageBox.Show("Are you sure you want to delete this item?", "Confirmation", MessageBoxButton.YesNo);


                    if (result == MessageBoxResult.Yes)
                    {
                        try
                        {
                            sqlDataBase.DeleteRecord(selectedProduct.ProductName);
                            userProductSupermarket.Remove(selectedProduct);
                            this.TotalSupermarketPurchase = userProductSupermarket.Sum(x => x.TotalPrice);
                            OnPropertyChanged("totalSupermarketPurchase");
                        }
                        catch(Exception ex)
                        {
                            throw new Exception("Error in connection to database", ex);
                        }
                       
                    }

                }
                else
                {
                    MessageBox.Show("Select at least One Product !! ");
                }
            }, (object _) => selectedProduct != null);
        }

      
   
        
        public int TotalSupermarketPurchase
        {
            get { return this.totalSupermarketPurchase; }
            set
            {
                this.totalSupermarketPurchase = value;
                OnPropertyChanged("TotalSupermarketPurchase");
            }
        }

       

       



      



    }
}