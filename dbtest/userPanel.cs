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
            Inventory inventory = new();

            List<Inventory> inventories = inventory.getInventoryFromDatabase();

            foreach (var _inventory in inventories) {
                string commaExist = _inventory.DrugName.Contains(",") ? _inventory.DrugName.Substring(0, _inventory.DrugName.IndexOf(",")) : _inventory.DrugName;
                string displayDrugName = capitalizeEachFirstWord(commaExist);

                Console.WriteLine($"[{_inventory.DrugID}] Name: {displayDrugName} | Manufacturer: {_inventory.DrugManufacturer} | Price: {_inventory.DrugPrice}");
            }
        }

            public void userCart() {
                userAccount userAccount = new(account);
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
                List<Inventory> userCart = userAccount.isUserCartNotEmpty() ? userAccount.getCartUserFromDatabase() : new();

                int cartStorage = userCart.Count();

                if (userCart.Count == 0) {
                    Console.WriteLine("You have no items in the cart.");
                } else {
                    displayCartItems(userCart);
            }


            do {

                    Console.Write("Choose (1,2,3...[0 to exit]): ");
                    chooseCart = Convert.ToInt32(Console.ReadLine());

                    if (chooseCart == 0) {
                        displayCartItems(userCart);
                        break;
                    }

                    if (chooseCart < 0 || chooseCart > inventories.Count) {
                        Console.WriteLine("Your input is invalid");
                        continue;
                    }

                    userCart.Add(inventories[chooseCart - 1]);
                    Console.WriteLine($"You added {removeComma(inventories[chooseCart - 1].DrugName.ToString())} to your cart.");


                userAccount.putItemFromUserChoice(inventories[chooseCart - 1]);   
                
                } while (true);
                
            }
        
        public void checkout() {
            
        }

        //formality of the text
        public string capitalizeEachFirstWord(string input) {
            TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
            return textInfo.ToTitleCase(input);
        }

        public void displayCartItems(List<Inventory> userCart) {
            Console.WriteLine("Here is you existing items in the cart.");

            foreach (var itemCart in userCart) {
                string commaExist = itemCart.DrugName.Contains(",") ? itemCart.DrugName.Substring(0, itemCart.DrugName.IndexOf(",")) : itemCart.DrugName;
                string displayDrugName = capitalizeEachFirstWord(commaExist);

                Console.WriteLine($"[{itemCart.DrugID}] Name: {displayDrugName} | Manufacturer: {itemCart.DrugManufacturer} | Price: {itemCart.DrugPrice}");
            }
        }

        public string removeComma(string input) {
            string commaExist = input.Contains(",") ? input.Substring(0, input.IndexOf(",")) : input;
            return capitalizeEachFirstWord(commaExist);
        }
    }
}
