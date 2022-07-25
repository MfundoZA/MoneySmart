using MoneySmart.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoneySmart.ViewModel
{
    public class MainViewModel
    {
        public Database database { get; private set; }

        public MainViewModel()
        {
            database = new Database();
        }
    }
}
