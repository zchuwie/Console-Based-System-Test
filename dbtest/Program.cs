using System;
using System.Security.Cryptography.X509Certificates;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.Esf;

namespace dbtest { 
    class Program {
        Account account = new();

        public static void Main(string[] args) {
            Program p = new();
            int choose;
            Console.WriteLine("Welcome to Test System!\n[1]Login\n[2]SignUp\n[3]Hash\n[4]Forget Password\n[5]Exit");
            Console.Write("Choose: ");
            choose = int.Parse(Console.ReadLine());

            switch (choose) {
                case 1: 
                    p.userLogin();
                    break;
                case 2:
                    p.userSignUp();
                    break;
                case 3:
                    p.testHash();
                    break;
                case 4:
                    p.forgetPassword();
                    break;
                case 5:
                    return;
                default:
                    Console.WriteLine("You chose wtf");
                    break;
            }          
        }

        private void userLogin() {            
            string username, password;
            Console.Write("Enter your username: ");
            username = Console.ReadLine();

            Console.Write("Enter your password: ");
            password = Console.ReadLine();

            HashedPassword hp = new(password);

            string hashedPass = hp.hashCombinedDisplay;

            account.Username = username.Trim();
            account.Password = hashedPass;

            string usernameDB = account.login(account);

            if (usernameDB == "admin") _ = new adminPanel(account);
            else if (usernameDB == username) _ = new userPanel(account);           
            else Console.WriteLine("Username or password incorrect");
            
        }

        private void userSignUp() {
            string username, password, email, answer;
            int ask = 0;

            do {
                Console.Write("Enter your username: ");
                username = Console.ReadLine();

                Console.Write("Enter your email: ");
                email = Console.ReadLine();

                Console.Write("Enter your password (min.8, with single of the ff: [number, caps, and symbol]): ");
                password = Console.ReadLine();

                if (!String.IsNullOrEmpty(username) && !String.IsNullOrEmpty(email) && !String.IsNullOrEmpty(password)) {
                    userValidation valid = new();

                    account.Username = username.ToLower();

                    bool isDuplicate = account.checkDuplicate(account);

                    if (isDuplicate) {
                        Console.WriteLine("Someone already have that username");
                        continue;
                    }
                    else {
                        bool validatedEmail = valid.isEmailValid(email);
                        bool validatedPass = valid.isPasswordValid(password);

                        if (!validatedEmail) {
                            Console.WriteLine("Invalid Email. Try again.");
                            continue;
                        }

                        if (!validatedPass) {
                            Console.WriteLine("Invalid Password. Try again.");
                            continue;
                        }

                        string hashedPass = new HashedPassword(password).hashCombinedDisplay;

                        account.Email = email.Trim();
                        account.Password = hashedPass;

                        if (account.Signup(account)) {
                            Console.WriteLine("Account has been created successfully.");
                            return;
                        } else {
                            Console.WriteLine("An error occurred during sign up. Please try again.");
                        }
                    }
                } 
                else {
                    Console.WriteLine("Please complete your information");
                    ask++;

                    if (ask == 3) {
                        Console.Write("Do you still want to sign up? (y/n): ");
                        answer = Console.ReadLine().ToLower();

                        if (answer == "n") {
                            return;
                        }
                    }
                }
            } while (true);
        }

        private void forgetPassword() {
            string acc, newPass, confirmPass;

            Console.Write("Enter your email: ");
            acc = Console.ReadLine();

            account.Email = acc.ToLower();

            bool isExisting = account.accountExist(account);

            if (!isExisting) {
                Console.WriteLine("This account doesn't exist...");
                return;
            }

            Console.Write("Enter your new password: ");
            newPass = Console.ReadLine();

            Console.Write("Enter again: ");
            confirmPass = Console.ReadLine();

            bool isPasswordGreat = new userValidation().isPasswordValid(newPass);

            if (isPasswordGreat != true) {
                Console.WriteLine("Your password is weak.");
                return;
            }

            if (newPass != confirmPass) {
                Console.WriteLine("Your passwords don't match.");
                return;
            }

            account.Password = new HashedPassword(newPass).hashCombinedDisplay;

            if (account.forgetPassword(account)) {
                Console.WriteLine("You have successfully reset your password.");
            }
        }

        private void testHash() {
            string password;

            do {
                Console.Write("Enter password (or type 'n' to exit): ");
                password = Console.ReadLine();

                if (password == "n") {
                    Console.WriteLine("Exiting...");
                    break;
                }
                HashedPassword hash = new(password);
                

            } while (true);
        }
    }
}
