using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace dbtest {
    class userAccount {
        public Account account {  get; set; }
        public userAccount(Account acc) {
            this.account = acc;
        }

        //this method get the Inventory item per execution and then store it in the table userCartTransactions
        public void getItemFromUserChoice(string transaction, Inventory item) {
            MySqlConnection conn = DatabaseConnection.GetConnection();

            using (conn) {
                try {
                    conn.Open();

                    string query = "INSERT INTO userCartTransaction (drug_id, transactionID) VALUES (@drug_id, @transactionID)";
                    MySqlCommand cmd = new(query, conn);

                    using (cmd) {
                        cmd.Parameters.AddWithValue("@drug_id", item.DrugID);
                        cmd.Parameters.AddWithValue("@transactionID", transaction);

                        cmd.ExecuteNonQuery();
                    }

                } catch (Exception ex) {
                    Console.WriteLine("Error while getting the cart: " + ex.Message);
                    return;
                }
            }
        }

        public void itemCheckout() {
            //once this method will run, it will make the isActive into 0 so that when the method keepActiveTransaction from transaction runs and with the same user, the already done transaction will not be chosen and will return the list into null
        }

    }
}
