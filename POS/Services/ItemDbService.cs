using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using POS.Models;
using System.Data.SQLite;

namespace POS.Services
{
    public class ItemDbService
    {
        public SQLiteConnection connection { get; set; }
        public SQLiteCommand command { get; set; }
        public SQLiteDataReader reader { get; set; }
        public string query { get; set; }
        public string cs { get; set; }

        public ItemDbService()
        {
            cs = @"data source = Store.db";
            connection = new SQLiteConnection(cs);

        }

        public ObservableCollection<Item> GetItems()
        {
            ObservableCollection<Item> items = new ObservableCollection<Item>();
            query = "select * from Item";
            connection.Open();
            command = new SQLiteCommand(query, connection);
            reader = command.ExecuteReader();
            
            while (reader.Read())
            {
                items.Add(new Item { Id = reader.GetInt32(0), Description = reader.GetString(1), Price = reader.GetFloat(2), Unit=reader.GetString(3), Qts=reader.GetInt32(4),Marque=reader.GetString(5), Category= reader.GetString(6)});
            }
            connection.Close();
            return items;


        }

        public void Refresh(ObservableCollection<Item> items)
        {
            query = "select * from Item";
            connection.Open();
            command = new SQLiteCommand(query, connection);
            reader = command.ExecuteReader();
            items.Clear();
            while (reader.Read())
            {
                items.Add(new Item { Id = reader.GetInt32(0), Description = reader.GetString(1), Price = reader.GetFloat(2), Unit = reader.GetString(3), Qts = reader.GetInt32(4), Marque = reader.GetString(5), Category = reader.GetString(6) });
            }
            connection.Close();
        }

        public Item GetItem(Item item)
        {
            query = $"select * from Item where description='{item.Description}'";
            connection.Open();
            command = new SQLiteCommand(query, connection);
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                item.Id = reader.GetInt32(0);
            }
            connection.Close();
            return item;

        }

        public void AddItem(Item item)
        {
            query = $"Insert into Item (description,price,unit,qts,marque,category) values ('{item.Description}',{item.Price},'{item.Unit}',{0},'{item.Marque}','{item.Category}')";
            command = new SQLiteCommand(query, connection);
            connection.Open();
            //command.Parameters.AddWithValue("@name", unit.Name);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void DelItem(Item item)
        {
            query = $"Delete from Item where id={item.Id}";
            command = new SQLiteCommand(query, connection);
            connection.Open();
            //command.Parameters.AddWithValue("@name", unit.Name);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void EditItem(Item item)
        {
            query = $"Update Item set description='{item.Description}',price='{item.Price}',unit='{item.Unit}',marque='{item.Marque}',category='{item.Category}' where id={item.Id}";
            command = new SQLiteCommand(query, connection);
            connection.Open();
            //command.Parameters.AddWithValue("@name", unit.Name);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void SearchItem(string text, ObservableCollection<Item> items)
        {
            query = $"select * from Item where description Like '%{text}%' OR id Like '%{text}%' OR category Like '%{text}%'";
            connection.Open();
            command = new SQLiteCommand(query, connection);
            reader = command.ExecuteReader();
            items.Clear();
            while (reader.Read())
            {
                items.Add(new Item { Id = reader.GetInt32(0), Description = reader.GetString(1), Price = reader.GetFloat(2), Unit = reader.GetString(3), Qts = reader.GetInt32(4), Marque = reader.GetString(5), Category = reader.GetString(6) });
            }
            connection.Close();
        }

    }
}
