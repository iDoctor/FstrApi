using System.Text.RegularExpressions;

namespace FstrApi
{
    public class Validation
    {
        public string ValidateEmail(string email)
        {
            string result = string.Empty;

            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                result = string.Empty;
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                if (addr.Address == trimmedEmail)
                    result = trimmedEmail;
            }
            catch
            {
                result = string.Empty;
            }

            return result;
        }

        public string ValidatePhone(string phone)
        {
            var regexResult = Regex.Match(phone, @"\d+");

            if (regexResult.Success)
                return regexResult.Value;
            else
                return string.Empty;
        }
    }
}
