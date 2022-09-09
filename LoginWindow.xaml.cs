using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;

namespace FootballManager
{
    /// <summary>
    /// represent log window system 
    /// </summary>
    public partial class LoginWindow : Window
    {
        /// <summary>
        /// create login window 
        /// </summary>
        public LoginWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// This method is connecting with DB and can give us access to program.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlCon = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename =|DataDirectory|\FootballManagementDB.mdf; Integrated Security = True");


            try
            {
                if(sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                String query = "SELECT COUNT(1) FROM LoginData WHERE Username=@Username AND Password=@Password";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@Username", txtUsername.Text);
                sqlCmd.Parameters.AddWithValue("@Password", txtPassword.Password);
                int count = Convert.ToInt32(sqlCmd.ExecuteScalar());
                if(count == 1)
                {
                    MainWindow gridManagers = new MainWindow();
                    gridManagers.Show();
                    this.Close();

                }
                else
                { 
                    SqlCommand cmd = new SqlCommand("Insert into Error(Username,Password) values ('" + txtUsername.Text + "','" + txtPassword.Password + "')",sqlCon);
                    int i = cmd.ExecuteNonQuery();
                    if (i != 0)
                    {
                        MessageBox.Show("Username or Password is incorreect!");
                    }
                    sqlCon.Close();
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlCon.Close();
            }
        }
    }
}
