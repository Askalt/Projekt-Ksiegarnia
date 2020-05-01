using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Projekt_P4_Bookshop
{
    /// <summary>
    /// Logika interakcji dla klasy Cart.xaml
    /// </summary>
    public partial class Cart : Window
    {
        public Cart()
        {
            InitializeComponent();
        }

        private void customer_name_cart_TextChanged(object sender, TextChangedEventArgs e)
        {
            customer_name_cart.Text = LoginScreen.username.ToUpper();
           
        }
        private void customer_id_card_TextChanged(object sender, TextChangedEventArgs e)
        {
            customer_id_card.Text = LoginScreen.username_id_lw;
        }

        private void button_return_cart_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

    
    }
}
