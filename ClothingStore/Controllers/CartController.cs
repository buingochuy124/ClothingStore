using ClothingStore.Models;
using DataAccess.DTO;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ClothingStore.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {
            var userSession = (UserDTO)Session[DataAccess.Libs.UserSessionConfig.SessionAccount];
            try
            {
                if (userSession == null)
                {
                    return RedirectToAction("Login", "Unauthenticate");

                }
                else
                {
                    var userCart = new DataAccess.DAOImpl.CartDAOImpl().Carts_GetCartByUser(userSession.UserID);
                    var result = new List<ClothingStore.Models.ViewModel.UserCartViewModel>();
                    foreach (var item in userCart)
                    {
                        result.Add(new Models.ViewModel.UserCartViewModel
                        {
                            Product = new DataAccess.DAOImpl.ProductDAOImpl().Product_GetDetail(item.ProductID),
                            Quantity = item.Quantity,
                        });
                    }
                    return View(result);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ActionResult DeleteCart(int ProductID)
        {
            var userSession = (UserDTO)Session[DataAccess.Libs.UserSessionConfig.SessionAccount];
            var result = 0;
            try
            {
                result = new DataAccess.DAOImpl.CartDAOImpl().Cart_Delete(ProductID);
                if (result > 0)
                {
                    return Content("<script language='javascript' type='text/javascript'>" +
                     "alert('Delete successfully !!!');" +
                     "window.location.href='/Cart/Index';</script>");
                }
                else
                {
                    return Content("<script language='javascript' type='text/javascript'>" +
                     "alert('Delete fail !!!');" +
                     "window.location.href='/Cart/Index';</script>");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public JsonResult CartUpdate(string Quantity)
        {
            var returnData = new ReturnData();

            try
            {
                var userSession = (UserDTO)Session[DataAccess.Libs.UserSessionConfig.SessionAccount];
                var quantity = 0;
                var cart = new DataAccess.DAOImpl.CartDAOImpl().Carts_GetCartByUser(userSession.UserID);
                for (int i = 0; i < cart.Count; i++)
                {
                    quantity = int.Parse(Quantity.Split('_')[i]);
                    var updatecart = new DataAccess.DAOImpl.CartDAOImpl().Cart_Update(cart[i].ProductID, quantity);
                }
                returnData.Description = "Update Successfully";
                return Json(returnData, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                returnData.Description = "Update Fail!! something wrong";
                return Json(returnData, JsonRequestBehavior.AllowGet);
            }


        }
        public JsonResult CheckOut(string Quantity)
        {
            var userSession = (UserDTO)Session[DataAccess.Libs.UserSessionConfig.SessionAccount];
            var returnData = new ReturnData();

            var quantity = 0;
            var cart = new DataAccess.DAOImpl.CartDAOImpl()
                .Carts_GetCartByUser(userSession.UserID);
            for (int i = 0; i < cart.Count; i++)
            {
                quantity = int.Parse(Quantity.Split('_')[i]);
                var updatecart = new DataAccess.DAOImpl.CartDAOImpl()
                    .Cart_Update(cart[i].ProductID, quantity);
            }

            cart = new DataAccess.DAOImpl.CartDAOImpl()
                .Carts_GetCartByUser(userSession.UserID);

            double total = 0;

            foreach (var item in cart)
            {
                total += item.Quantity * (new DataAccess.DAOImpl.ProductDAOImpl().Product_GetDetail(item.ProductID).Cost);
            }
            var now = DateTime.Now;
            var createOrder = new DataAccess.DAOImpl.OrderDAOImpl().CreateOrder(userSession.UserID, total, now);
            var orderID = new DataAccess.DAOImpl.OrderDAOImpl().Order_GetOrderID(userSession.UserID, now).OrderID;
            var createOrderDetail = 0;
            for (int i = 0; i < cart.Count; i++)
            {
                createOrderDetail = new DataAccess.DAOImpl.OrderDetailDAOImpl()
                    .OrderDetail_Create(cart[i].ProductID, cart[i].Quantity, orderID);
            }

            var clearCart = new DataAccess.DAOImpl.CartDAOImpl().Cart_CheckOut(userSession.UserID);

            returnData.Description = "Check Out Successfully";
            return Json(returnData, JsonRequestBehavior.AllowGet);
        }
    }
}