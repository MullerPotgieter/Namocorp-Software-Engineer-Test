namespace Namocorp_Contacts_Manager.Models
{
    public class AddContactViewModel
    {
        public Guid ContactId { get; set; }

        public string FirstName { get; set; }

        public string Surname { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string TelephoneNumber { get; set; }

        public string CellNumber { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Country { get; set; }
    }
}
