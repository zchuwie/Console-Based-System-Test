using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dbtest {
    class adminPanel {
        public Account account;

        public adminPanel(Account acc) {
            this.account = acc;
            frontPage();
        }
        public void frontPage() {
            int choose;
            Console.Write($"Welcome {account.Username}. What do you want to do?\n" +
                $"[1]Insert Medicine\n[2]Check Available Medicines\n[3]Check Users\n[4]Settings\n[5]Logout\n");

            Console.Write("Choose: ");
            choose = Convert.ToInt32(Console.ReadLine());

            switch (choose) {
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
            }
        }
    }
}
