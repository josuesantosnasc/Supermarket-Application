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
        private NpgsqlConnection connection;

        public int indexSelectedProduct { get; set; }
        public int TotalSupermarketPurchase { get; set; }

        public SQLConnection SQLDatabase { get; set; }  

      

        public ObservableCollection<SupermarketList> UserSupermarketList { get; set; }

        public SupermarketList SelectedProduct { get; set; }

      
        public ICommand Add { get; private set; }

        public ICommand Edit { get; private set; }

        public ICommand Remove { get; private set; }
        public SupermarketAppVM()
        {
            UserSupermarketList = new ObservableCollection<SupermarketList>()
            {
               
            };
            SQLDatabase = new SQLConnection();

            this.UserSupermarketList = SQLDatabase.GetAllRecords();

            this.TotalSupermarketPurchase = UserSupermarketList.Sum(x => x.totalPrice);


            UserSupermarketList.CollectionChanged += CollectionChanged;

            


           

            AddNewProduct();

            EditProduct();

            RemoveProduct();

        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void ResetUserSupermarketList()
        {
            this.UserSupermarketList.Clear();
            this.UserSupermarketList = SQLDatabase.GetAllRecords();
            OnPropertyChanged("TotalSupermarketPurchase");
        }

        public void AddNewProduct()
        {
            Add = new RelayCommand((object _) =>
            {
                SupermarketList UserSupermarketListDataWindow = new SupermarketList();


                SupermarketListInformation UserAddNewProductWindow = new SupermarketListInformation();

                UserAddNewProductWindow.DataContext = UserSupermarketListDataWindow;

                UserAddNewProductWindow.ShowDialog();

                bool IsResultTrue = (bool)UserAddNewProductWindow.DialogResult;



                if (IsResultTrue)
                {
                    UserSupermarketList.Add(UserSupermarketListDataWindow);

                    if (UserSupermarketList[UserSupermarketList.IndexOf(UserSupermarketListDataWindow)]!=null)
                    {
                        SupermarketList UserSupermarketListAddDataBase = UserSupermarketList[UserSupermarketList.IndexOf(UserSupermarketListDataWindow)];

                        SQLDatabase.InsertRecord(UserSupermarketListAddDataBase.productName, UserSupermarketListAddDataBase.amountProduct, UserSupermarketListAddDataBase.totalPrice);

                        ResetUserSupermarketList();
                    }

                    



                }

                

                
            });
        }

        public void EditProduct()
        {
            Edit = new RelayCommand((object _) =>
            {
                if (SelectedProduct!=null)
                {
                    SupermarketList copyUserSupermarketListDataWindow = (SupermarketList)SelectedProduct.Clone();

                    string OldProductName = copyUserSupermarketListDataWindow.productName;

                    SupermarketListInformation UserEditProductWindow = new SupermarketListInformation();

                    UserEditProductWindow.DataContext = copyUserSupermarketListDataWindow;

                    UserEditProductWindow.ShowDialog();

                    bool IsResultTrue = (bool)UserEditProductWindow.DialogResult;

                    if (IsResultTrue)
                    {
                      
                       


                        SQLDatabase.UpdateRecord(copyUserSupermarketListDataWindow.productName, copyUserSupermarketListDataWindow.amountProduct, copyUserSupermarketListDataWindow.totalPrice,OldProductName);

                        ResetUserSupermarketList();


                    }



                }
                else
                {
                    MessageBox.Show("Select at least One Product !! ");
                }

            }, (object _) => SelectedProduct != null );
        }

        public void RemoveProduct()
        {
            Remove = new RelayCommand((_object) =>
            {
                if (SelectedProduct != null)
                {
                    var result = MessageBox.Show("Are you sure you want to delete this item?", "Confirmation", MessageBoxButton.YesNo);


                    if (result == MessageBoxResult.Yes)
                    {
                        SQLDatabase.DeleteRecord(SelectedProduct.productName);
                        ResetUserSupermarketList();
                    }
                  
                }
                else
                {
                    MessageBox.Show("Select at least One Product !! ");
                }
            }, (object _) => SelectedProduct != null);
        }

        
        public int totalSupermarketPurchase
        {
            get { return TotalSupermarketPurchase; }
            set
            {
                TotalSupermarketPurchase = value;
                OnPropertyChanged("TotalSupermarketPurchase");
            }
        }

       



        public void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.TotalSupermarketPurchase = UserSupermarketList.Sum(x => x.totalPrice);
            OnPropertyChanged("TotalSupermarketPurchase");
        }

       



      



    }
}