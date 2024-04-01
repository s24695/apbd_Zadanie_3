using System;

namespace LegacyApp
{
    public class UserService
    {
        private Validator _validator;

        public UserService()
        {
            _validator = new Validator();
        }

        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            //validators
            _validator.ValidateName(firstName, lastName);
            _validator.ValidateEmail(email);
            _validator.ValidateAge(dateOfBirth);
            
            //client
            var clientRepository = new ClientRepository();
            var client = clientRepository.GetById(clientId);
            
             //  create user
            var user = CreateUser(client, dateOfBirth, email, firstName, lastName);
            
            //client type
            if (client.Type == "VeryImportantClient")
            {
                user.HasCreditLimit = false;
            }
            else if (client.Type == "ImportantClient")
            {
                using (var userCreditService = new UserCreditService())
                {
                    int creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                    creditLimit = creditLimit * 2;
                    user.CreditLimit = creditLimit;
                }
            }
            else
            {
                user.HasCreditLimit = true;
                using (var userCreditService = new UserCreditService())
                {
                    int creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                    user.CreditLimit = creditLimit;
                }
            }
            
            //limit check
            _validator.ValidateUserCredit(user);
            
            //Add user
            UserDataAccess.AddUser(user);
            return true;
        }

        static User CreateUser(Client client, DateTime dateOfBirth, string email, string firstName, string lastName)
        {
            var user = new User
                        {
                            Client = client,
                            DateOfBirth = dateOfBirth,
                            EmailAddress = email,
                            FirstName = firstName,
                            LastName = lastName
                        };
            return user;
        }
    }
}
