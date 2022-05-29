using DataAccess.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClothingStore.Models.ViewModel
{
    public class OrderDetailViewModel
    {
        public int ID { get; set; }
        public int ProductID { get; set; }
        public ProductDTO Product { get; set; }

        public int Quantity { get; set; }
        public int OrderID { get; set; }
    }
}