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
    public class AddressesController : Controller
    {
        Repository.IRepository<Addresses> _repo;
        
        public AddressesController()
        {
            _repo = new Repository.GenericRepository<Addresses>();
        }
        public ActionResult Index()
        {
            return View(AllAdresses());
        }
        public ActionResult AddAddress()
        {
            return View();
        }
        public ActionResult Insert(AddressesViewModels sm)
        {
            var address = new Addresses
            {
                AddressLine1 = sm.AddressLine1,
                AddressLine2 = sm.AddressLine2,
                City = sm.City,
                StateCode = sm.StateCode,
                Zip=sm.Zip
            };
            _repo.Add(address);
            return Json(new
            {
                redirectUrl = Url.Action("Index", "Addresses"),
                isRedirect = true
            });
        }
        public ActionResult Edit(int Id)
        {
            var address = _repo.GetById(Id);
            var addressModel = new AddressesViewModels
            {
                AddressLine1 = address.AddressLine1,
                AddressLine2 = address.AddressLine2,
                City = address.City,
                Id = address.Id,
                StateCode = address.StateCode,
                Zip = address.Zip
            };
            return View(addressModel);
        }
        [HttpPost]
        public ActionResult Update(AddressesViewModels sm)
        {
            var address = new Addresses
            {
                Id = sm.Id,
                AddressLine1 = sm.AddressLine1,
                AddressLine2 = sm.AddressLine2,
                City= sm.City,
                StateCode = sm.StateCode,
                Zip = sm.Zip
            };
            _repo.Update(address);


            return Json(new
            {
                redirectUrl = Url.Action("Index", "Addresses"),
                isRedirect = true
            });

        }

        public ActionResult Delete(int Id)
        {
            _repo.Delete(Id);
            return Json(new
            {
                redirectUrl = Url.Action("Index", "Addresses"),
                isRedirect = true
            });
        }
        private List<AddressesViewModels> AllAdresses()
        {
            var addressesList = new List<AddressesViewModels>();
            var addresses = _repo.GetAll();
            if(addresses.Any())
            {
                foreach(var address in addresses)
                {
                    addressesList.Add(new Models.AddressesViewModels
                    {
                        AddressLine1 = address.AddressLine1,
                        AddressLine2 = address.AddressLine2,
                        City = address.City,
                        Id = address.Id,
                        StateCode = address.StateCode,
                        Zip = address.Zip
                    });
                }
            }
            return addressesList;
        }

    }
}