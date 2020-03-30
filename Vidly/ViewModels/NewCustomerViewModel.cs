using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.ViewModels
{
    public class NewCustomerViewModel
    {

        public IEnumerable<MembershipType> MembershipTypes { get; set; }
        // properties of new customer
        public Customer Customer { get; set; }


    }
}