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

namespace DrainMind.View
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        private MainWindow mw;
        /// <summary>
        /// Window du Menu
        /// </summary>
        public Menu()
        {
            //Centre la fenetre
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
            mw = new MainWindow();

        }
        /// <summary>
        /// Lance le jeux
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            mw.Show();           
        }
        /// <summary>
        /// Quitte totalement le jeux
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
        /// <summary>
        /// Ouvre les options
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OptionButton_Click(object sender, RoutedEventArgs e)
        {
            Options o = new Options(this);
            this.Hide();
            o.Show();
            
        }
    }
}
