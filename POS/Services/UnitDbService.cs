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
    public class UnitDbService
    {
        public SQLiteConnection connection { get; set; }
        public SQLiteCommand command { get; set; }
        public SQLiteDataReader reader { get; set; }
        public string query { get; set; }
        public string cs { get; set; }
        public UnitDbService()
        {
            cs = @"data source = Store.db";
            connection = new SQLiteConnection(cs);

        }

        public ObservableCollection<Models.Unit> GetUnits()
        {
            ObservableCollection<Models.Unit> units = new ObservableCollection<Models.Unit>();
            query = "select * from Unit";
            connection.Open();
            command = new SQLiteCommand(query,connection);
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                units.Add(new Models.Unit { Id=reader.GetInt32(0), Name=reader.GetString(1)});
            }
            connection.Close();
            return units;


        }

        public void Refresh(ObservableCollection<Unit> units)
        {
            query = "select * from Unit";
            connection.Open();
            command = new SQLiteCommand(query, connection);
            reader = command.ExecuteReader();
            units.Clear();
            while (reader.Read())
            {
                units.Add(new Models.Unit { Id = reader.GetInt32(0), Name = reader.GetString(1) });
            }
            connection.Close();
        }

        public Unit GetUnit(Unit unit)
        {
            query = $"select * from Unit where name='{unit.Name}'";
            connection.Open();
            command = new SQLiteCommand(query, connection);
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                unit.Id = reader.GetInt32(0);
            }
            connection.Close();
            return unit;

        }

        public void AddUnit(Unit unit)
        {
            query = $"Insert into Unit (name) values ('{unit.Name}')";
            command = new SQLiteCommand(query, connection);
            connection.Open();
            //command.Parameters.AddWithValue("@name", unit.Name);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void DelUnit(Unit unit)
        {
            query = $"Delete from Unit where id={unit.Id}";
            command = new SQLiteCommand(query, connection);
            connection.Open();
            //command.Parameters.AddWithValue("@name", unit.Name);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void EditUnit(Unit unit)
        {
            query = $"Update Unit set name='{unit.Name}' where id={unit.Id}";
            command = new SQLiteCommand(query, connection);
            connection.Open();
            //command.Parameters.AddWithValue("@name", unit.Name);
            command.ExecuteNonQuery();
            connection.Close();
        }


    }
}
