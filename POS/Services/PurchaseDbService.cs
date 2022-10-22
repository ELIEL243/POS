using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using POS.Models;
using System.Data.SQLite;
using POS.Services;

namespace POS.Services
{
    public class PurchaseDbService
    {
        public SQLiteConnection connection { get; set; }
        public SQLiteCommand command { get; set; }
        public SQLiteCommand command2 { get; set; }
        public SQLiteDataReader reader { get; set; }
        public string query { get; set; }
        public string cs { get; set; }

        public PurchaseDbService()
        {
            cs = @"data source = Store.db";
            connection = new SQLiteConnection(cs);
        }

        public ObservableCollection<Purchase> GetPurchases()
        {
            ObservableCollection<Purchase> purchases = new ObservableCollection<Purchase>();
            query = "select * from Purchase where completed=1";
            connection.Open();
            command = new SQLiteCommand(query, connection);
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                 purchases.Add(new Purchase { Id = reader.GetInt32(0), Item = reader.GetString(1), Qts = reader.GetInt32(2), Date = DateTime.Parse(reader.GetString(3)).ToShortDateString(), Purchase_price = reader.GetFloat(4), Total = reader.GetFloat(5) });
            }
            connection.Close();
            return purchases;
            

        }

        public ObservableCollection<Purchase> GetUnCompletedPurchases()
        {
            ObservableCollection<Purchase> purchases = new ObservableCollection<Purchase>();
            query = "select * from Purchase where completed=0";
            connection.Open();
            command = new SQLiteCommand(query, connection);
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                purchases.Add(new Purchase { Id = reader.GetInt32(0), Item = reader.GetString(1), Qts = reader.GetInt32(2), Date = DateTime.Parse(reader.GetString(3)).ToShortDateString(), Purchase_price = reader.GetFloat(4), Total = reader.GetFloat(5) });
            }
            connection.Close();
            return purchases;


        }
        public void Refresh(ObservableCollection<Purchase> purchases)
        {
            query = "select * from Purchase where completed=1";
            connection.Open();
            command = new SQLiteCommand(query, connection);
            reader = command.ExecuteReader();
            purchases.Clear();
            while (reader.Read())
            {
                purchases.Add(new Purchase { Id = reader.GetInt32(0), Item = reader.GetString(1), Qts = reader.GetInt32(2), Date = DateTime.Parse(reader.GetString(3)).ToShortDateString(), Purchase_price = reader.GetFloat(4), Total = reader.GetFloat(5) });

            }
            connection.Close();
        }

        public void RefreshUnCompleted(ObservableCollection<Purchase> purchases)
        {
            query = "select * from Purchase where completed=0";
            connection.Open();
            command = new SQLiteCommand(query, connection);
            reader = command.ExecuteReader();
            purchases.Clear();
            while (reader.Read())
            {
                purchases.Add(new Purchase { Id = reader.GetInt32(0), Item = reader.GetString(1), Qts = reader.GetInt32(2), Date = DateTime.Parse(reader.GetString(3)).ToShortDateString(), Purchase_price = reader.GetFloat(4), Total = reader.GetFloat(5) });

            }
            connection.Close();
        }

        public void AddPurchase(Purchase purchase)
        { 
            query = $"Insert into Purchase (item,qts,date,purchase_price,total,completed) values ('{purchase.Item}',{purchase.Qts},'{purchase.Date}',{purchase.Purchase_price},{purchase.Total}, 0)";
            command = new SQLiteCommand(query, connection);
            connection.Open();
            //command.Parameters.AddWithValue("@name", unit.Name);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void DelPurchase(Purchase purchase)
        {
            query = $"Delete from Purchase where id={purchase.Id}";
            command = new SQLiteCommand(query, connection);
            connection.Open();
            //command.Parameters.AddWithValue("@name", unit.Name);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void EditPurchase(Purchase purchase)
        {
            query = $"Update Purchase set qts={purchase.Qts},purchase_price={purchase.Purchase_price} where id={purchase.Id}";
            command = new SQLiteCommand(query, connection);
            connection.Open();
            //command.Parameters.AddWithValue("@name", unit.Name);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void CompletePurchase(ObservableCollection<Purchase> purchases)
        {
            int id=0;
            ItemDbService itemDbService = new ItemDbService();
            foreach (Purchase p in purchases)

            {
                Console.WriteLine(p.Item);
                
                query = $"Update Purchase set completed=1 where id={p.Id}";
                command = new SQLiteCommand(query, connection);
                connection.Open();
                //command.Parameters.AddWithValue("@name", unit.Name);
                command.ExecuteNonQuery();
                connection.Close();
                connection.Open();
                foreach (Item i in itemDbService.GetItems())
                {
                    if (i.Description == p.Item)
                    {
                        id = i.Id;
                    }
                }
                string query2 = $"Update Item set qts=qts+{p.Qts} where id='{id}'";
                command2 = new SQLiteCommand(query2, connection);
                command2.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void SearchPurchases(ObservableCollection<Purchase> purchases, string date)
        {
            query = $"select * from Purchase where completed=1 and date='{date}'";
            connection.Open();
            command = new SQLiteCommand(query, connection);
            reader = command.ExecuteReader();
            purchases.Clear();
            while (reader.Read())
            {
                Console.WriteLine($"{date} = {reader.GetString(3)}");
                purchases.Add(new Purchase { Id = reader.GetInt32(0), Item = reader.GetString(1), Qts = reader.GetInt32(2), Date = DateTime.Parse(reader.GetString(3)).ToShortDateString(), Purchase_price = reader.GetFloat(4), Total = reader.GetFloat(5) });
            }
            connection.Close();
            

        }

        public void SearchPurchasesBetween(ObservableCollection<Purchase> purchases, string date, string date2)
        {
            query = $"select * from Purchase where completed=1 and strftime('%Y-%m-%d', date) between '{date}' and '{date2}'";
            connection.Open();
            command = new SQLiteCommand(query, connection);
            reader = command.ExecuteReader();
            purchases.Clear();
            while (reader.Read())
            {
                //Console.WriteLine($"{date} = {reader.GetString(3)}");
                purchases.Add(new Purchase { Id = reader.GetInt32(0), Item = reader.GetString(1), Qts = reader.GetInt32(2), Date = DateTime.Parse(reader.GetString(3)).ToShortDateString(), Purchase_price = reader.GetFloat(4), Total = reader.GetFloat(5) });
            }
            connection.Close();


        }

    }
}
