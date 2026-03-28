using System.Text;

namespace mahadalzahrawebapi.Services
{
    public class PasswordGenerator
    {

        private const string Uppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string Lowercase = "abcdefghijklmnopqrstuvwxyz";
        private const string Digits = "0123456789";
        private const string SpecialChars = "!@#$%&*";

        public string GeneratePassword(int length = 10)
        {
            if (length < 4)
            {
                throw new ArgumentException("Password length should be at least 4 characters.");
            }

            StringBuilder password = new StringBuilder();
            Random random = new Random();

            // Ensure password contains at least one character of each type
            password.Append(Uppercase[random.Next(Uppercase.Length)]);
            password.Append(Lowercase[random.Next(Lowercase.Length)]);
            password.Append(Digits[random.Next(Digits.Length)]);
            password.Append(SpecialChars[random.Next(SpecialChars.Length)]);

            // Fill the rest of the password length
            string allChars = Uppercase + Lowercase + Digits + SpecialChars;
            for (int i = 4; i < length; i++)
            {
                password.Append(allChars[random.Next(allChars.Length)]);
            }

            return ShufflePassword(password.ToString());
        }

        private string ShufflePassword(string password)
        {
            char[] array = password.ToCharArray();
            Random rng = new Random();
            int n = array.Length;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                var value = array[k];
                array[k] = array[n];
                array[n] = value;
            }
            return new string(array);
        }
    }


}
