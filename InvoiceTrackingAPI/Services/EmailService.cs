using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading;
using System.ComponentModel;

namespace InvoiceTrackingAPI.Services
{
    public class EmailService
    { 
        
        public void SendMail(string e_to, string e_reason)
        {
            SmtpClient client = new SmtpClient("smtp.gmail.com");
            MailAddress from = new MailAddress("davemixstair@gmail.com", "Dave " + (char)0xD8 + " Napo", System.Text.Encoding.UTF8);
            MailAddress to = new MailAddress(e_to);

            MailMessage message = new MailMessage(from, to);

            client.Port = 25;


            message.Body = "Invoice rejected for the below reasons: \n\n";
            string someArrows = new string(new char[] { '\u2190', '\u2191', '\u2192', '\u2193' });
            //message.Body += Environment.NewLine + someArrows;
            message.Body += e_reason;
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.Subject = "test message 1" + someArrows;
            message.SubjectEncoding = System.Text.Encoding.UTF8;

            client.Credentials = new System.Net.NetworkCredential("davemixstair@gmail.com", "accountPass@92");
            client.EnableSsl = true;
            client.Send(message);

            //client.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);

            //string userState = "test message1";
            //client.SendAsync(message, userState);
            /*Console.WriteLine("Sending message... press c to cancel mail. Press any other key to exit.");
            string answer = Console.ReadLine();

            if (answer.StartsWith("c") && mailSent == false)
            {
                client.SendAsyncCancel();
            }
            // Clean up.*/
            message.Dispose();


        }

        static bool mailSent = false;
        private static void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            // Get the unique identifier for this asynchronous operation.
            String token = (string)e.UserState;

            if (e.Cancelled)
            {
                Console.WriteLine("[{0}] Send canceled.", token);
            }
            if (e.Error != null)
            {
                Console.WriteLine("[{0}] {1}", token, e.Error.ToString());
            }
            else
            {
                Console.WriteLine("Message sent.");
            }
            mailSent = true;
        }


    }
}