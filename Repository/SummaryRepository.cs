using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class SummaryRepository : ISummaryRepository
    {
        AssignmentContext _context;
        GenericRepository<Addresses> _repo;
       
        public SummaryRepository()
        {
            this._context = new AssignmentContext();
            this._repo = new GenericRepository<Addresses>();

        }
        public List<Summary> AddressSummary()
        {

            var result = new List<Summary>();

           

            var summary = from obj in this._context.Contacts
                          where obj.Address_Id != 0

                          group obj by obj.Address_Id into summGroup
                          select new
                          {
                              address = summGroup.Key,
                              noofContacts = summGroup.Count()
                          };

            if(summary.Any())
            {
                foreach(var item in summary)
                {
                    result.Add(new Repository.Summary { Address = _repo.GetById(item.address).AddressLine1 + " " + _repo.GetById(item.address).AddressLine1, NumberOfContacts = item.noofContacts });
                }
            }

            return result;
        }
    }
}
