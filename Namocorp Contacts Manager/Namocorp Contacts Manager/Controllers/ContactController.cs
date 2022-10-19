using Namocorp_Contacts_Manager.Data;
using Namocorp_Contacts_Manager.Models;
using Namocorp_Contacts_Manager.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Namocorp_Contacts_Manager.Controllers
{
    public class ContactController : Controller
    {
        private readonly NamocorpContactsManagerDbContext namocorpContactsManagerDbContext;

        public ContactController(NamocorpContactsManagerDbContext namocorpContactsManagerDbContext)
        {
            this.namocorpContactsManagerDbContext = namocorpContactsManagerDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Contact> contacts = await namocorpContactsManagerDbContext.Contacts.ToListAsync();

            return View(contacts);

        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddContactViewModel addContactRequest)
        {
            Contact contact = new Contact()
            {
                ContactId = Guid.NewGuid(),
                FirstName = addContactRequest.FirstName,
                Surname = addContactRequest.Surname,
                DateOfBirth = addContactRequest.DateOfBirth,
                TelephoneNumber = addContactRequest.TelephoneNumber,
                CellNumber = addContactRequest.CellNumber,
                Address =
                    addContactRequest.Street + ", " +
                    addContactRequest.City + ", " +
                    addContactRequest.State + ", " +
                    addContactRequest.Country + ";",
                Street = addContactRequest.Street,
                City = addContactRequest.City,
                State = addContactRequest.State,
                Country = addContactRequest.Country,
                CityData = await GeographicDataRequester.extractCountryPosition(addContactRequest.Country),
                StateData = await GeographicDataRequester.extractCountryStates(addContactRequest.Country),
                CountryData = await GeographicDataRequester.extractStateCities(addContactRequest.Country, addContactRequest.State)
            };

            contact.eliminateNulls();

            await namocorpContactsManagerDbContext.Contacts.AddAsync(contact);
            await namocorpContactsManagerDbContext.SaveChangesAsync();

            return RedirectToAction("Add");

        }

        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var contact = await namocorpContactsManagerDbContext.Contacts.FirstOrDefaultAsync(x => x.ContactId == id);

            if (contact != null)
            { 
                var viewModel = new UpdateContactViewModel()
                {
                    ContactId = contact.ContactId,
                    FirstName = contact.FirstName,
                    Surname = contact.Surname,
                    DateOfBirth = contact.DateOfBirth,
                    TelephoneNumber = contact.TelephoneNumber,
                    CellNumber = contact.CellNumber,
                    Address = contact.Address,
                    Street = contact.Street,
                    City = contact.City,
                    State = contact.State,
                    Country = contact.Country,
                    CityData = contact.CityData,
                    StateData = contact.StateData,
                    CountryData = contact.CountryData
                };

                return await Task.Run(() => View("View", viewModel));
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> View(UpdateContactViewModel model)
        {
            var contact = await namocorpContactsManagerDbContext.Contacts.FindAsync(model.ContactId);

            if(contact != null)
            {
                contact.ContactId = model.ContactId;
                contact.FirstName = model.FirstName;
                contact.Surname = model.Surname;
                contact.DateOfBirth = model.DateOfBirth;
                contact.TelephoneNumber = model.TelephoneNumber;
                contact.CellNumber = model.CellNumber;
                contact.Address =
                    model.Street + ", " +
                    model.City + ", " +
                    model.State + ", " +
                    model.Country + ";";
                contact.Street = model.Street;
                contact.City = model.City;
                contact.State = model.State;
                contact.Country = model.Country;
                contact.CityData = await GeographicDataRequester.extractCountryPosition(model.Country);
                contact.StateData = await GeographicDataRequester.extractCountryStates(model.Country);
                contact.CountryData = await GeographicDataRequester.extractStateCities(model.Country, model.State);

                contact.eliminateNulls();

                await namocorpContactsManagerDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");

        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateContactViewModel model)
        {
            var contact = await namocorpContactsManagerDbContext.Contacts.FindAsync(model.ContactId);

            if(model != null)
            {
                namocorpContactsManagerDbContext.Contacts.Remove(contact);
                await namocorpContactsManagerDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }           

            return RedirectToAction("Index");

        }
    }
}
