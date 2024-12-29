using System.Net.Mail;
using System.Net;

public class SendMail
{
    public async Task SendEmailAsync(string from, string toEmail, string subject, string body)
    {
        MailMessage message = new MailMessage(from, toEmail, subject, body);
        message.BodyEncoding = System.Text.Encoding.UTF8;
        message.SubjectEncoding = System.Text.Encoding.UTF8;
        message.IsBodyHtml = true;
        message.ReplyToList.Add(new MailAddress(from));

        // Cấu hình SMTP client với Gmail
        using var smtpClient = new SmtpClient("smtp.gmail.com", 587)
        {
            Credentials = new NetworkCredential("taquangthang2002hn@gmail.com", "pecz zpbb enzn sgbh"),
            EnableSsl = true
        };

        try
        {
            await smtpClient.SendMailAsync(message);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}