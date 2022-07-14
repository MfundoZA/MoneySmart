using MoneySmart.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace MoneySmart.Data
{
    class Database
    {
        private SqlConnection Connection { get; set; }
        private string ConnectionString { get; set; }
        public List<Transaction> Transactions { get; set; }

        public Database()
        {
            ConnectionString = "Data Source=JORDAN;Initial Catalog=MoneySmart;Integrated Security=True";
            Connection = new SqlConnection(ConnectionString);
            Transactions = new List<Transaction>();
            getTransactions();
        }

        public void getTransactions()
        {
            Connection.Open();
            if (Connection.State == System.Data.ConnectionState.Open)
            {
                string getAllTransactions = "SELECT t.id, t.description, [Type].type, t.amount, pay.payment_method FROM [Transaction] " +
                    "AS t INNER JOIN [Type] ON t.type_id = [Type].type_id " +
                    "INNER JOIN PaymentMethod AS pay ON t.payment_method_id = pay.payment_method_id";
                SqlCommand command = new SqlCommand(getAllTransactions, Connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Transaction transaction = new Transaction();
                    transaction.Id = (int) reader[0];
                    transaction.Descrption = (string) reader[1];

                    Models.Type transactionType;
                    Enum.TryParse((string) reader[2], out transactionType);
                    transaction.Type = transactionType;

                    transaction.Amount = (decimal) reader[3];

                    Models.PaymentMethod paymentMethod;
                    Enum.TryParse((string)reader[4], out paymentMethod);
                    transaction.PaymentMethod = paymentMethod;

                    Transactions.Add(transaction);
                }
            }
        }

        public string getTheme()
        {
            string appTheme = null;

            Connection.Open();
            if (Connection.State == System.Data.ConnectionState.Open)
            {
                string getTheme = "SELECT [value] FROM Setting WHERE [key] = 'Theme'";
                SqlCommand command = new SqlCommand(getTheme, Connection);
                SqlDataReader reader = command.ExecuteReader();

                reader.Read();
                appTheme = (string) reader[0];
            }

            return appTheme;
        }
    }
}
