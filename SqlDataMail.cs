using System;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;

namespace OtomatikMail
{
    class SqlDataMail
    {
        static void Main(string[] args)
        {
            try
            {
                string cs = @"Data Source=LAPTOP-08AMRNF7\SSA2200005204; database=FoodDB; integrated security=true";
                string sql = " SELECT CategoryName, CategoryDescription FROM Categories";

                DataTable dt = new DataTable();

                using (SqlConnection conn = new SqlConnection(cs))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(sql, conn))
                    {
                        sda.Fill(dt);
                    }
                }

                if (dt.Rows.Count > 0)
                {
                    string mailBody = "📌 Kategori Listesi: \n\n";
                    foreach (DataRow item in dt.Rows)
                    {
                        mailBody += $"{item["CategoryName"]}: {item["CategoryDescription"]}\n";
                    }

                    MailGonder(mailBody);
                }
                else
                {
                    Console.WriteLine("⚠️ Kategoriler tablosu boş!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Hata oluştu: {ex.Message}");
            }
        }

        private static void MailGonder(string mailBody)
        {
            try
            {
                MailMessage ePosta = new MailMessage
                {
                    From = new MailAddress("salimaka2014@gmail.com"),
                    Subject = "Son Siparişler",
                    Body = mailBody,
                    IsBodyHtml = false // HTML olarak göndermek istersen true yap
                };

                ePosta.To.Add("lestoltenberg2023@gmail.com");

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.Credentials = new NetworkCredential("salimaka2014@gmail.com", "uipf ijlh wosw nrmo");
                    smtp.EnableSsl = true;
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
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
