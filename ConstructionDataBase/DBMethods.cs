using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionDataBase
{
    class DBMethods
    {
        SqlConnection connection = new SqlConnection(@"Data Source=(localdb)\v11.0;Initial Catalog=ConstructionDB;Integrated Security=True");

        //SELECT
        public DataTable Select(string table)
        {
            string query = "SELECT * FROM " + table;
            SqlCommand command = new SqlCommand(query, connection);
            DataTable data = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(data);
            return data;
        }

        //INSERT
        public void InsertImone(Imone im)
        {
            connection.Open();

            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Imone", connection);
            DataTable data = new DataTable();
            adapter.Fill(data);

            SqlCommand cmd = new SqlCommand("INSERT INTO Imone(Id,Pavadinimas,Adresas) VALUES(@id, @pavadinimas, @adresas)");
            cmd.Connection = connection;
            cmd.Parameters.AddWithValue("@id", im.Id);
            cmd.Parameters.AddWithValue("@pavadinimas", im.Pavadinimas);
            cmd.Parameters.AddWithValue("@adresas", im.Adresas);

            adapter.InsertCommand = cmd;

            DataRow newRow = data.NewRow();
            newRow["Id"] = im.Id;
            newRow["Pavadinimas"] = im.Pavadinimas;
            newRow["Adresas"] = im.Adresas;

            data.Rows.Add(newRow);
            adapter.Update(data);
               
            connection.Close();
        }

        //UPDATE
        public void UpdateImone(int id, Imone im)
        {
            connection.Open();
            SqlCommand cmd = new SqlCommand("UPDATE Imone SET Pavadinimas = @pavadinimas, Adresas = @adresas WHERE Id = " + id, connection);
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Imone", connection);
            DataTable data = new DataTable();
            adapter.Fill(data);

            cmd.Parameters.AddWithValue("@pavadinimas", im.Pavadinimas);
            cmd.Parameters.AddWithValue("@adresas", im.Adresas);

            adapter.UpdateCommand = cmd;

            data.Rows[0]["Pavadinimas"] = im.Pavadinimas;
            data.Rows[0]["Adresas"] = im.Adresas;

            adapter.Update(data);
            connection.Close();
        }

        //DELETE
        public void DeleteImone(int id)
        {
            connection.Open();
            SqlCommand cmd = new SqlCommand("DELETE FROM Imone WHERE Id = " + id, connection);
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Imone", connection);
            DataTable data = new DataTable();
            adapter.Fill(data);

            adapter.DeleteCommand = cmd;

            data.Rows[0].Delete();

            adapter.Update(data);
            connection.Close();
        }



        public List<long> FillComboBox(string table, string field)
        {
            List<long> items = new List<long>();
            long ID;
            string cmdText = "SELECT * FROM " + table;
            SqlCommand cmd = new SqlCommand(cmdText, connection);
            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                ID = Convert.ToInt64(reader[field].ToString());
                items.Add(ID);
            }
            
            connection.Close();
            
            return items;
        }

        public DataTable SkipAndTake(int skip, int take, DataTable data)
        {
            return data.AsEnumerable().Skip(skip).Take(take).CopyToDataTable();
        }

        public DataTable GroupAndSum(DataTable data)
        {
            var groupedData = from d in data.AsEnumerable()
                              group d by d.Field<int>("Statybviete") into g
                              select new
                              {
                                  Statybviete = g.Key,
                                  Count = g.Count(),
                                  AlguSuma = g.Sum(x => x.Field<int>("Alga"))
                              };

            DataTable myDataTable = new DataTable();

            myDataTable.Columns.Add("Statybviete", typeof(int));
            myDataTable.Columns.Add("Count", typeof(int));
            myDataTable.Columns.Add("Algu suma", typeof(int));

            foreach (var element in groupedData)
            {
                var row = myDataTable.NewRow();
                row["Statybviete"] = element.Statybviete;
                row["Count"] = element.Count;
                row["Algu suma"] = element.AlguSuma;
                myDataTable.Rows.Add(row);
            }

            return myDataTable;
        }

    }
}
