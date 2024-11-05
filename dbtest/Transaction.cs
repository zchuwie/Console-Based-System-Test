using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace dbtest {
    class Transaction {
        public Account account { get; set; }
        public string transactionID {  get; set; }

        public Transaction() { }
        public Transaction(Account acc, string transactionID) {
            this.account = acc;
            this.transactionID = transactionID;
        }

        //this inserts the unique transaction number of an ID. It serves as for the purpose of using JOINS so that you can see all the items in the getItemFromUserChoice method from userAccount
        public void insertIntoGeneralTransaction () {
            Account instanceAccount = new();
            MySqlConnection conn = DatabaseConnection.GetConnection();

            using (conn) {
                try {
                    conn.Open ();

                    string query = "INSERT INTO usertransactions (userTransactionID, user_id, isActive) VALUES (@userTransaction, @user_id, 1)";
                    MySqlCommand cmd = new(query, conn);

                    using (cmd) {
                        cmd.Parameters.AddWithValue("@userTransaction", transactionID);
                        cmd.Parameters.AddWithValue("@user_id", instanceAccount.getUserId(account));

                        cmd.ExecuteNonQuery();                        
                    }
                } catch (Exception ex) {
                    Console.WriteLine("inserting transaction in main table error: " + ex.Message);
                    return;
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

                        string query = "SELECT COUNT(*) FROM usertransactions WHERE userTransactionID = @usertransactionID";
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

        //checking if the isActive of the transaction is 1 or 0;
        public bool isTransactionIDActive() {
            MySqlConnection conn = DatabaseConnection.GetConnection();
            using (conn) {
                try {
                    conn.Open();

                    string query = "SELECT isActive FROM userTransactions WHERE userTransactionID = @userTramsactionID";
                    MySqlCommand cmd = new(query, conn);

                    using (cmd) {
                        cmd.Parameters.AddWithValue("@userTransactionID", transactionID);
                        object result = cmd.ExecuteScalar();

                        if (result == null) {
                            return false;
                        }

                        bool isActive = Convert.ToBoolean(result);
                        return isActive;

                    }
                } catch (Exception ex) {
                    Console.WriteLine("Error finding activeness of transaction: " + ex.Message);
                }
            }
            return false;
        }

        //when inserting transactionID from the same user (from the inside method of userPanel class), it will check first if there is still active transactionID (means not yet checked out), if so, we will not insert the transactionID into table but will just use it as reference for the class 
        public string keepActiveTransactionID() {
            Account instanceAccount = new Account();
            MySqlConnection conn = DatabaseConnection.GetConnection();
            string transactionID = null;
            using (conn) {
                try {
                    conn.Open();

                    string query = "SELECT userTransactionID FROM userTransactions WHERE user_id = @user_id && isActive = 1";
                    MySqlCommand cmd = new(query, conn);

                    using (cmd) {
                        cmd.Parameters.AddWithValue("@user_id", instanceAccount.getUserId(account));
                        object result = cmd.ExecuteScalar();

                        if (result != null) {
                            transactionID = (string)result;
                        } 
                    }
                } catch (Exception ex) {
                    Console.WriteLine("Error keeping transaction ID: " + ex.Message);
                    return null;
                }
            }
            return transactionID;
        }
    }
}
