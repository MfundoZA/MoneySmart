using MoneySmart.ViewModel;
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
using MoneySmart.Views;

namespace MoneySmart
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainViewModel viewModel;
        public MainWindow()
        {
            InitializeComponent();
            viewModel = new MainViewModel();
        }

        private void mniNewIncomeOnClick(object sender, RoutedEventArgs e)
        {
            var newIncomeWindow = new NewIncomeWindow();
            bool? result = newIncomeWindow.ShowDialog();
        }

        private void mniNewExpenseOnClick(object sender, RoutedEventArgs e)
        {

        }

        private void mniDashboardOnClick(object sender, RoutedEventArgs e)
        {

        }
        private void mniSettingsOnClick(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Activated(object sender, EventArgs e)
        {
            lstTransactions.ItemsSource = viewModel.database.Transactions;
        }
    }
}
