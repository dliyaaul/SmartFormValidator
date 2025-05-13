using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class SmartFormValidator<T>
{
    private List<string> _errors;

    public SmartFormValidator()
    {
        _errors = new List<string>();
    }

    public bool Validate(T formObject)
    {
        if (formObject == null)
        {
            _errors.Add("Form is null");
            return false;
        }

        if (formObject is Dictionary<string, string> dictionary)
        {
            // Validasi Email
            if (!dictionary.ContainsKey("Email"))
            {
                _errors.Add("Email is required.");
            }
            else
            {
                string email = dictionary["Email"] as string;
                if (string.IsNullOrWhiteSpace(email))
                {
                    _errors.Add("Email cannot be empty.");
                }
                else if (!IsValidEmail(email))
                {
                    _errors.Add("Invalid email format.");
                }
            }

            // Validasi Username
            if (!dictionary.ContainsKey("Username"))
            {
                _errors.Add("Username is required.");
            }
            else
            {
                string username = dictionary["Username"] as string;
                if (string.IsNullOrWhiteSpace(username))
                {
                    _errors.Add("Username cannot be empty.");
                }
                else if (username.Length < 5 || username.Length > 15)
                {
                    _errors.Add("Username must be between 5 and 15 characters.");
                }
                else if (!Regex.IsMatch(username, @"^[a-zA-Z0-9]+$"))
                {
                    _errors.Add("Username must contain only letters and numbers.");
                }
            }

            // Valiadsi Password
            if (!dictionary.ContainsKey("Password"))
            {
                _errors.Add("Password is required.");
            }
            else
            {
                string password = dictionary["Password"] as string;
                if (string.IsNullOrWhiteSpace(password))
                {
                    _errors.Add("Password cannot be empty.");
                }
                else if (password.Length < 8)
                {
                    _errors.Add("Password must be at least 8 characters long.");
                }
                else if (!Regex.IsMatch(password, @"[A-Z]"))
                {
                    _errors.Add("Password must contain at least one uppercase letter.");
                }
                else if (!Regex.IsMatch(password, @"[a-z]"))
                {
                    _errors.Add("Password must contain at least one lowercase letter.");
                }
                else if (!Regex.IsMatch(password, @"[0-9]"))
                {
                    _errors.Add("Password must contain at least one digit.");
                }
                else if (!Regex.IsMatch(password, @"[\W_]"))
                {
                    _errors.Add("Password must contain at least one special character (e.g., @, #, $, etc.).");
                }
            }
        }

        return _errors.Count == 0;
    }

    public List<string> GetErrors()
    {
        return _errors;
    }

    private bool IsValidEmail(string email)
    {
        string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        return Regex.IsMatch(email, pattern);
    }
}
