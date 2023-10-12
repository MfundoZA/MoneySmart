using MoneySmart.Data;
using MoneySmart.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Text;

namespace MoneySmart.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Transaction> Transactions { get; set; }
        public decimal MonthlyIncome { get; set; }
        public decimal MonthlyExpenses { get; set; }
        public decimal MonthlySavings { get; set; }
        public string FormattedMonthlyIncome { get; set; }
        public string FormattedMonthlyExpenses { get; set; }
        public string FormattedMonthlySavings { get; set; }
        public static Database database { get; private set; }

        public MainViewModel()
        {
            database = App.database;

            refreshMontlyPanels();
        }

        public void refreshMontlyPanels()
        {
            Transactions = database.getTransactions();

            decimal incomeSum = 0;
            decimal expensesSum = 0;

            foreach (Transaction transaction in Transactions)
            {
                if (transaction.Type == Models.Type.Income)
                {
                    incomeSum += transaction.Amount;
                }
                else
                {
                    expensesSum += transaction.Amount;
                }
            }

            MonthlyIncome = incomeSum;
            MonthlyExpenses = expensesSum;
            MonthlySavings = MonthlyIncome - MonthlyExpenses;

            FormattedMonthlyIncome = string.Format("{0:C0}", MonthlyIncome);
            FormattedMonthlyExpenses = string.Format("{0:C0}", MonthlyExpenses);
            FormattedMonthlySavings = string.Format("{0:C0}", MonthlySavings);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
