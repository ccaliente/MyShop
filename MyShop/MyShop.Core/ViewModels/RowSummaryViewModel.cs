using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.ViewModels
{
    public class RowSummaryViewModel
    {
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public decimal RowSummary { get; set; }

        public RowSummaryViewModel()
        {

        }
        public RowSummaryViewModel(int quantity, decimal price1 , decimal rowsummary )
        {
            Quantity = quantity;
            Price = price1;
            RowSummary = rowsummary;
        }
    }
}
