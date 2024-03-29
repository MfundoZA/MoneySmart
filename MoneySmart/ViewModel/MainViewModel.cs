﻿using MoneySmart.Data;
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
        public int SelectedIndex { get; set; }
        private decimal monthlyIncome;
        private decimal monthlyExpenses;
        private decimal monthlySavings;

        private string formattedMonthlyIncome;
        private string formattedMonthlyExpenses;
        private string formattedMonthlySavings;

        public static Database Database { get; private set; }

        public decimal MonthlyIncome
        {
            get
            {
                return monthlyIncome;
            }

            set
            {
                monthlyIncome = value;
                OnPropertyChanged(nameof(MonthlyIncome));
            }
        }

        public decimal MonthlyExpenses
        {
            get
            {
                return monthlyExpenses;
            }

            set
            {
                monthlyIncome = value;
                OnPropertyChanged(nameof(MonthlyExpenses));
            }
        }

        public decimal MonthlySavings
        {
            get
            {
                return monthlySavings;
            }

            set
            {
                monthlySavings = value;
                OnPropertyChanged(nameof(MonthlySavings));
            }
        }

        public string FormattedMonthlyIncome
        {
            get
            {
                return formattedMonthlyIncome;
            }

            set
            {
                formattedMonthlyIncome = value;
                OnPropertyChanged(nameof(FormattedMonthlyIncome));
            }
        }

        public string FormattedMonthlyExpenses
        {
            get
            {
                return formattedMonthlyExpenses;
            }

            set
            {
                formattedMonthlyExpenses = value;
                OnPropertyChanged(nameof(FormattedMonthlyExpenses));
            }
        }

        public string FormattedMonthlySavings
        {
            get
            {
                return formattedMonthlySavings;
            }

            set
            {
                formattedMonthlySavings = value;
                OnPropertyChanged(nameof(FormattedMonthlySavings));
            }
        }

        public static Database database { get; private set; }

        public MainViewModel()
        {
            Database = App.database;
            Transactions = Database.getTransactions();
            updateMontlyProperties();
        }

        public void updateMontlyProperties()
        {
            Transactions = Database.getTransactions();
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

            monthlyIncome = incomeSum;
            monthlyExpenses = expensesSum;
            monthlySavings = MonthlyIncome - MonthlyExpenses;

            var ci = new CultureInfo("en-ZA");

            FormattedMonthlyIncome = MonthlyIncome.ToString("C", ci);
            FormattedMonthlyExpenses = MonthlyExpenses.ToString("C", ci);
            FormattedMonthlySavings = MonthlySavings.ToString("C", ci);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void deleteTransaction(int selectedIndex)
        {
            Database.deleteTransaction(Transactions[selectedIndex]);
            Transactions.RemoveAt(selectedIndex);
            updateMontlyProperties();
        }
    }
}
