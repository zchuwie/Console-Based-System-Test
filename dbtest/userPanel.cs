using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Globalization;
using System.ComponentModel.Design;

namespace dbtest {
    class userPanel {
        public Account account { get; set; }
        
        
        public userPanel(Account acc) {
            this.account = acc;
            frontPage();
        }
        public void frontPage() {
            int choose;
            Console.Write($"Welcome {account.Username}. What do you want to do?\n" +
                $"[1]Dashboard\n[2]Check Available Medicines\n[3]Cart\n[4]Settings\n[5]Logout\n");

            Console.Write("Choose: ");
            choose = Convert.ToInt32(Console.ReadLine());

            switch (choose) {
                case 1:
                    break;
                case 2:
                    displayMedicines();
                    userCart();
                    checkout();
                    break;
                case 3:
                    break;
                case 4:
                    deleteAccount();
                    break;
                case 5:
                break;
            } 
        }

        public void deleteAccount() {
            string choose;
            Console.Write("Are you sure? (y/n): ");
            choose = Console.ReadLine().ToLower();

            if (choose == "y") {
                bool isDeleted = account.deleteAccount(account);

                if (isDeleted) { 
                    Console.WriteLine("Account has been deleted. Going back to main menu...");
                    Program.Main(new string[] { });
                    return;
                    
                } else {
                    Console.WriteLine("Error occurred while deleting...");
                }
            } else if(choose == "n") {
                Console.WriteLine("Going to the dashboard");
                return;
            } else {
                Console.WriteLine("I cant understand you.");
                return;
            }
        }

        public void displayMedicines() {
            Inventory inventory = new Inventory();

            List<Inventory> inventories = inventory.getInventoryFromDatabase();

            foreach (var _inventory in inventories) {
                string commaExist = _inventory.DrugName.Contains(",") ? _inventory.DrugName.Substring(0, _inventory.DrugName.IndexOf(",")) : _inventory.DrugName;
                string displayDrugName = capitalizeEachFirstWord(commaExist);

                Console.WriteLine($"[{_inventory.DrugID}] Name: {displayDrugName} | Manufacturer: {_inventory.DrugManufacturer} | Price: {_inventory.DrugPrice}");
            }
        }

        public void userCart() {
            Transaction transaction = new();
            Inventory inventory = new();

            string input;
            int chooseCart;

            Console.WriteLine("Do you want to order? (y/n): ");
            input = Console.ReadLine();

            if (input == "n") {
                Console.WriteLine("Alright! see you!");
                return;
            }

            if (input != "y") {
                Console.WriteLine("I cannot understand you. Sorry.");
                return;
            }

            List<Inventory> inventories = inventory.getInventoryFromDatabase();
            List<Inventory> userCart = new();
            userAccount userAccount = new(account);

            string transactionID = transaction.isTransactionIDActive() ? transaction.keepActiveTransactionID() : transaction.uniqueTransactionID();
            int cartStorage = userCart.Count();
            bool isActiveID = transaction.isTransactionIDActive();

            do {

                Console.Write("Choose (1,2,3...[0 to exit]): ");
                chooseCart = Convert.ToInt32(Console.ReadLine());

                if (chooseCart == 0) {
                    if (userCart != null && !isActiveID) {
                        transaction = new(account, transactionID);
                        transaction.insertIntoGeneralTransaction();
                        return;
                    } else {
                        Console.WriteLine("Empty cart");
                        return;
                    };
                }

                if (chooseCart > inventories.Count) {
                    Console.WriteLine("Your input is bigger than the list...");
                    continue;
                }

                userCart.Add(inventories[chooseCart - 1]);
                Console.WriteLine($"You added {userCart[cartStorage].DrugName} in your cart.");
                userAccount.getItemFromUserChoice(transactionID, userCart[cartStorage]);             
            } while (true);
        }
        
        public void checkout() {
            //the userCheckout method from userAccount will be placed here
        }

        //formality of the text
        public string capitalizeEachFirstWord(string input) {
            TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
            return textInfo.ToTitleCase(input);
        }
    }
}
