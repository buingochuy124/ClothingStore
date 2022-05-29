using DataAccess.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClothingStore.Models.ViewModel
{
    public class OrderViewModel
    {
        public int OrderID { get; set; }
        public int UserID { get; set; }
        public UserDTO User { get; set; }
        public double Total { get; set; }
        public DateTime Date { get; set; }
    }
}