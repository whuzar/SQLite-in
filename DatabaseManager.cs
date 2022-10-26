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
            var command = new SqliteCommand("CREATE TABLE IF NOT EXISTS people (id int AUTO_INCREMENT NOT NULL, name STRING, surname STRING, age INT, PRIMARY KEY (id));",
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
        public List<Person> GetPeople()
        {
            var people = new List<Person>();

            var command = new SqliteCommand("SELECT name, surname, age FROM people", connection);

            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                people.Add(new Person
                {
                    Name = reader.GetString(0),
                    Surname = reader.GetString(1),
                    Age = reader.GetInt32(2)
                });
            }
            return people;
        }
        public void DeletePerson()
        {
            var command = new SqliteCommand("DELETE FROM people WHERE name = (SELECT name FROM people ORDER BY name DESC LIMIT 1)", connection);
            command.ExecuteScalar();
        }
    }
}
