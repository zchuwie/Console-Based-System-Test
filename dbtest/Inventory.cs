using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace dbtest {
    class Inventory {
        public int DrugID { get; set; }
        public string DrugName { get; set; }
        public decimal DrugPrice { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string DrugManufacturer { get; set; }
        public bool PrescriptionNeeded { get; set; }
        public static List<Inventory> DatabaseInventory { get; set; } = new List<Inventory>();


        //this gets the whole inventory from the table inventory
        public List<Inventory> getInventoryFromDatabase() {
            MySqlConnection conn = DatabaseConnection.GetConnection();

            DatabaseInventory.Clear();

            using (conn) {
                try {
                    conn.Open();
                    string query = "SELECT * FROM drug_inventory";
                    MySqlCommand cmd = new(query, conn);

                    using (cmd) {
                        using (MySqlDataReader reader = cmd.ExecuteReader()) {
                            while (reader.Read()) {
                                Inventory inventory = new Inventory {
                                    DrugID = reader.GetInt32("drug_id"),
                                    DrugName = reader.GetString("drug_name").ToLower(),
                                    ExpirationDate = reader.GetDateTime("expiration_date"),
                                    DrugManufacturer = reader.GetString("manufacturer"),
                                    DrugPrice = reader.GetDecimal("price"),
                                    PrescriptionNeeded = reader.GetBoolean("prescription_needed")
                                };

                                DatabaseInventory.Add(inventory);
                            }
                        }
                    }
                } catch (Exception ex) {
                    Console.WriteLine("Filling up data error: " + ex.Message);
                }
            }
            return DatabaseInventory;
        }
    }
}