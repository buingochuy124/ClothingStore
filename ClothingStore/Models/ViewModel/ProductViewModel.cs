using DataAccess.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClothingStore.Models.ViewModel
{
    public class ProductViewModel
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public double Cost { get; set; }
        public string ProductDescription { get; set; }
        public string ProductImageURL { get; set; }


        public int CategoryID { get; set; }
        public string CategoryName { get; set; }

    }
}