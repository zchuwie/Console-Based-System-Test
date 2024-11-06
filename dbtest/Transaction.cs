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

        
        public void insertIntoUserTransaction () {
           
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
    }
}
