using System;
using System.Net;
using System.Net.Mail;

namespace OtomatikMail
{
    class SqlDataMail
    {
        static void Main(string[] args)
        {
            Console.Write("Göndermek istediğiniz mesajı yazın: ");
            string mailBody = Console.ReadLine();


            // Mail gönderme fonksiyonunu çağır
            MailGonder(mailBody);
        }

        private static void MailGonder(string mailBody)
        {
            try
            {
                MailMessage ePosta = new MailMessage
                {
                    From = new MailAddress("your-address@gmail.com"),
                    Subject = "Özel Mesaj",
                    Body = mailBody,
                    IsBodyHtml = false // Eğer HTML formatlı mesaj istersen true yapabilirsin
                };

                ePosta.To.Add("target-address@gmail.com");

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.Credentials = new NetworkCredential("your-address@gmail.com", "password");
                    smtp.EnableSsl = true;
                    smtp.Send(ePosta);
                }

                Console.WriteLine("✅ Mail başarıyla gönderildi!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"📧 Mail gönderme hatası: {ex.Message}");
            }
        }
    }
}
