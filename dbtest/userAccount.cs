using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace dbtest {
    class userAccount {
        public Account account {  get; set; }
        public static List<Inventory> userCartDatabase { get; set; } = new List<Inventory>();
        public userAccount(Account acc) {
            this.account = acc;
        }

        //this method get the Inventory item per execution and then store it in the table userCartTransactions
        public void putItemFromUserChoice(Inventory item) {
            Account instanceAccount = new();
            MySqlConnection conn = DatabaseConnection.GetConnection();

            using (conn) {
                try {
                    conn.Open();

                    string query = "INSERT INTO temporaryCartUser (drug_id, user_id, alreadyCheckout) VALUES (@drug_id, @user_id, 0)";
                    MySqlCommand cmd = new(query, conn);

                    using (cmd) {
                        cmd.Parameters.AddWithValue("@drug_id", item.DrugID);
                        cmd.Parameters.AddWithValue("@user_id", instanceAccount.getUserId(account));

                        cmd.ExecuteNonQuery();
                    }

                } catch (Exception ex) {
                    Console.WriteLine("Error while getting the cart: " + ex.Message);
                    return;
                }
            }
        }

        public List<Inventory> getCartUserFromDatabase() {
            Account instanceAccount = new Account();
            MySqlConnection conn = DatabaseConnection.GetConnection();


            using (conn) {
                try {
                    conn.Open();

                    string query = "SELECT tempCart.drug_id AS drugID, inventory.drug_name AS drugName, inventory.manufacturer AS manufacturer, inventory.price AS price " +
                                   "FROM temporaryCartUser tempCart " +
                                   "LEFT JOIN drug_inventory inventory ON tempCart.drug_id = inventory.drug_id " +
                                   "WHERE user_id = @user_id";

                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    using (cmd) {
                        cmd.Parameters.AddWithValue("@user_id", instanceAccount.getUserId(account));

                        using (MySqlDataReader reader = cmd.ExecuteReader()) {
                            while (reader.Read()) {
                                Inventory userCart = new Inventory {
                                    DrugID = reader.GetInt32("drugID"),
                                    DrugName = reader.GetString("drugName").ToLower(),
                                    DrugManufacturer = reader.GetString("manufacturer"),
                                    DrugPrice = reader.GetDecimal("price"),
                                };

                                userCartDatabase.Add(userCart);
                            }
                        }
                    }
                } catch (Exception ex) {
                    Console.WriteLine("Error fetching the cart from database: " + ex.Message);
                }
            }
            return userCartDatabase;
        }

        public bool isUserCartNotEmpty() {
            Account instanceAccount = new();
            MySqlConnection conn = DatabaseConnection.GetConnection();

            using (conn) {
                try {
                    conn.Open();

                    string query = "SELECT Count(*) FROM temporaryCartUser WHERE user_id = @user_id";
                    MySqlCommand cmd = new(query, conn);

                    using (cmd) {
                        cmd.Parameters.AddWithValue("@user_id", instanceAccount.getUserId(account));

                        object rows = cmd.ExecuteScalar();

                        int countRows = Convert.ToInt32(rows);
                        return countRows > 0;

                    }
                } catch (Exception ex) {
                    Console.WriteLine("Error whle checking the user cart: " + ex.Message);
                    return false;
                }
            }
        }
    }
}
