using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace dbtest {
    class userValidation {
        public bool isEmailValid(string email) {
            string regex = "^[\\w\\.-]+@[a-zA-Z\\d-]+\\.[a-zA-Z]{2,}$"; // i dunno what shit is this

            return Regex.IsMatch(email, regex);
        }

        public bool isPasswordValid(string password) {
            string regex = @"^(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()_\-+=<>?{}~])[A-Za-z\d!@#$%^&*()_\-+=<>?{}~]{8,}$"; ///im cooked

            return Regex.IsMatch(password, regex);
        }
    }
}
