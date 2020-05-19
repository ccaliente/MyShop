using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Models
{
    public class Customer : BaseEntity
    {
        public string UserId { get; set; }
        //[Required(ErrorMessage = "Please fill first name")]
        public string FirstName { get; set; }
        //[Required(ErrorMessage = "Please fill last name")]
        public string LastName { get; set; }
        //[Required(ErrorMessage = "Please fill Email")]
        public string Email { get; set; }
        //[Required(ErrorMessage = "Please fill Street")]
        public string Street { get; set; }
        //[Required(ErrorMessage = "Please fill City")]
        public string City { get; set; }
        //[Required(ErrorMessage = "Please fill Country")]
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public string Phone { get; set; }
        //public virtual InvoiceData Invoice { get; set; }

    }
}
