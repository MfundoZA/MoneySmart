using MoneySmart.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoneySmart.ViewModel
{
    public class MainViewModel
    {
        public ObservableCollection<Transaction> Transactions { get; set; }

        public MainViewModel()
        {
            database = new Database();
            Transactions = database.getTransactions();
        }
    }
}
