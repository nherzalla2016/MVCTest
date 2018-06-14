using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
   public class AssignmentContext : DbContext
    {
        public AssignmentContext()
           : base(ConfigurationManager.ConnectionStrings["DataContext"].ToString())
       {

        }
        public DbSet<Contacts> Contacts { get; set; }
        public DbSet<Addresses> Addresses { get; set; }
    }
}
