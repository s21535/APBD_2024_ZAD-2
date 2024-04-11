using System;

namespace LegacyApp
{
    public class User
    {

        private string _firstName;
        private string _lastName;

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
        
        public object Client { get; internal set; }
        public DateTime DateOfBirth { get; internal set; }
        public string EmailAddress { get; internal set; }
        //public string FirstName { get; internal set; }
        //public string LastName { get; internal set; }
        public bool HasCreditLimit { get; internal set; }
        public int CreditLimit { get; internal set; }
    }
}