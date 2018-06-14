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
        public ContactsController()
        {
            _repo = new Repository.GenericRepository<Contacts>();
        }
        public ActionResult Index()
        {
           
            var contactsViewModelList= AllContacts();
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
                LastName = sm.LastName
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
            return View();
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
                NumberOfComupters = sm.NumberOfComupters
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