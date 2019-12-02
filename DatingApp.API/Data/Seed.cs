using System.Collections.Generic;
using System.Linq;
using DatingApp.API.Models;
using Newtonsoft.Json;

namespace DatingApp.API.Data
{
    public class Seed
    {
        // Requires DbContext
        public static void SeedUsers(DataContext context) 
        {
            // Check is Database empty
            if (!context.Users.Any())
            {
                // Read data json file
                var userData = System.IO.File.ReadAllText("Data/UserSeedData.json");
                // Parse them into list of user object
                var Users = JsonConvert.DeserializeObject<List<User>>(userData);
                // loop through users to set password
                foreach (var user in Users) {
                    byte[] passwordHash, passwordSalt;
                    CreatePasswordHash("password", out passwordHash, out passwordSalt);
                    user.PasswordHash = passwordHash;
                    user.PasswordSalt = passwordSalt;
                    user.Username = user.Username.ToLower();
                    context.Users.Add(user);
                }
                context.SaveChanges();
            }
        }

        
        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                // ComputeHash takes a byte array as argument
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
            
        }
    }
}