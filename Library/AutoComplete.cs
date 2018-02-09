using System;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace Library
{
    class AutoComplete
    {
        MySqlConnection connection;
        public AutoComplete()
        {
            string connectionString = "datasource='127.0.0.1'; port='3306'; username='root'; password=''; database='library';";
            connection = new MySqlConnection(connectionString);
        }
        public void update(String query, TextBox tb)
        {
            connection.Open();
            MySqlCommand command = new MySqlCommand(query, connection);
            AutoCompleteStringCollection myCollection = new AutoCompleteStringCollection();
            try
            {
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    myCollection.Add(reader.GetString(0));
                }
                reader.Close();
                tb.AutoCompleteCustomSource = myCollection;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
