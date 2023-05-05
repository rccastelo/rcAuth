using System;
using System.Text.RegularExpressions;

namespace rcUtils
{
    public static class Validations
    {
        public static bool ValidateChars(string text)
        {
            try {
                Regex regex = new Regex(@"^[A-Z0-9_@#$&.-]+$", 
                    RegexOptions.IgnoreCase, 
                    TimeSpan.FromMilliseconds(200));

                return regex.IsMatch(text);
            } catch (RegexMatchTimeoutException) {
                return false;
            }
        }
    }
}
