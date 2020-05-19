using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Models
{
    public class InvoiceData : BaseEntity
    {
        [Required(ErrorMessage = "Please fill company name")]
        public string CompanyName { get; set; }
        [Required(ErrorMessage = "Please fill bulstat")]
        public string Bulstat { get; set; }
        [Required(ErrorMessage = "Please fill VAT")]
        public string VAT { get; set; }
        [Required(ErrorMessage = "Please fill owner name")]
        public string Owner { get; set; }
        [Required(ErrorMessage = "Please fill address")]
        public string Address { get; set; }

        //public virtual Customer Customer { get; set; }
    }
}
