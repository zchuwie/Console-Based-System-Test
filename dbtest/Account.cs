using MySql.Data.MySqlClient;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace dbtest {
    class Account {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public Account() { }        

        public string login(Account acc) {
            MySqlConnection conn = DatabaseConnection.GetConnection();
            string username = null;
            using (conn) {
                try {
                    conn.Open();

                    string query = "SELECT username FROM userAccount WHERE username = @username && password = @password";
                    MySqlCommand cmd = new(query, conn);

                    using (cmd) {
                        cmd.Parameters.AddWithValue("@username", acc.Username);
                        cmd.Parameters.AddWithValue("@password", acc.Password);
                        object result = cmd.ExecuteScalar();

                        if (result != null) {
                            username = (string)result;
                        }
                    }                   
                } 
                catch (Exception ex) {
                    Console.WriteLine("Login error: " + ex.Message);
                }
            }
            return username;
        }

        public bool Signup(Account acc) {
            MySqlConnection conn = DatabaseConnection.GetConnection();

            if (acc == null) {
                return false;
            }

            bool isDuplicate = checkDuplicate(acc);

            if (isDuplicate) {
                return false;
            }

            using (conn) {
                try {
                    conn.Open();

                    HashedPassword hp = new(acc.Password);

                    string query = "INSERT INTO useraccount (username, email, password) VALUES (@username, @email, @password)";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn)) {
                        cmd.Parameters.AddWithValue("@username", acc.Username);
                        cmd.Parameters.AddWithValue("@email", acc.Email);
                        cmd.Parameters.AddWithValue("@password", acc.Password); //this was already hashed from Main method

                        int rows = cmd.ExecuteNonQuery();

                        if (rows > 0) {

                            int userID = getUserId(acc);

                            string query2 = "INSERT INTO hashedPassword (hashSalt, hashPassword, user_id) VALUES (@hashSalt, @hashPassword, @user_id)";
                            using (MySqlCommand cmd2 = new MySqlCommand(query2, conn)) {
                                cmd2.Parameters.AddWithValue("@hashSalt", hp.hashSaltDisplay);
                                cmd2.Parameters.AddWithValue("@hashPassword", hp.hashPasswordDisplay);
                                cmd2.Parameters.AddWithValue("@user_id", userID);

                                int rows2 = cmd2.ExecuteNonQuery();

                                return rows2 > 0; 
                            }
                        } else {
                            return false; 
                        }
                    }
                } catch (MySqlException ex) {
                    Console.WriteLine("MySQL Error: " + ex.Message);
                    return false; 
                } catch (Exception ex) {
                    Console.WriteLine("Signup Error: " + ex.Message);
                    return false; 
                }
            }
        }

        public bool checkDuplicate(Account acc) {
            MySqlConnection conn = DatabaseConnection.GetConnection();
            if (acc == null) {
                return false;
            }

            using (conn) {
                try {
                    conn.Open();

                    string query = "SELECT username FROM userAccount WHERE username = @username";
                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    using (cmd) {
                        cmd.Parameters.AddWithValue("@username", acc.Username);

                        object result = cmd.ExecuteScalar();

                        if (result != null) {
                            return true;
                        } else return false;
                    }
                } 
                catch (Exception ex) {
                    Console.WriteLine("Check duplication error: " + ex.Message);
                    return false;
                }
            }     
        }

        public bool accountExist(Account acc) {
            MySqlConnection conn = DatabaseConnection.GetConnection();
            if (acc == null) {
                return false;
            }

            using (conn) {
                try {
                    conn.Open();

                    string query = "SELECT username FROM userAccount WHERE email = @email";
                    MySqlCommand cmd = new MySqlCommand( query, conn);
                    using(cmd) {
                        cmd.Parameters.AddWithValue("@email", acc.Email);

                        object result = cmd.ExecuteScalar();

                        if (result != null) {
                            return true;
                        } else {
                            return false;
                        }
                    }                   
                } 
                catch (Exception ex) {
                    Console.WriteLine("account Existion error: " + ex.Message);
                    return false;
                }
            }
        }

        public bool forgetPassword(Account acc) {
            MySqlConnection conn = DatabaseConnection.GetConnection();

            using (conn) {
                try {
                    conn.Open();

                    string query = "UPDATE userAccount SET password = @password WHERE email = @email";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn)) {
                        cmd.Parameters.AddWithValue("@password", acc.Password);
                        cmd.Parameters.AddWithValue("@email", acc.Email);

                        int rows = cmd.ExecuteNonQuery();

                        return rows > 0;
                    }

                }
                catch (Exception ex) {
                    Console.WriteLine("Reset password error: " + ex.Message);
                    return false;
                }
            }
        }

        public int getUserId(Account acc) {
            MySqlConnection conn = DatabaseConnection.GetConnection();
            using (conn) {
                try {
                    conn.Open();

                    string query = "SELECT user_id FROM userAccount WHERE username = @username";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    using (cmd) {
                        cmd.Parameters.AddWithValue("@username", acc.Username);

                        object result = cmd.ExecuteScalar();

                        if (result != null) {
                            return Convert.ToInt32(result);
                        } else {
                            return 0;
                        }
                    }
                } catch (Exception ex) {
                    Console.WriteLine("Get userID error: " + ex.Message);
                    return 0;
                }
            }
        }

        public bool deleteAccount(Account acc) {
            MySqlConnection conn = DatabaseConnection.GetConnection();

            using (conn) {
                try {
                    conn.Open();

                    int userID = getUserId(acc);

                    string query = "DELETE user, hash FROM userAccount AS user JOIN hashedPassword AS hash ON hash.user_id = user.user_id WHERE user.user_id = @userID";
                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    using (cmd) {
                        cmd.Parameters.AddWithValue("@userID", userID);

                        int rows = cmd.ExecuteNonQuery();
                        return rows > 0;
                    }
                } 
                catch (Exception ex) {
                    Console.WriteLine("Deletion error: " + ex.Message);
                    return false;
                }
            }
            return false;
        }
    }
}
