using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Npgsql;

namespace Supermarket_Application
{
    public class SQLConnection

    {
        public ObservableCollection<SupermarketList> UserSupermarketList;
       public SQLConnection()
        {
            this.UserSupermarketList = new ObservableCollection<SupermarketList>();
        }


        

        public void InsertRecord(string ProductName, int AmountProduct, int TotalPrice)
        {
            using (NpgsqlConnection con = GetConnection())
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
                    }
                    cmd.Dispose();
                    con.Close();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
               

            }
        }

        public void UpdateRecord(string ProductName, int AmountProduct, int TotalPrice, string OldProductName)

        {

            using (NpgsqlConnection con = GetConnection())
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
                    }
                    cmd.Dispose();
                    con.Close();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
               

            }
        }

        public void DeleteRecord(string OldProductName)

        {

            using (NpgsqlConnection con = GetConnection())
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
                    }
                    cmd.Dispose();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
               
            }
        }


        public ObservableCollection<SupermarketList> GetAllRecords()

        {

            using (NpgsqlConnection con = GetConnection())
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
                            SupermarketList UserSupermarketListFromDataBase = new SupermarketList(Convert.ToString(reader[1]), Convert.ToInt32(reader[2]), Convert.ToInt32(reader[3]));
                            this.UserSupermarketList.Add(UserSupermarketListFromDataBase);

                        }
                    }

                    return this.UserSupermarketList;

                    cmd.Dispose();
                    con.Close();

                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                    return this.UserSupermarketList;

                }

            }
        }


        private static NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection(@"Server=localhost;Port=5430;User Id=root;Password=root;Database=test_db");

        }
    }
}
