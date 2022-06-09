using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;
using Npgsql;

namespace Supermarket_Application
{
    public class PostGreConnection:SQLConnectionsAndMethodsModel

    {
        public ObservableCollection<ProductSupermarket> userProductSupermarket { get; set; }    
       public PostGreConnection()
        {
            this.userProductSupermarket = new ObservableCollection<ProductSupermarket>();
        }


        

        public void InsertRecord(string ProductName, int AmountProduct, int TotalPrice)
        {
            using (NpgsqlConnection con = (NpgsqlConnection)GetConnection())
            {
                string query = $@"insert into public.usersupermarketlist(productname,amountproduct,totalprice) values('{ProductName}',{AmountProduct},{TotalPrice})";
                NpgsqlCommand cmd = new NpgsqlCommand(query, con);

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
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    throw ex;
                }
                finally{
                    cmd.Dispose();
                    con.Close();
                }
               

            }
        }

        public void UpdateRecord(string ProductName, int AmountProduct, int TotalPrice, string OldProductName)

        {

            using (NpgsqlConnection con = (NpgsqlConnection)GetConnection())
            {
                string query = $@"update public.usersupermarketlist set productName='{ProductName}', amountproduct={AmountProduct}, totalprice={TotalPrice} where productname='{OldProductName}'";
                NpgsqlCommand cmd = new NpgsqlCommand(query, con);

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
                catch(Exception ex)
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

            using (NpgsqlConnection con = (NpgsqlConnection)GetConnection())
            {
                string query = $@"delete from public.usersupermarketlist  where productname='{OldProductName}'";
                NpgsqlCommand cmd = new NpgsqlCommand(query, con);

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
                catch(Exception ex)
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

            using (NpgsqlConnection con = (NpgsqlConnection)GetConnection())
            {
                string query = @"select * from public.usersupermarketlist";
                NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                try
                {
                    con.Open();

                    int n = cmd.ExecuteNonQuery();

                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
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

            
            return new NpgsqlConnection(@"Server=localhost;Port=5430;User Id=root;Password=root;Database=test_db");

        }
    }
}
