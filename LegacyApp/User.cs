using System;

namespace LegacyApp
{
    public class User
    {

        private string _firstName;
        private string _lastName;
        private DateTime _dateOfBirth;
        private string _emailAddress;

        public string FirstName 
        {
            get
            {
                return _firstName;
            }
            set
            {
                if (string.IsNullOrEmpty(value)) {
                    throw new ArgumentException($"Trying to set empty value as First Name");
                }
                else
                {
                    _firstName = value;
                }
            }
        }
        public string LastName 
        {
            get
            {
                return _lastName;
            }
            set
            {
                if (string.IsNullOrEmpty(value)) {
                    throw new ArgumentException($"Trying to set empty value as Last Name");
                }
                else
                {
                    _lastName = value;
                }
            }
        }
        public DateTime DateOfBirth 
        {
            get
            {
                return _dateOfBirth;
            }
            set
            {
                var now = DateTime.Now;
                int age = now.Year - value.Year;
                if (now.Month < value.Month || (now.Month == value.Month && now.Day < value.Day)) age--;
                if (age < 21)
                {
                    throw new ArgumentException($"User's age less than 21");
                }
            }
        }
        public string EmailAddress
        {
            get
            {
                return _emailAddress;
            }
            set
            {
                if (!value.Contains("@") && !value.Contains(".")) {
                    throw new ArgumentException($"Email Adress value is incorrect");
                }
                else
                {
                    _emailAddress = value;
                }
            }
        }
        public object Client { get; internal set; }
        public bool HasCreditLimit { get; internal set; }
        public int CreditLimit { get; internal set; }
    }
}