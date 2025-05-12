using System;
using System.Collections.Generic;
namespace SmartFormValidator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Smart Form Validator (Automata + Generics) ===\n");

            // Automata: Validasi username
            string username = "muhammaddliyak";
            bool isUsernameValid = AutomataValidator.ValidateUsername(username);
            Console.WriteLine($"Username valid? {isUsernameValid}");

            // Automata: Validasi email
            string email = "user@example.com";
            bool isEmailValid = AutomataValidator.ValidateEmail(email);
            Console.WriteLine($"Email valid? {isEmailValid}");

            // Generics: Validasi password
            var passwordRules = new List<Func<string, bool>>()
            {
                GenericValidator.IsNotEmpty,
                GenericValidator.MinLength(8),
                GenericValidator.ContainsDigit
            };

            string password = "Password123";
            bool isPasswordValid = GenericValidator.Validate(password, passwordRules);
            Console.WriteLine($"Password valid? {isPasswordValid}");
        }
    }
}