using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Supermarket_Application
{
    public class MySQLConnection:SQLConnectionsAndMethodsModel

    {
        public ObservableCollection<ProductSupermarket> userProductSupermarket { get; set; }
        public MySQLConnection()
        { 
            this.userProductSupermarket = new ObservableCollection<ProductSupermarket>();

        }

        public void InsertRecord(string ProductName, int AmountProduct, int TotalPrice)
        {
            using (MySqlConnection con = (MySqlConnection)GetConnection())
            {
                string query = $@"insert into usersupermarketlist(productname,amountproduct,totalprice) values('{ProductName}',{AmountProduct},{TotalPrice})";
                MySqlCommand cmd = new MySqlCommand(query, con);

                try
                {
                    con.Open();

                    int n = cmd.ExecuteNonQuery();

                    if (n == 1)
                    {
                        MessageBox.Show("Record Inserted");
                    }
                    else
                    {
                        MessageBox.Show("Error in inserting data into database");
                        throw new InvalidOperationException("Error in deleting the data");
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    throw ex;
                }
                finally
                {
                    cmd.Dispose();
                    con.Close();
                }


            }
        }

        public void UpdateRecord(string ProductName, int AmountProduct, int TotalPrice, string OldProductName)

        {

            using (MySqlConnection con = (MySqlConnection)GetConnection())
            {
                string query = $@"update usersupermarketlist set productName='{ProductName}', amountproduct={AmountProduct}, totalprice={TotalPrice} where productname='{OldProductName}'";
                MySqlCommand cmd = new MySqlCommand(query, con);

                try
                {
                    con.Open();

                    int n = cmd.ExecuteNonQuery();

                    if (n == 1)
                    {
                        MessageBox.Show("Successfully updated");
                    }
                    else
                    {
                        MessageBox.Show("Error in updating the database");
                        throw new InvalidOperationException("Error in deleting the data");
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    throw ex;
                }
                finally
                {
                    cmd.Dispose();
                    con.Close();
                }


            }
        }

        public void DeleteRecord(string OldProductName)

        {

            using (MySqlConnection con = (MySqlConnection)GetConnection())
            {
                string query = $@"delete from usersupermarketlist  where productname='{OldProductName}'";
                MySqlCommand cmd = new MySqlCommand(query, con);

                try
                {
                    con.Open();

                    int n = cmd.ExecuteNonQuery();

                    if (n == 1)
                    {
                        MessageBox.Show("Successfully deleted");
                    }
                    else
                    {
                        MessageBox.Show("Error in deleting the data");
                        throw new InvalidOperationException("Error in deleting the data");
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    throw ex;
                }
                finally
                {
                    cmd.Dispose();
                    con.Close();
                }

            }
        }


        public ObservableCollection<ProductSupermarket> GetAllRecords()

        {

            using (MySqlConnection con = (MySqlConnection)GetConnection())
            {
                string query = @"select * from usersupermarketlist";
                MySqlCommand cmd = new MySqlCommand(query, con);
                try
                {
                    con.Open();

                    int n = cmd.ExecuteNonQuery();

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ProductSupermarket userProductSupermarketFromDataBase = new ProductSupermarket(Convert.ToString(reader[1]), Convert.ToInt32(reader[2]), Convert.ToInt32(reader[3]));
                            this.userProductSupermarket.Add(userProductSupermarketFromDataBase);

                        }
                    }

                    return this.userProductSupermarket;




                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                    throw ex;

                    return this.userProductSupermarket;

                }
                finally
                {
                    cmd.Dispose();
                    con.Close();
                }

            }
        }


        public DbConnection GetConnection()
        {


            return new MySqlConnection(@"Server=localhost;Port=3306;User Id=root;Password=root;Database=homedb");

        }
    }
}
