using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;
using System.Drawing;
using System.Data.Common;

namespace Warehouse
{
    class Table
    {
        private SqlConnection sqlConnection;
        private SqlDataAdapter sqlDataAdapter;
        private DataSet dataSet;


        public Table(ref DataGridView dataGridView)
        {
            this.sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

            //
            string selectQuery = "SELECT * FROM Warehouse;";
            this.sqlDataAdapter = new SqlDataAdapter(selectQuery, this.sqlConnection);

            // СОЗДАНИЕ ЗАПРОСОВ INSERT, UPDATE и DELETE
            SqlCommand insertCommand = new SqlCommand("INSERT INTO Warehouse (ProductName, TypeId, SupplierId, Quantity, Price, DateAdded) VALUES (@ProductName, @TypeId, @SupplierId, @Quantity, @Price, @DateAdded)", this.sqlConnection);
            
            insertCommand.Parameters.Add("@ProductName", SqlDbType.NVarChar, 100, "ProductName");
            insertCommand.Parameters.Add("@TypeId", SqlDbType.Int, 4, "TypeId");
            insertCommand.Parameters.Add("@SupplierId", SqlDbType.Int, 4, "SupplierId");
            insertCommand.Parameters.Add("@Quantity", SqlDbType.Int, 4, "Quantity");
            insertCommand.Parameters.Add("@Price", SqlDbType.Money, 8, "Price");
            insertCommand.Parameters.Add("@DateAdded", SqlDbType.Date, 3, "DateAdded");
            insertCommand.Parameters.Add("@Id", SqlDbType.Int, 4, "Id");

            SqlCommand updateCommand = new SqlCommand("UPDATE Warehouse SET ProductName = @ProductName, TypeId = @TypeId, SupplierId = @SupplierId, Quantity = @Quantity, Price = @Price, DateAdded = @DateAdded WHERE Id = @Id", this.sqlConnection);
            updateCommand.Parameters.Add("@Id", SqlDbType.Int, 4, "Id");
            updateCommand.Parameters.Add("@ProductName", SqlDbType.NVarChar, 100, "ProductName");
            updateCommand.Parameters.Add("@TypeId", SqlDbType.Int, 4, "TypeId");
            updateCommand.Parameters.Add("@SupplierId", SqlDbType.Int, 4, "SupplierId");
            updateCommand.Parameters.Add("@Quantity", SqlDbType.Int, 4, "Quantity");
            updateCommand.Parameters.Add("@Price", SqlDbType.Money, 8, "Price");
            updateCommand.Parameters.Add("@DateAdded", SqlDbType.Date, 3, "DateAdded");

            SqlCommand deleteCommand = new SqlCommand("DELETE FROM Warehouse WHERE Id = @Id", this.sqlConnection);
            deleteCommand.Parameters.Add("@Id", SqlDbType.Int, 4, "Id");

            // ДОБАВЛЕНИЕ ЗАПРОСОВ INSERT, UPDATE И DELETE В sqlDataAdapter
            this.sqlDataAdapter.InsertCommand = insertCommand;
            this.sqlDataAdapter.UpdateCommand = updateCommand;
            this.sqlDataAdapter.DeleteCommand = deleteCommand;

            // УКАЗАНИЕ АВТОИНКРЕМЕНТА НА ПОЛЕ Id
            insertCommand.Parameters["@Id"].Value = DBNull.Value;
            insertCommand.Parameters["@Id"].Direction = ParameterDirection.Output;

            // ЗАГРУЗКА ДАННЫХ В DataSet И ПРИВЯЗКА К DataGridView
            this.dataSet = new DataSet();
            this.sqlDataAdapter.Fill(dataSet, "Warehouse");

            dataGridView.DataSource = this.dataSet.Tables["Warehouse"];
        }


        public void OpenConnection()
        {
            this.sqlConnection.Open();
        }


        public DataTable ReadData(string cmdText)
        {
            SqlDataReader sqlDataReader = null;
            SqlCommand sqlCommand = new SqlCommand(cmdText, this.sqlConnection);
            DataTable dataTable = new DataTable();

            try
            {
                sqlDataReader = sqlCommand.ExecuteReader();

                for (int i = 0; i < sqlDataReader.FieldCount; i++) // вывод названия столбцов
                {
                    dataTable.Columns.Add(sqlDataReader.GetName(i));
                }

                while (sqlDataReader.Read()) // вывод значений столбцов
                {
                    DataRow dataRow = dataTable.NewRow();
                    for (int i = 0; i < sqlDataReader.FieldCount; i++)
                    {
                        dataRow[i] = sqlDataReader[i];
                    }
                    dataTable.Rows.Add(dataRow);
                }

                return dataTable;
            }
            catch (SqlException se)
            {
                MessageBox.Show($"Ошибка при считывании данных:\n{se.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                sqlDataReader?.Close();
                sqlCommand?.Dispose();
            }
        }


        public void Do(string cmdText)
        { 
            SqlCommand sqlCommand = new SqlCommand(cmdText, this.sqlConnection);
            sqlCommand.ExecuteNonQuery();
        }

        public void Update()
        {
            this.sqlDataAdapter.Update(this.dataSet, "Warehouse");
        }

        public void Delete(int id)
        {
            this.sqlDataAdapter.DeleteCommand.Parameters["@Id"].Value = id;
            this.sqlDataAdapter.DeleteCommand.ExecuteNonQuery();
        }

        public void CloseConnection()
        {
              this.sqlConnection.Close();
        }
    }
}
