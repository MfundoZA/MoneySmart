using MoneySmart.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Text;

namespace MoneySmart.Data
{
    public class Database
    {
        private SqlConnection Connection { get; set; }
        private string ConnectionString { get; set; }

        public Database()
        {
            ConnectionString = "Data Source=JORDAN;Initial Catalog=MoneySmart;Integrated Security=True";
            Connection = new SqlConnection(ConnectionString);
        }

        public ObservableCollection<Transaction> getTransactions()
        {
            var transactions = new ObservableCollection<Transaction>();

            if (Connection.State != System.Data.ConnectionState.Open)
            {
                Connection.Open();
            }

            if (Connection.State == System.Data.ConnectionState.Open)
            {
                string getAllTransactions = "SELECT t.id, t.description, [TransactionType].type, t.amount, pay.payment_method FROM [Transaction] " +
                    "AS t INNER JOIN [TransactionType] ON t.type_id = [TransactionType].id " +
                    "INNER JOIN PaymentMethod AS pay ON t.payment_method_id = pay.id " +
                    "ORDER BY [TransactionType].type DESC";
                SqlCommand command = new SqlCommand(getAllTransactions, Connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Transaction transaction = new Transaction();
                    transaction.Id = (int)reader[0];
                    transaction.Description = (string)reader[1];

                    Models.Type transactionType;
                    Enum.TryParse((string)reader[2], out transactionType);
                    transaction.Type = transactionType;

                    transaction.Amount = (decimal)reader[3];

                    Models.PaymentMethod paymentMethod;
                    Enum.TryParse((string)reader[4], out paymentMethod);
                    transaction.PaymentMethod = paymentMethod;

                    transactions.Add(transaction);
                }

                Connection.Close();
            }

            return transactions;
        }

        public void addTransaction(Transaction transaction)
        {
            Connection.Open();
            if (Connection.State == System.Data.ConnectionState.Open)
            {
                string addTransaction = "INSERT INTO [Transaction] ([description], [type_id], amount, payment_method_id) VALUES" +
                    $"('{transaction.Description}', {(int)transaction.Type}, {transaction.Amount}, {(int)transaction.PaymentMethod});";

                SqlCommand command = new SqlCommand(addTransaction, Connection);
                command.ExecuteNonQuery();
            }

            Connection.Close();
        }

        public void updateTransction(Transaction transaction)
        {
            Connection.Open();
            if (Connection.State == System.Data.ConnectionState.Open)
            {
                string updateTransaction = "UPDATE [Transaction] " +
                    $"SET [description] = '{transaction.Description}',  [type_id] = {((int)transaction.Type)}, " +
                    $"amount = {transaction.Amount}, payment_method_id = {(int)transaction.PaymentMethod}" +
                    $" WHERE [id] = {transaction.Id}";

                SqlCommand command = new SqlCommand(updateTransaction, Connection);
                command.ExecuteNonQuery();
            }

            Connection.Close();
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

            Connection.Close();
            return appTheme;
        }
    }
}
