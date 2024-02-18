using System;
using System.Net.Mail;
using System.Net;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace Notion_to_Html
{
    public class Function
    {
        [FunctionName("Function")]
        public void Run([TimerTrigger("0 */5 * * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            // SMTP server settings
            string smtpServer = "smtp.example.com";
            int smtpPort = 587; // Update with the appropriate port
            string smtpUsername = "your-smtp-username";
            string smtpPassword = "your-smtp-password";

            // Sender and recipient email addresses
            string fromEmail = "yuva@motionbee.com";
            string toEmail = "prajwalsrangegowda@gmail.com";

            // Create an SMTP client
            SmtpClient client = new SmtpClient(smtpServer, smtpPort)
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(smtpUsername, smtpPassword),
                EnableSsl = true // Set to true if your SMTP server requires SSL
            };

            // Create and send the email message
            MailMessage message = new MailMessage(fromEmail, toEmail)
            {
                Subject = "Test Email",
                Body = "This is a test email sent from Azure Functions."
            };

            try
            {
                client.Send(message);
                log.LogInformation("Email sent successfully.");
            }
            catch (Exception ex)
            {
                log.LogError($"Failed to send email: {ex.Message}");
            }
            finally
            {
                // Dispose of the SMTP client
                client.Dispose();
            }
        }
    }
}
