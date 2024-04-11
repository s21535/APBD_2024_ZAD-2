using System;

namespace LegacyApp
{
    public class UserService {
        private IClientRepository _clientRepository;
        private ICreditService _creditService;
        private User _user;
        
        public UserService() {
            _clientRepository = new ClientRepository();
            _creditService = new UserCreditService();
            _user = new User();
        }
        
        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId) {
            
            //Infrastructure
            var client = _clientRepository.GetById(clientId);
            
            try
            {
                {
                    _user.Client = client;
                    _user.DateOfBirth = dateOfBirth;
                    _user.EmailAddress = email;
                    _user.FirstName = firstName;
                    _user.LastName = lastName;
                };
            }
            catch (ArgumentException e)
            {
                return false;
            }

            //BL + Infrastructure
            if (client.Type == "VeryImportantClient") {
                _user.HasCreditLimit = false;
            }
            else if (client.Type == "ImportantClient") {
                int creditLimit = _creditService.GetCreditLimit(_user.LastName, _user.DateOfBirth);
                creditLimit = creditLimit * 2;
                _user.CreditLimit = creditLimit;
            }
            else {
                _user.HasCreditLimit = true;
                int creditLimit = _creditService.GetCreditLimit(_user.LastName, _user.DateOfBirth);
                _user.CreditLimit = creditLimit;
            }
            
            //BL - validation
            if (_user.HasCreditLimit && _user.CreditLimit < 500) {
                return false;
            }

            //Infrastructure
            UserDataAccess.AddUser(_user);
            return true;
        }
    }
}
