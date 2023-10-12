using MoneySmart.Models;
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
    /// Interaction logic for NewExpenseWindow.xaml
    /// </summary>
    public partial class NewExpenseWindow : Window
    {
        public NewExpenseWindow()
        {
            InitializeComponent();
        }

        private void btnCreateExpense_Click(object sender, RoutedEventArgs e)
        {
            string description = txtDescription.Text;
            Models.Type type = Models.Type.Expense;
            decimal amount = Decimal.Parse(txtAmount.Text);
            PaymentMethod paymentMethod;

            switch (cmbPaymentMethod.SelectedIndex)
            {
                case 0:
                    paymentMethod = PaymentMethod.Cash;
                    break;

                case 1:
                    paymentMethod = PaymentMethod.Card;
                    break;

                case 2:
                    paymentMethod = PaymentMethod.EFT;
                    break;

                default:
                    paymentMethod = PaymentMethod.Cash;
                    break;
            }

            Transaction expense = new Transaction(description, type, amount, paymentMethod);
            App.database.addTransaction(expense);

            this.DialogResult = true;
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
