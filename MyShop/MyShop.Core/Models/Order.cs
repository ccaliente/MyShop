using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Models
{
    public class Order : BaseEntity
    {
        [Required(ErrorMessage = "Order must have client's first name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Order must have client's last name")]
        public string Surname { get; set; }
        public string Email { get; set; }
        [Required(ErrorMessage = "Order must have delivery street")]
        public string Street { get; set; }
        [Required(ErrorMessage = "Order must have delivery city")]
        public string City { get; set; }
        [Required(ErrorMessage = "Order must have delivery country")]
        public string Country { get; set; }
        public string ZipCode { get; set; }
        [Required(ErrorMessage = "Order must have client's phone number")]
        public string Phone { get; set; }
        public string OrderStatus { get; set; }

        [Required(ErrorMessage = "Order must have delivery office")]
        public string DeliveryOffice { get; set; }

        [Required(ErrorMessage = "Order must have delivery type")]
        public string DeliveryType { get; set; }

        public int PaymentType { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }

        public Order()
        {
            OrderItems = new List<OrderItem>();
        }
    }

}
