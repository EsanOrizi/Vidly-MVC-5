using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        // To add IsSubscribedToNewsLetter to customer class and customer table 
        public bool IsSubscribedToNewsLetter { get; set; }
        // navigation property, allows us to navigate from customer to membership type and load both from the database
        public MembershipType MembershipType { get; set; }
        // this property is a foreign key 
        public byte MembershipTypeId { get; set; }
    }
}