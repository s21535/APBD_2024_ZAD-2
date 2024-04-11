using System;

namespace LegacyApp
{
    public class UserService {
        private IClientRepository _clientRepository;
        private ICreditService _creditService;
        private User user;
        
        public UserService() {
            _clientRepository = new ClientRepository();
            _creditService = new UserCreditService();
            user = new User();
        }
        
        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId) {
            //BL - validation
            /*if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName)) {
                return false;
            }*/

            //BL - validation
            if (!email.Contains("@") && !email.Contains(".")) {
                return false;
            }
            
            //BL
            var now = DateTime.Now;
            int age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) age--;
            
            if (age < 21) {
                return false;
            }

            //Infrastructure
            var client = _clientRepository.GetById(clientId);

            
            try
            {
                {
                    /*Client = client,
                    DateOfBirth = dateOfBirth,
                    EmailAddress = email,*/
                    user.FirstName = firstName;
                    user.LastName = lastName;
                };
            }
            catch (ArgumentException e)
            {
                return false;
            }

            //BL + Infrastructure
            if (client.Type == "VeryImportantClient") {
                user.HasCreditLimit = false;
            }
            else if (client.Type == "ImportantClient") {
                int creditLimit = _creditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                creditLimit = creditLimit * 2;
                user.CreditLimit = creditLimit;
            }
            else {
                user.HasCreditLimit = true;
                int creditLimit = _creditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                user.CreditLimit = creditLimit;
            }
            
            //BL - validation
            if (user.HasCreditLimit && user.CreditLimit < 500) {
                return false;
            }

            //Infrastructure
            UserDataAccess.AddUser(user);
            return true;
        }
    }
}
