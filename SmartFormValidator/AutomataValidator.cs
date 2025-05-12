using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFormValidator
{
    public static class AutomataValidator
    {
        public static bool ValidateUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username)) return false;

            if (username.Length < 3 || username.Length > 20) return false;

            // Tidak boleh diawali atau diakhiri titik/underscore
            if (username.StartsWith(".") || username.StartsWith("_") ||
                username.EndsWith(".") || username.EndsWith("_")) return false;

            // Validasi karakter satu per satu
            char lastChar = '\0';
            foreach (char c in username)
            {
                if (!(char.IsLetterOrDigit(c) || c == '_' || c == '.'))
                    return false;

                // Tidak boleh dua karakter spesial berturut-turut
                if ((c == '.' || c == '_') && (lastChar == '.' || lastChar == '_'))
                    return false;

                lastChar = c;
            }

            return true;
        }

        public static bool ValidateEmail(string input)
        {
            string state = "start";

            foreach (char c in input)
            {
                switch (state)
                {
                    case "start":
                        state = Char.IsLetterOrDigit(c) ? "local" : "error";
                        break;
                    case "local":
                        if (c == '@') state = "at";
                        else if (Char.IsLetterOrDigit(c) || c == '.') state = "local";
                        else state = "error";
                        break;
                    case "at":
                        state = Char.IsLetterOrDigit(c) ? "domain" : "error";
                        break;
                    case "domain":
                        if (c == '.') state = "dot";
                        else if (Char.IsLetterOrDigit(c)) state = "domain";
                        else state = "error";
                        break;
                    case "dot":
                        state = Char.IsLetter(c) ? "tld" : "error";
                        break;
                    case "tld":
                        state = Char.IsLetter(c) ? "tld" : "error";
                        break;
                    default:
                        return false;
                }

                if (state == "error") return false;
            }

            return state == "tld";
        }
    }
}