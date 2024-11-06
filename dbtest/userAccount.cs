using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace dbtest {
    class userAccount {
        Account instanceAccount = new();
        public Account account { get; set; }
        public static List<Inventory> userCartDatabase { get; set; } = new List<Inventory>();
        public userAccount(Account acc) {
            this.account = acc;
        }

        //this method get the Inventory item per execution and then store it in the table userCartTransactions
        public void putItemFromUserChoice(Inventory item) {
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
                    Console.WriteLine("Error while checking the user cart: " + ex.Message);
                    return false;
                }
            }
        }

        public bool updateCheckoutStatus() {
            MySqlConnection conn = DatabaseConnection.GetConnection();

            using (conn) {
                try {
                    conn.Open();

                    string query = "UPDATE temporaryCartUser SET alreadyCheckout = 1 WHERE user_id = @user_id";
                    MySqlCommand cmd = new(query, conn);

                    using (cmd) {
                        cmd.Parameters.AddWithValue("@user_id", instanceAccount.getUserId(account));

                        object rows = cmd.ExecuteScalar();
                        int rowCount = Convert.ToInt32(rows);

                        return rowCount > 0;
                    }
                } catch (Exception ex) {
                    Console.WriteLine("Error updating checkout status: " + ex.Message);
                    return false;
                }
            }
        }

        public List<Inventory> getCheckoutCartFromDatabase() {
            MySqlConnection conn = DatabaseConnection.GetConnection();


            using (conn) {
                try {
                    conn.Open();

                    string query = "SELECT tempCart.drug_id AS drugID, inventory.drug_name AS drugName, inventory.manufacturer AS manufacturer, inventory.price AS price " +
                                   "FROM temporaryCartUser tempCart " +
                                   "LEFT JOIN drug_inventory inventory ON tempCart.drug_id = inventory.drug_id " +
                                   "WHERE user_id = @user_id && alreadyCheckout = 1";

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

        public bool insertIndividualRecordWithTransaction(string transactionID, List<Inventory>itemCart) {
            MySqlConnection conn = DatabaseConnection.GetConnection();

            using (conn) {
                try {
                    conn.Open();

                    string query = "INSERT INTO userCartTransaction (drug_id, transaction_id) VALUES (@drug_id, @transactionID)";
                    MySqlCommand cmd = new(query, conn);

                    using (cmd) {
                        foreach (var item in itemCart) {

                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@drug_id", item.DrugID);
                            cmd.Parameters.AddWithValue("@transaction_id", transactionID);

                            int rowsAffected = cmd.ExecuteNonQuery();

                            return rowsAffected > 0;
                        }     
                    }
                } 
                catch (Exception ex) {
                    Console.WriteLine("Error inserting checkout item: " + ex.Message);
                    return false;
                }
            }
            return false;
        }

        public void deleteCheckoutItemFromUserCart() {
            MySqlConnection conn = DatabaseConnection.GetConnection();

            using (conn) {
                try {
                    conn.Open();

                    string query = "DELETE FROM temporaryCartUser WHERE user_id = @user_id && alreadyCheckout = 1";
                    MySqlCommand cmd = new(query, conn);

                    using (cmd) {
                        cmd.Parameters.AddWithValue("@user_id", instanceAccount.getUserId(account));

                        cmd.ExecuteNonQuery();
                    }

                } catch (Exception ex) {
                    Console.WriteLine("Error while removing checkout items from cart: " + ex.Message);
                    return;
                }
            }
        }
    }
}
