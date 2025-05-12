using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFormValidator
{
    public static class GenericValidator
    {
        public static bool Validate<T>(T input, List<Func<T, bool>> rules)
        {
            foreach (var rule in rules)
            {
                if (!rule(input)) return false;
            }
            return true;
        }

        public static bool IsNotEmpty(string input) => !string.IsNullOrWhiteSpace(input);

        public static Func<string, bool> MinLength(int length)
            => input => input.Length >= length;

        public static bool ContainsDigit(string input)
            => input.Any(char.IsDigit);
    }
}
