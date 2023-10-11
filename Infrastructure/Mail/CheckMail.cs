using System.Text.RegularExpressions;

namespace Infrastructure.Mail
{
    public static class CheckMail
    {
        public static bool IsEmail(string email)
        {
            // Define a regular expression pattern for email addresses
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

            // Create a Regex object with the pattern
            Regex regex = new Regex(pattern);

            // Use the Regex object to match the input email address against the pattern
            Match match = regex.Match(email);

            // Return true if the input email address matches the pattern, false otherwise
            return match.Success;
        }
    }
}
