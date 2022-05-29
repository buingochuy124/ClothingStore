using DataAccess.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClothingStore.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index()
        {
            var userSession = (UserDTO)Session[DataAccess.Libs.UserSessionConfig.SessionAccount];
            if (userSession == null)
            {
                return RedirectToAction("Login", "Unauthenticate");
            }
            else
            {
                if(userSession.RoleID == 2)
                {
                    var result = new DataAccess.DAOImpl.OrderDAOImpl().Order_GetUserOrder(userSession.UserID);
                    return View(result);
                }
                else
                {
                    var result = new List<ClothingStore.Models.ViewModel.OrderViewModel>();
                    var orders =  new DataAccess.DAOImpl.OrderDAOImpl().Orders_GetList();
                    foreach (var item in orders)
                    {
                        result.Add(new ClothingStore.Models.ViewModel.OrderViewModel
                        {
                            OrderID = item.OrderID,
                            Date = item.Date,
                            Total = item.Total,
                            User = new DataAccess.DAOImpl.UserDAOImpl().Users_GetList().FirstOrDefault(u => u.UserID == item.UserID)
                        });
                    }
                    return View(result);
                }
               
               
            }

        }
        public ActionResult OrderDetail(int OrderID)
        {
            var userSession = (UserDTO)Session[DataAccess.Libs.UserSessionConfig.SessionAccount];
            if (userSession == null)
            {
                return RedirectToAction("Login", "Unauthenticate");
            }
            else
            {
                var orderDetails = new DataAccess.DAOImpl.OrderDetailDAOImpl()
                    .OrderDetail_GetOrderDetail(OrderID);
                var result = new List<ClothingStore.Models.ViewModel.OrderDetailViewModel>();
                foreach (var item in orderDetails)
                {
                    result.Add(new Models.ViewModel.OrderDetailViewModel
                    {
                        Quantity = item.Quantity,
                        Product = new DataAccess.DAOImpl.ProductDAOImpl().Product_GetDetail(item.ProductID)
                    });
                }
                return View(result);
            }
        }
    }
}