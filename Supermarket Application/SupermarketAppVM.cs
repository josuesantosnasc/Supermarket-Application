using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Supermarket_Application
{
    public class SupermarketAppVM: INotifyPropertyChanged
    {
        public int TotalSupermarketPurchase { get; set; }

        private int IndexSelectedProduct { get; set; }


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

            UserSupermarketList.CollectionChanged += CollectionChanged;

            this.TotalSupermarketPurchase = 0;

           

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

                    OnPropertyChanged("TotalSupermarketPurchase");



                }

                

                
            });
        }

        public void EditProduct()
        {
            Edit = new RelayCommand((object _) =>
            {
                if (SelectedProduct!=null)
                {
                    SupermarketList UserSupermarketListEditDataWindow = new SupermarketList(SelectedProduct.productName,SelectedProduct.amountProduct,SelectedProduct.totalPrice);

                    SupermarketListInformation UserEditProductWindow = new SupermarketListInformation();

                    UserEditProductWindow.DataContext = UserSupermarketListEditDataWindow;

                    UserEditProductWindow.ShowDialog();

                    bool IsResultTrue = (bool)UserEditProductWindow.DialogResult;

                    if (IsResultTrue)
                    {
                        UserSupermarketList[IndexSelectedProduct] = UserSupermarketListEditDataWindow;
                        OnPropertyChanged("TotalSupermarketPurchase");

                    }



                }
                else
                {
                    MessageBox.Show("Select at least One Product !! ");
                }
                
            });
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
                        UserSupermarketList.Remove(SelectedProduct);
                        OnPropertyChanged("TotalSupermarketPurchase");
                    }
                  
                }
                else
                {
                    MessageBox.Show("Select at least One Product !! ");
                }
            });
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
        }


    }
}