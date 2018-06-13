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
        
        public ActionResult Index()
        {
            return View();
        }
    }
}