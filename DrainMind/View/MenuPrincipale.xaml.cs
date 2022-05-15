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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DrainMind.View
{
    /// <summary>
    /// Interaction logic for MenuPrincipale.xaml
    /// </summary>
    public partial class MenuPrincipale : Page
    {
        public MenuPrincipale()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Lance le jeux
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            Window mw = Application.Current.Windows.Cast<Window>().FirstOrDefault(window => window is MainWindow) as MainWindow;
            mw.Content = new DrainMindView();
        }

        /// <summary>
        /// Ouvre les options
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OptionButton_Click(object sender, RoutedEventArgs e)
        {
            Window mw = Application.Current.Windows.Cast<Window>().FirstOrDefault(window => window is MainWindow) as MainWindow;
            mw.Content = new Options(this);
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
    }
}
