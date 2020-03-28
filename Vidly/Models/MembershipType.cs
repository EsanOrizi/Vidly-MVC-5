using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.Models
{

    // new class to create the MembershipType table
    public class MembershipType
    {
        public byte Id { get; set; }
        public short SignUpFee { get; set; }
        public byte DurationInMonths { get; set; }
        public byte DiscountRate { get; set; }
        // To add MembershipType
        public String MembershipTypeName { get; set; }

    }
}