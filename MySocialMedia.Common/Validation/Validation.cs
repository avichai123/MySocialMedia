using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MySocialMedia.Common.Validation
{
    public class Validation
    {
        public static string ValidUserName(string userName)
        {
            string ok = "user name not valid";
            if (userName.Length < 5 || userName.Length > 15)
            {
                return ok;
            }
            if (!char.IsLetter(userName[0]))
            {
                return ok;
            }
            Regex regex = new Regex(@"^[a-zA-Z0-9_-]+$");
            if (!regex.IsMatch(userName))
            {
                return ok;
            }
            if (userName.Contains("__") || userName.Contains("--"))
            {
                return ok;
            }

            return null;

        }
        public static string ValidPassword(string password)
        {
            string ok = "password not valid";
            if (password.Length < 8)
            {
                return ok;
            }
            if (!password.Any(char.IsUpper))
            {
                return ok;
            }
            if (!password.Any(char.IsLower))
            {
                return ok;
            }
            if (!password.Any(char.IsDigit))
            {
                return ok;
            }
            Regex specialCharRegex = new Regex(@"[!@#$%^&*(),.?\:{ }|<>]");
            if (!specialCharRegex.IsMatch(password))
            {
                return ok;
            }
            if (password.Contains(" "))
            {
                return ok;
            }
            return null;
        }

        public static string ValidName(string firstName)
        {
            string ok = "name not valid";
            if (firstName.Length < 2 || firstName.Length > 50)
            {
                return ok;
            }
            Regex regex = new Regex(@"^[a-zA-Zא-ת ]+$");
            if (!regex.IsMatch(firstName))
            {
                return ok;
            }
            if (char.IsWhiteSpace(firstName[0]) || char.IsWhiteSpace(firstName[firstName.Length - 1]))
            {
                return ok;
            }
            if (firstName.Contains("  "))
            {
                return ok;
            }
            return null;
        }
        public static List<string> ValidationAll(string firstName, string lastName, string username , string password)
        {
            List<string> result = new List<string>();
            string userN = ValidUserName(username);
            string pass = ValidPassword(password);
            string firstN = ValidName(firstName);
            string lastN = ValidName(lastName);
            if (userN != null )
            {
                result.Add(userN);
            }
            if(pass != null )
            {
                result.Add(pass);
            }
            if(firstN != null )
            {
                result.Add("first " + firstN);
            }
            if(lastN != null )
            {
                result.Add("last " + lastN);
            }
            return result;  
        }
    }
}
