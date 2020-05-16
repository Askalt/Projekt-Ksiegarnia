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
        public static string username_id_mw;
        public static string author_name_f;
       
        public static string order_id_gen;
        public static int index_ID;




        public MainWindow()
        {
            InitializeComponent();
        }

        private void username_name_TextChanged(object sender, TextChangedEventArgs e)
        {
            username_name.Text = LoginScreen.username.ToUpper();
      
        }

        private void txt_username_id_mw_TextChanged(object sender, TextChangedEventArgs e)
        {
            txt_username_id_mw.Text = LoginScreen.username_id_lw;
        }

        private void button_load_book_Click(object sender, RoutedEventArgs e)
        {
 
            SqlConnection sqlConn = new SqlConnection(@"Data Source=DESKTOP-MPTGS57\SQLEXPRESS;Initial Catalog=BookStore;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            try
            {
                sqlConn.Open();
                author_name_f = txt_name_authora_find.Text;
                string query = "select Author_name as Author,Title_name as Title,Published_year as Published,Retail_price as Price from [dbo].[STOCK]";        
                SqlCommand sqlCommand = new SqlCommand(query, sqlConn);
                sqlCommand.CommandType = CommandType.Text;
              

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

        private void find_book_button_Click(object sender, RoutedEventArgs e)
        {
            if (txt_name_authora_find.Text == "")
            {
                MessageBox.Show("Author name is empty");
            }
            else
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
                    
                }

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            var data = data_grid_table.SelectedItem;
            string author_n = (data_grid_table.SelectedCells[0].Column.GetCellContent(data) as TextBlock).Text;
            txt_author_shop.Text = author_n;
            string title_n = (data_grid_table.SelectedCells[1].Column.GetCellContent(data) as TextBlock).Text;
            txt_title_shop.Text = title_n;
            string price_n = (data_grid_table.SelectedCells[3].Column.GetCellContent(data) as TextBlock).Text;
            txt_price_shop.Text = price_n;
            
            string connectionString=@"Data Source=DESKTOP-MPTGS57\SQLEXPRESS;Initial Catalog=BookStore;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            using (SqlConnection sqlConn2 = new SqlConnection(connectionString))
            {
                var query = "SELECT MAX ([Order_ID]) FROM [dbo].[Cart]";
                SqlCommand sqlCmd = new SqlCommand(query, sqlConn2);
                sqlConn2.Open();
                order_id_gen = sqlCmd.ExecuteScalar().ToString();
                index_ID = Int32.Parse(order_id_gen);
                index_ID++;
                txt_order_generic.Text = MainWindow.index_ID.ToString();

                sqlConn2.Close();
            }
            using (SqlConnection sqlConn = new SqlConnection(connectionString))
            {
                username_id_mw = LoginScreen.username_id_lw;
                sqlConn.Open();

                if (txt_numer_book.Text == "")
                {
                    MessageBox.Show("Something is empty");
                }
                else
                {
                SqlCommand sqlCmd = new SqlCommand("CartBookAdd", sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@Order_ID", txt_order_generic.Text.Trim());
                sqlCmd.Parameters.AddWithValue("@Customer_ID", txt_username_id_mw.Text.Trim());
                sqlCmd.Parameters.AddWithValue("@Book_name", txt_title_shop.Text.Trim());
                sqlCmd.Parameters.AddWithValue("@Price", txt_price_shop.Text.Trim());
                sqlCmd.Parameters.AddWithValue("@Amount", txt_numer_book.Text.Trim());
                sqlCmd.ExecuteNonQuery();
                MessageBox.Show("Dodano do koszyka!");
                 }
            }





        }

        private void book_store_customer_want_Click(object sender, RoutedEventArgs e)
        {
            Cart cart = new Cart();
            cart.Show();
            this.Close();

        }





 
    }
}
