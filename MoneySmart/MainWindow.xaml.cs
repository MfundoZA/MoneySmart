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
            this.DataContext = viewModel;
        }

        private void mniNewIncomeOnClick(object sender, RoutedEventArgs e)
        {
            var newIncomeWindow = new NewIncomeWindow();

            // Returns wheter the task was completed or canceled
            bool? result = newIncomeWindow.ShowDialog();
            viewModel.updateMontlyProperties();
        }

        private void mniNewExpenseOnClick(object sender, RoutedEventArgs e)
        {
            var newExpenseWindow = new NewExpenseWindow();

            // Returns wheter the task was completed or canceled
            bool? result = newExpenseWindow.ShowDialog();
            viewModel.updateMontlyProperties();
        }

        private void mniDashboardOnClick(object sender, RoutedEventArgs e)
        {

        }
        private void mniSettingsOnClick(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Activated(object sender, EventArgs e)
        {
            viewModel.updateMontlyProperties();
            lstTransactions.ItemsSource = viewModel.Transactions;
        }

        private void cniEdit_Click(object sender, RoutedEventArgs e)
        {
            if (lstTransactions.SelectedIndex == -1)
            {
                return;
            }

            viewModel.selectedIndex = lstTransactions.SelectedIndex;
            var transaction = viewModel.Transactions[lstTransactions.SelectedIndex];
            var newEditTransactionWindow = new Window();
            
            newEditTransactionWindow = new EditTransactionWindow();

            newEditTransactionWindow.DataContext = viewModel;
            newEditTransactionWindow.ShowDialog();
        }

        private void cniDelete_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
