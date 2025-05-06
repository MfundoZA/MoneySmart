using MoneySmart.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Data.SqlClient;
using System.Text;

namespace MoneySmart.Data
{
    public class Database
    {
        private SqlConnection Connection { get; set; }
        private string ConnectionString { get; set; }

        public Database()
        {   
            Connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MoneySmart;Integrated Security=True");
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
                string getAllTransactions = "SELECT [Id], [Description], [Type], amount, PaymentMethod FROM [Transaction] " +
                    "ORDER BY type DESC";
                SqlCommand command = new SqlCommand(getAllTransactions, Connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Transaction transaction = new Transaction();
                    transaction.Id = (int)reader[0];
                    transaction.Description = (string)reader[1];

                    Models.Type transactionType;
                    Enum.TryParse(reader[2].ToString(), out transactionType);
                    transaction.Type = transactionType;

                    transaction.Amount = (decimal)reader[3];

                    PaymentMethod paymentMethod;
                    Enum.TryParse(reader[4].ToString(), out paymentMethod);
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
                string addTransaction = "INSERT INTO [Transaction] ([Description], [Type], amount, PaymentMethod) VALUES" +
                    $"('{transaction.Description}', {(int)transaction.Type}, {transaction.Amount}, {(int)transaction.PaymentMethod});";

                SqlCommand command = new SqlCommand(addTransaction, Connection);
                command.ExecuteNonQuery();
            }

            Connection.Close();
        }

        public void updateTransaction(Transaction transaction)
        {
            Connection.Open();
            if (Connection.State == System.Data.ConnectionState.Open)
            {
                string updateTransaction = "UPDATE [Transaction] " +
                    $"SET [Description] = '{transaction.Description}',  [Type] = {((int)transaction.Type)}, " +
                    $"amount = {transaction.Amount}, PaymentMethod = {(int)transaction.PaymentMethod}" +
                    $" WHERE [Id] = {transaction.Id}";

                SqlCommand command = new SqlCommand(updateTransaction, Connection);
                command.ExecuteNonQuery();
            }

            Connection.Close();
        }

        public bool deleteTransaction(Transaction transaction)
        {
            try
            {
                Connection.Open();
                if (Connection.State == System.Data.ConnectionState.Open)
                {
                    string deleteTransaction = "DELETE FROM [Transaction]" +
                        $"WHERE {transaction.Id} = [Transaction].[Id]";

                    SqlCommand command = new SqlCommand(deleteTransaction, Connection);
                    command.ExecuteNonQuery();
                }
            }
            catch
            {
                Connection.Close();
                return false;
            }

            Connection.Close();
            return true;
        }

        public string getTheme()
        {
            string appTheme = null;

            Connection.Open();
            if (Connection.State == System.Data.ConnectionState.Open)
            {
                string getTheme = "SELECT [Value] FROM Setting WHERE [Key] = 'Theme'";
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
