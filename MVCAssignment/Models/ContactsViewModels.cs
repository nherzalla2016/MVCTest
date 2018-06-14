using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCAssignment.Models
{
    public class ContactsViewModels
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string BirthDate { get; set; }
        public int NumberOfComupters { get; set; }

        public int Address_Id { get; set; }

        public Dictionary<int,string>  Addresses { get; set; }

    }
}