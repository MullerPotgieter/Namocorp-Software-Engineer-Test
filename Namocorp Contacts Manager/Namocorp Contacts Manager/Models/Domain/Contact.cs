namespace Namocorp_Contacts_Manager.Models.Domain
{
    public class Contact
    {
        public Guid ContactId { get; set; }

        public string FirstName { get; set; }

        public string Surname { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string TelephoneNumber { get; set; }
        
        public string CellNumber { get; set; }

        public string Address { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        public string CityData { get; set; }

        public string StateData { get; set; }

        public string CountryData { get; set; }

        public void eliminateNulls()
        {
            if (FirstName == null)
                FirstName = "";

            if (Surname == null)
                Surname = "";

            if (CellNumber == null)
                CellNumber = "";

            if (TelephoneNumber == null)
                TelephoneNumber = "";

            if (Address == null)
                Address = "";

            if (Street == null)
                Street = "";

            if (City == null)
                City = "";

            if (State == null)
                State = "";

            if (Country == null)
                Country = "";

            if (CityData == null)
                CityData = "";

            if (StateData == null)
                StateData = "";

            if (CountryData == null)
                CountryData = "";

        }
    }
}
