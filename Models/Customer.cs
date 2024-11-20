using System.Collections.Generic;

namespace SWEPP.Models
{
    public class Customer
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string Subdivision { get; set; }
        public string MajorIntersection { get; set; }
        public string PaymentType { get; set; }
        public List<Receipt> OrderHistory { get; set; } = new List<Receipt>();
    }
}