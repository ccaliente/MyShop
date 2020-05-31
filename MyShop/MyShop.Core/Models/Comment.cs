using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Models
{
    public class Comment : BaseEntity
    {
        public string Text { get; set; }
        public string UserId { get; set; }
        public int Rating { get; set; }
        public string ProductId { get; set; }
    }
}
