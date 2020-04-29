﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Models
{
    public class ProductCategory : BaseEntity
    {
        [Required(ErrorMessage="PLease fill category")]
        public string Category { get; set; }

    }
}
