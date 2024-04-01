using System;

namespace LegacyApp;

public class Validator
{
    public bool ValidateName(string name, string lName)
    {
        return !(string.IsNullOrEmpty(name) || string.IsNullOrEmpty(lName));
    }

    public bool ValidateEmail(string email)
    {
        return email.Contains("@") && email.Contains(".");
    }

    public bool ValidateAge(DateTime dateOfBirth)
    {
        var now = DateTime.Now;
        int age = now.Year - dateOfBirth.Year;
        if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) age--;

        if (age < 21)
        {
            return false;
        }

        return true;
    }

    public bool ValidateUserCredit(User user)
    {
        return !(user.HasCreditLimit && user.CreditLimit < 500);
    }
    
}