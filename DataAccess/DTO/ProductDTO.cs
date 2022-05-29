using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO
{
    public class ProductDTO
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public double Cost { get; set; }
        public string ProductDescription { get; set; }
        public string ProductImageURL { get; set; }
        public int CategoryID { get; set; }
    }
}
