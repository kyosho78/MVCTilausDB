using MVCTilausDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace MVCTilausDB.ViewModels
{
    public class LoginsManager
    {
        public string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Muunnetaan hajautus byte-muodosta Base64-muotoon
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2")); // Muunnetaan jokainen bitti heksadesimaalimuotoon
                }
                return builder.ToString();
            }
        }

        // Metodi käyttäjän lisäämiseksi tietokantaan
        public void AddUser(string userName, string password)
        {
            string hashedPassword = HashPassword(password);

            // Lisätään käyttäjä tietokantaan
            using (TilausDBEntities db = new TilausDBEntities())
            {
                var login = new Logins
                {
                    UserName = userName,
                    PassWord = hashedPassword,
                    RegisterationDate = DateTime.Now
                };

                try
                {
                    db.Logins.Add(login);
                    db.SaveChanges();
                }

                catch (Exception ex)
                {
                    // Tässä voit lisätä tietueen lokitietoihin tai tulostaa virheilmoituksen konsoliin
                    Console.WriteLine("Virhe tallennettaessa tietokantaan: " + ex.Message);
                    throw; // Heitä edelleen poikkeus, jotta MVC voi käsitellä sen
                }
            }
        }

    }
}