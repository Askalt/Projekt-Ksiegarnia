using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Projekt_P4_Bookshop
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string author_name_f;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void username_name_TextChanged(object sender, TextChangedEventArgs e)
        {
            username_name.Text = LoginScreen.username.ToUpper();
          

        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void button_load_book_Click(object sender, RoutedEventArgs e)
        {
       
            SqlConnection sqlConn = new SqlConnection(@"Data Source=DESKTOP-MPTGS57\SQLEXPRESS;Initial Catalog=BookStore;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            try
            {
                sqlConn.Open();
                author_name_f = txt_name_authora_find.Text;
                string query = "select Author_name as Author,Title_name as Title,Published_year as Published,Retail_price as Price from [dbo].[STOCK] where Author_name=@Author_name";        
                SqlCommand sqlCommand = new SqlCommand(query, sqlConn);
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Parameters.AddWithValue("@Author_name", txt_name_authora_find.Text);

                sqlCommand.ExecuteNonQuery();

                SqlDataAdapter dataAdapt = new SqlDataAdapter(sqlCommand);
                DataTable dt = new DataTable("[dbo].[STOCK]");
                dataAdapt.Fill(dt);
                data_grid_table.ItemsSource = dt.DefaultView;
                
                sqlConn.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                throw;
            }

        }

        private void button_sort_desc_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlConn = new SqlConnection(@"Data Source=DESKTOP-MPTGS57\SQLEXPRESS;Initial Catalog=BookStore;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            try
            {
                sqlConn.Open();

                string query = "select Author_name as Author,Title_name as Title,Published_year as Published,Retail_price as Price from [dbo].[STOCK] order by Price desc";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConn);
                sqlCommand.ExecuteNonQuery();

                SqlDataAdapter dataAdapt = new SqlDataAdapter(sqlCommand);
                DataTable dt = new DataTable("[dbo].[STOCK]");
                dataAdapt.Fill(dt);
                data_grid_table.ItemsSource = dt.DefaultView;

                sqlConn.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                throw;
            }
        }

        private void button_sord_asc_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlConn = new SqlConnection(@"Data Source=DESKTOP-MPTGS57\SQLEXPRESS;Initial Catalog=BookStore;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            try
            {
                sqlConn.Open();

                string query = "select Author_name as Author,Title_name as Title,Published_year as Published,Retail_price as Price from [dbo].[STOCK] order by Price";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConn);
                sqlCommand.ExecuteNonQuery();

                SqlDataAdapter dataAdapt = new SqlDataAdapter(sqlCommand);
                DataTable dt = new DataTable("[dbo].[STOCK]");
                dataAdapt.Fill(dt);
                data_grid_table.ItemsSource = dt.DefaultView;

                sqlConn.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                throw;
            }
        }
    }
}
