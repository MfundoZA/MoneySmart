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

        public decimal MonthlyIncome
        {
            get
            {
                return MonthlyIncome;
            }

            set
            {
                MonthlyIncome = value;
                OnPropertyChanged(nameof(MonthlyIncome));
            }
        }

        public decimal MonthlyExpenses
        {
            get
            {
                return MonthlyExpenses;
            }

            set
            {
                MonthlyIncome = value;
                OnPropertyChanged(nameof(MonthlyExpenses));
            }
        }

        public decimal MonthlySavings
        {
            get
            {
                return MonthlySavings;
            }

            set
            {
                MonthlySavings = value;
                OnPropertyChanged(nameof(MonthlySavings));
            }
        }

        public string FormattedMonthlyIncome
        {
            get
            {
                return FormattedMonthlyIncome;
            }

            set
            {
                FormattedMonthlyIncome = value;
                OnPropertyChanged(nameof(FormattedMonthlyIncome));
            }
        }

        public string FormattedMonthlyExpenses
        {
            get
            {
                return FormattedMonthlyExpenses;
            }

            set
            {
                FormattedMonthlyExpenses = value;
                OnPropertyChanged(nameof(FormattedMonthlyExpenses));
            }
        }

        public string FormattedMonthlySavings
        {
            get
            {
                return FormattedMonthlySavings;
            }

            set
            {
                FormattedMonthlySavings = value;
                OnPropertyChanged(nameof(FormattedMonthlySavings));
            }
        }

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
