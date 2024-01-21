using System.Security.Cryptography;
using System.Text;

namespace MetaClothes.Tools
{
    public class Password
    {

        public static string hashpassword(string password)
        {

            var sha = SHA256.Create();
            var asByteArray = Encoding.Default.GetBytes(password);
            var hashedpassword = sha.ComputeHash(asByteArray);
            return Convert.ToBase64String(hashedpassword);



        }



    }
}
