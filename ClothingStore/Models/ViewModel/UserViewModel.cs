using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClothingStore.Models.ViewModel
{
    public class UserViewModel
    {
        public int UserID { get; set; }
        public string UserAccount { get; set; }
        public string UserPassword { get; set; }
        public string UserFullName { get; set; }
        public string UserAddress { get; set; }
        public string UserPhoneNumber { get; set; }
        public int RoleID { get; set; }
        public string RoleName { get; set; }
    }
}