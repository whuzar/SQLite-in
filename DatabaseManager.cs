using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace SQLite
{
    class DatabaseManager
    {
        private SqliteConnection connection;
        
        public DatabaseManager()
        {
            var path = @"Data Source=database.db";

            connection = new SqliteConnection(path);
            connection.Open();

            createTable();
        }
        private void createTable()
        {
            var command = new SqliteCommand("CREATE TABLE IF NOT EXISTS people (name STRING, surname STRING, age INT)",
                connection);
            command.ExecuteScalar();  
        }
        public void AddPerson(Person person)
        {
            var command = new SqliteCommand("INSERT INTO people (name, surname, age) VALUES (@name, @surname, @age)", connection);

            command.Parameters.AddWithValue("name", person.Name);
            command.Parameters.AddWithValue("surname", person.Surname);
            command.Parameters.AddWithValue("age", person.Age);

            command.ExecuteScalar();
        }
    }
}
