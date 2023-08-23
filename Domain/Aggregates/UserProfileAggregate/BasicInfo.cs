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
            //TODO: add validation
            return new BasicInfo
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Phone = phone,
                DateOfBirth = dateOfBirth,
                CurrentCity = currentCity
            };
        }
    }
}
