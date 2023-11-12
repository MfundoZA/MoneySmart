using MoneySmart.Models;
using MoneySmart.ViewModel;
using System;
using System.Windows;
using Type = MoneySmart.Models.Type;

namespace MoneySmart.Views
{
    /// <summary>
    /// Interaction logic for EditTransactionWindow.xaml
    /// </summary>
    public partial class EditTransactionWindow : Window
    {
        public MainViewModel viewModel { get; set; }

        public EditTransactionWindow()
        {
            InitializeComponent();

            var transaction = MainViewModel.Transactions[MainWindow.SelectedTransaction];

            txtId.Text = transaction.Id.ToString();
            txtDescription.Text = transaction.Description;
            cmbTransactionType.SelectedIndex = (int) transaction.Type;
            txtAmount.Text = transaction.Amount.ToString();
            cmbPaymentMethod.SelectedIndex = (int)transaction.PaymentMethod;
        }

        private void btnEditTransaction_Click(object sender, RoutedEventArgs e)
        {
            var transactionType = (Type) cmbTransactionType.SelectedIndex;
            var paymentMethod = (PaymentMethod) cmbPaymentMethod.SelectedIndex;

            var editedTransaction = new Transaction(Int32.Parse(txtId.Text), txtDescription.Text, transactionType, Decimal.Parse(txtAmount.Text), paymentMethod);


            // Update list then update database
            MainViewModel.Transactions[MainWindow.SelectedTransaction] = editedTransaction;
            MainViewModel.database.updateTransction(editedTransaction);
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
