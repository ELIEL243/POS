using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POS.Models;
using System.Data.SQLite;
using System.Collections.ObjectModel;

namespace POS.Services
{
    public class CategoryDbService
    {
        public SQLiteConnection connection { get; set; }
        public SQLiteCommand command { get; set; }
        public SQLiteDataReader reader { get; set; }
        public string query { get; set; }
        public string cs { get; set; }

        public CategoryDbService()
        {
            cs = @"data source = Store.db";
            connection = new SQLiteConnection(cs);
        }

        public ObservableCollection<Category> GetCategories()
        {
            ObservableCollection<Category> categories = new ObservableCollection<Category>();
            query = "select * from Category";
            connection.Open();
            command = new SQLiteCommand(query, connection);
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                categories.Add(new Category { Id = reader.GetInt32(0), Name = reader.GetString(1) });
            }
            connection.Close();
            return categories;


        }

        public void Refresh(ObservableCollection<Category> categories)
        {
            query = "select * from Category";
            connection.Open();
            command = new SQLiteCommand(query, connection);
            reader = command.ExecuteReader();
            categories.Clear();
            while (reader.Read())
            {
                categories.Add(new Category { Id = reader.GetInt32(0), Name = reader.GetString(1) });
            }
            connection.Close();
        }

        public Category GetCategory(Category category)
        {
            query = $"select * from Category where name='{category.Name}'";
            connection.Open();
            command = new SQLiteCommand(query, connection);
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                category.Id = reader.GetInt32(0);
            }
            connection.Close();
            return category;

        }

        public void AddCategory(Category category)
        {
            query = $"Insert into Category (name) values ('{category.Name}')";
            command = new SQLiteCommand(query, connection);
            connection.Open();
            //command.Parameters.AddWithValue("@name", unit.Name);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void DelCategory(Category category)
        {
            query = $"Delete from Category where id={category.Id}";
            command = new SQLiteCommand(query, connection);
            connection.Open();
            //command.Parameters.AddWithValue("@name", unit.Name);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void EditCategory(Category category)
        {
            query = $"Update Category set name='{category.Name}' where id={category.Id}";
            command = new SQLiteCommand(query, connection);
            connection.Open();
            //command.Parameters.AddWithValue("@name", unit.Name);
            command.ExecuteNonQuery();
            connection.Close();
        }

    }
}
