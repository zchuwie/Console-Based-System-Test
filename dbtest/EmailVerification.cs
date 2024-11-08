using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;


namespace dbtest {
    class EmailVerification {
        Account account;
        public EmailVerification(Account acc)
        {
            this.account = acc;
        }

        public string sendVerificationCode() {
            Random random = new Random();
            string verificationCode = random.Next(100000, 999999).ToString();

            MailMessage message = new MailMessage();
            message.From = new MailAddress($"{Environment.GetEnvironmentVariable("BUTIKA_EMAIL")}");
            message.To.Add(account.Email);
            message.Subject = "Your Verification Code";

            //Well-formatted email
            string emailBody = $@"
        <html>
        <body style='font-family: Arial, sans-serif; line-height: 1.6;'>
            <h2 style='color: #333;'>Verification Code</h2>
            <p>Dear user,</p>
            <p>Here is your verification code:</p>
            <p style='font-size: 24px; font-weight: bold; color: #2d89ef;'>{verificationCode}</p>
            <p>Please enter this code to verify your email address. If you did not request this, please ignore this email.</p>
            <p>We hope you all the best, <br> Buti-ka </p>
        </body>
        </html>";

            message.Body = emailBody;
            message.IsBodyHtml = true; 


            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com") {
                Port = 587,
                Credentials = new NetworkCredential($"{Environment.GetEnvironmentVariable("BUTIKA_EMAIL")}", $"{Environment.GetEnvironmentVariable("BUTIKA_APP_PASS")}"), 
                EnableSsl = true, 
            };

            try {
                smtpClient.Send(message);
                Console.WriteLine("Verification code sent successfully.");
                return verificationCode; 
            } catch (Exception ex) {
                Console.WriteLine("Error sending email: " + ex.Message);
                return null;
            }
        }
    }
}
