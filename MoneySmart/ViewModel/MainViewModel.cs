using MoneySmart.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoneySmart.ViewModel
{
    class MainViewModel
    {
        private Database database { get; set; }

        public MainViewModel()
        {
            database = new Database();
        }
    }
}
