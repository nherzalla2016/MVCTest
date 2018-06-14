using MVCAssignment.Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCAssignment.Controllers
{
    [Authorize]
    public class ContactsController : Controller
    {
        Repository.IRepository<Contacts> _repo;
        Repository.IRepository<Addresses> _addressRepo;
        Repository.ISummaryRepository _summaryRepo;
        public ContactsController()
        {
            _repo = new Repository.GenericRepository<Contacts>();
            _addressRepo = new Repository.GenericRepository<Addresses>();
            _summaryRepo = new SummaryRepository();
        }
        public ActionResult Index()
        {
           
            var contactsViewModelList= AllContacts();
            return View(contactsViewModelList);
        }
        public ActionResult Summary()
        {
            var summaryVM = new List<SummaryViewModel>();
            var summaryResult = _summaryRepo.AddressSummary();
            if(summaryResult.Any())
            {
                foreach(var item in summaryResult)
                {
                    summaryVM.Add(new SummaryViewModel { Address = item.Address, NumberOfContacts = item.NumberOfContacts });
                }
            }

            return View(summaryVM);
        }

        public ActionResult Search(string search)
        {
            var contactsViewModelList = new List<ContactsViewModels>();

            if (!string.IsNullOrEmpty(search))
            {
                string querystr = search;
                var contactsSearch = _repo.SearchFor(q => q.FirstName.Contains(querystr) || q.LastName.Contains(querystr));
                if(contactsSearch.Any())
                {
                    foreach (var contact in contactsSearch)
                    {
                        contactsViewModelList.Add(new ContactsViewModels
                        {
                            Id = contact.Id,
                            FirstName = contact.FirstName,
                            LastName = contact.LastName,
                            EmailAddress = contact.EmailAddress,
                            NumberOfComupters = contact.NumberOfComupters,
                            BirthDate = contact.BirthDate.ToShortDateString()

                        });
                    }
                }
            }
            return View(contactsViewModelList);
        }
        public ActionResult Edit(int Id)
        {
           


            var contact =  _repo.GetById(Id);
            var contactModel = new ContactsViewModels
            {
                BirthDate = contact.BirthDate.ToShortDateString(),
                EmailAddress = contact.EmailAddress,
                FirstName = contact.FirstName,
                Id = contact.Id,
                LastName = contact.LastName,
                NumberOfComupters = contact.NumberOfComupters
            };


            contactModel.Addresses = new Dictionary<int, string>();
            var addresssList = _addressRepo.GetAll();
            if (addresssList.Any())
            {
                foreach (var address in addresssList)
                {
                    contactModel.Addresses.Add(address.Id, address.AddressLine1 + " " + address.AddressLine2);
                }

            }
            return View(contactModel);
        }
        [HttpPost]
        public ActionResult Update(ContactsViewModels sm)
        {

            var contact = new Contacts
            {
                Id = sm.Id,
                EmailAddress = sm.EmailAddress,
                BirthDate = Convert.ToDateTime(sm.BirthDate),
                NumberOfComupters = sm.NumberOfComupters,
                FirstName = sm.FirstName,
                LastName = sm.LastName,
                 Address_Id = sm.Address_Id
            };
           
            _repo.Update(contact);


            return Json(new
            {
                redirectUrl = Url.Action("Index", "Contacts"),
                isRedirect = true
            });
           
        }

        private List<ContactsViewModels> AllContacts()
        {
            var contactsViewModelList = new List<ContactsViewModels>();
            var contactsList = _repo.GetAll();
            if (contactsList.Any())
            {
                foreach (var contact in contactsList)
                {
                    contactsViewModelList.Add(new ContactsViewModels
                    {
                        Id = contact.Id,
                        FirstName = contact.FirstName,
                        LastName = contact.LastName,
                        EmailAddress = contact.EmailAddress,
                        NumberOfComupters = contact.NumberOfComupters,
                        BirthDate = contact.BirthDate.ToShortDateString()

                    });
                }
            }
            return contactsViewModelList;
        }

        public ActionResult AddContact()
        {
            var contactViewModel = new ContactsViewModels();
            contactViewModel.Addresses = new Dictionary<int, string>();
            var addresssList = _addressRepo.GetAll();
            if(addresssList.Any())
            {
                foreach(var address in addresssList)
                {
                    contactViewModel.Addresses.Add(address.Id, address.AddressLine1 + " " + address.AddressLine2);
                }
                
            }
            return View(contactViewModel);
        }
        public ActionResult Insert(ContactsViewModels sm)
        {

            var contact = new Contacts
            {
                Id = sm.Id,
                FirstName = sm.FirstName,
                LastName = sm.LastName,
                BirthDate = Convert.ToDateTime(sm.BirthDate),
                EmailAddress = sm.EmailAddress,
                NumberOfComupters = sm.NumberOfComupters,
                 Address_Id = sm.Address_Id
               
            };
            _repo.Add(contact);
            return Json(new
            {
                redirectUrl = Url.Action("Index", "Contacts"),
                isRedirect = true
            });
        }
        public ActionResult Delete(int Id)
        {
            _repo.Delete(Id);
            return Json(new
            {
                redirectUrl = Url.Action("Index", "Contacts"),
                isRedirect = true
            });
        }
    }
}