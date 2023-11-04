using MoneySmart.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MoneySmart.Views
{
    /// <summary>
    /// Interaction logic for EditTransactionWindow.xaml
    /// </summary>
    public partial class EditTransactionWindow : Window
    {
        public EditTransactionWindow()
        {
            InitializeComponent();

            var viewModel = (MainViewModel) this.DataContext;
            var transaction = viewModel.Transactions[viewModel.selectedIndex];

            txtId.Text = transaction.Id.ToString();
            txtDescription.Text = transaction.Description;
            cmbTransactionType.SelectedIndex = (int) transaction.Type;
            txtAmount.Text = transaction.Amount.ToString();
            cmbPaymentMethod.SelectedIndex = (int)transaction.PaymentMethod;
        }

        private void btnEditTransaction_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
