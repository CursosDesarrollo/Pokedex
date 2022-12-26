using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using Pokedex2.config;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Pokedex2.models
{
    public class Model
    {
        private readonly string table;
        private readonly string primaryKey;
        private string action;
        public string Comand { get; set; }

        public Model(string table, string primaryKey)
        {
            this.table = table;
            this.primaryKey = primaryKey;
            this.Comand = "";
            this.action = "";
        }

        public Model Select(string query = "*", string from = "")
        {
            from = from.Length > 0 ? from : this.table;
            this.action = "select";
            this.Comand += "SELECT "
                + query
                + " FROM "
                + from;
            return this;
        }

        public Model Where(string varCompare, string sign, string value, bool marks = true)
        {
            string quotes = marks ? Convert.ToString('"') : "";
            this.Comand += " WHERE "
                + varCompare
                + " "
                + sign
                + " "
                + quotes
                + value
                + quotes;
            return this;
        }

        public Model AndWhere(string varCompare, string sign, string value, bool marks = true)
        {
            string quotes = marks ? Convert.ToString('"') : "";
            this.Comand += " AND "
                + varCompare
                + " "
                + sign
                + " "
                + quotes
                + value
                + quotes;
            return this;
        }

        public Model AndLike(string column, string query)
        {
            this.Comand += " AND "
                + column
                + " LIKE "
                + "'%"
                + query
                + "%'";
            return this;
        }

        public Model Order(string query, string config)
        {
            this.Comand += " ORDER BY "
                + query
                + " "
                + config;
            return this;
        }

        public Model Insert(Dictionary<string, string> values)
        {
            this.action = "insert";
            Dictionary<string, string>.KeyCollection keys = values.Keys;
            string queryFirstPart = "INSERT INTO " + this.table + " (",
                querySecondPart = " VALUES (";
            int countData = 0;
            foreach (string key in keys)
            {
                countData++;
                string point = countData == keys.Count ? "" : ",";
                queryFirstPart += key + point;
                querySecondPart += '"' + values[key] + '"' + point;
            }
            this.Comand += queryFirstPart
                + ")"
                + querySecondPart
                + ")";
            return this;
        }

        public Model Update(Dictionary<string, string> values,bool marks = true)
        {
            this.action = "update";
            string quotes = marks ? Convert.ToString('"') : "";
            Dictionary<string, string>.KeyCollection keys = values.Keys;
            string query = "UPDATE " + this.table + " SET ";
            int countData = 0;
            foreach (string key in keys)
            {
                countData++;
                string point = countData == keys.Count ? "" : ",";
                query += key
                    + " = "
                    + quotes 
                    + values[key] 
                    + quotes 
                    + point;
            }
            this.Comand += query;
            return this;
        }

        public Model Delete()
        {
            this.action = "delete";
            this.Comand += "DELETE FROM "
                + this.table;
            return this;
        }

        private List<string> NameColumns(MySqlDataReader reader)
        {
            int columns = reader.FieldCount;
            List<string> nameColumns = new List<string>();
            for (int i = 0; i < columns; i++)
            {
                string name = reader.GetName(i).ToLower();
                nameColumns.Add(name);
            }
            return nameColumns;
        }

        private List<Dictionary<string, string>> ExecSelect(MySqlDataReader reader)
        {
            var connectDb = ConnectDataBase.GetObject().connectDb;
            if (!reader.HasRows)
            {
                connectDb.Close();
                return new List<Dictionary<string, string>>();
            }
            List<Dictionary<string, string>> data = new List<Dictionary<string, string>>();
            int columns = reader.FieldCount;
            List<string> nameColumns = this.NameColumns(reader);
            while (reader.Read())
            {
                Dictionary<string, string> newDictionary = new Dictionary<string, string>();
                for (int i = 0; i < columns; i++) newDictionary.Add(nameColumns[i], reader.GetString(i));
                data.Add(newDictionary);
            }
            connectDb.Close();
            return data;
        }

        private List<Dictionary<string, string>> ExecInsert(MySqlCommand newComand)
        {
            var connectDb = ConnectDataBase.GetObject().connectDb;
            newComand.ExecuteNonQuery();
            long id = newComand.LastInsertedId;
            connectDb.Close();
            return new List<Dictionary<string, string>>()
            {
                new Dictionary<string, string>()
                {
                    {this.primaryKey,Convert.ToString(id)}
                }
            };
        }

        private List<Dictionary<string, string>> ExecUpdateAndDelete(MySqlCommand newComand)
        {
            var connectDb = ConnectDataBase.GetObject().connectDb;
            newComand.ExecuteNonQuery();
            connectDb.Close();
            return new List<Dictionary<string, string>>();
        }

        private void RebootVariables()
        {
            this.Comand = "";
            this.action = "";
        }

        private List<Dictionary<string, string>> SelectOption(MySqlCommand newComand)
        {
            List<Dictionary<string, string>> data;
            switch (action)
            {
                case "select":
                    data = ExecSelect(newComand.ExecuteReader());
                    break;
                case "insert":
                    data = ExecInsert(newComand);
                    break;
                case "update":
                    data = ExecUpdateAndDelete(newComand);
                    break;
                case "delete":
                    data = ExecUpdateAndDelete(newComand);
                    break;
                default:
                    data = new List<Dictionary<string, string>>();
                    break;
            }
            return data;
        }

        public List<Dictionary<string, string>> Exec()
        {
            if (this.action.Length == 0
                || this.Comand.Length == 0) return new List<Dictionary<string, string>>();
            Console.WriteLine(this.Comand);
            Object result = ConnectDataBase.GetObject().ExecComand(this.Comand);
            if (result is string) throw new Exception((string)result);
            MySqlCommand newComand = (MySqlCommand)result;
            List<Dictionary<string, string>> data = SelectOption(newComand);
            RebootVariables();
            return data;
        }
    }
}
