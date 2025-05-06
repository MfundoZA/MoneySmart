using MoneySmart.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MoneySmart
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Database database { get; private set; }

        public App()
        {
            database = new Database();
            //String appTheme = database.getTheme();

        }
    }
}
