using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace Library
{
    class Grid
    {
        MySqlConnection connection;
        public void update(string query, DataGridView gv)
        {
            try
            {
                string connectionString = "datasource='127.0.0.1'; port='3306'; username='root'; password=''; database='library';";
                connection = new MySqlConnection(connectionString);
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                DataTable dTable = new DataTable();
                adapter.Fill(dTable);
                gv.DataSource = dTable;
                gv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
