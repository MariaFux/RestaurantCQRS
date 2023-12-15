using Domain.Exceptions;
using Domain.Validators.UserProfileValidators;

namespace Domain.Aggregates.UserProfileAggregate
{
    public class BasicInfo
    {
        private BasicInfo() 
        { 
        }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public string CurrentCity { get; private set; }

        //Factory method
        public static BasicInfo CreateBasicInfo(string firstName, string lastName, string email, 
            string phone, DateTime dateOfBirth, string currentCity)
        {
            var validator = new BasicInfoValidator();

            var objToValidate = new BasicInfo
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Phone = phone,
                DateOfBirth = dateOfBirth,
                CurrentCity = currentCity
            };
            
            var validationResult = validator.Validate(objToValidate);

            if (validationResult.IsValid) return objToValidate;

            var exception = new UserProfileNotValidException("The user profile is not valid");
            foreach (var error in validationResult.Errors)
            {
                exception.ValidationErrors.Add(error.ErrorMessage);
            }

            throw exception;
        }
    }
}
