using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Library
{
    public partial class loginForm : Form
    {
        MySqlConnection connection;
        public loginForm()
        {
            InitializeComponent();
        }

        private void login_b_login_Click(object sender, EventArgs e)
        {
            string connectionString = "datasource='127.0.0.1'; port='3306'; username='root'; password=''; database='library';";
            try
            {
                connection = new MySqlConnection(connectionString);
                connection.Open();
                string query = string.Format($"SELECT loginName,password FROM login WHERE loginName='{name_TB_login.Text}' && password='{password_TB_login.Text}'");
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read() && name_TB_login.Text == reader.GetString(0) && password_TB_login.Text == reader.GetString(1))
                {
                    reader.Close();
                    mainForm main = new mainForm();
                    main.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Enter correct Username and Password");
                }
            }
            catch(Exception ex)
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
