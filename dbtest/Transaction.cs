using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace dbtest {
    class Transaction {
        public Account account { get; set; }

        public Transaction() { }
        public Transaction(Account acc) {
            this.account = acc;
        }

        
        public void insertIntoUserTransaction (string transactionID) {
            MySqlConnection conn = DatabaseConnection.GetConnection();
            Account instanceAccount = new();

            using (conn) {
                try {
                    conn.Open ();

                    string query = "INSERT INTO userTransaction (transaction_id, user_id) VALUES (@transactionID, @userID)";
                    MySqlCommand cmd = new(query, conn);

                    using (cmd) {
                        cmd.Parameters.AddWithValue("@transactionID", transactionID);
                        cmd.Parameters.AddWithValue("@userID", instanceAccount.getUserId(account));

                        cmd.ExecuteNonQuery();
                    }
                } catch (Exception ex) {
                    Console.WriteLine("Error inserting into user transaction " + ex.Message);
                }
            }
        }

        //just making sure that the generated transaction number doesnt duplicate in the table
        public string uniqueTransactionID() {
            Random rnd = new Random();
            MySqlConnection conn = DatabaseConnection.GetConnection();
            string transactionID = "";

            using (conn) {
                try {
                    conn.Open();

                    do {
                        int generatedNumber = rnd.Next(1000, 9999);
                        transactionID = "100" + generatedNumber.ToString();

                        string query = "SELECT COUNT(*) FROM usertransaction WHERE userTransactionID = @usertransactionID";
                        MySqlCommand cmd = new(query, conn);

                        using (cmd) {
                            cmd.Parameters.AddWithValue("@usertransactionID", transactionID);
                            object result = cmd.ExecuteScalar();

                            int count = Convert.ToInt32(result);

                            if (count == 0) {
                                break;
                            }

                        }

                    } while (true);
                } catch (Exception ex) {
                    Console.WriteLine("Finding unique transaction error: " + ex.Message);
                }
            }
            return transactionID;
        } 
    }
}
