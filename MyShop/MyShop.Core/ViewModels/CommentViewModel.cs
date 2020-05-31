using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.ViewModels
{
    public class CommentViewModel
    {
        public string ProductId { get; set; }
        public string UserId { get; set; }
        public string Text { get; set; }
        public int Rating { get; set; }
    }
}
