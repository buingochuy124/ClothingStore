using ClothingStore.Models;
using System.Linq;
using System.Web.Mvc;

namespace ClothingStore.Controllers
{
    public class UnauthenticateController : Controller
    {
        // GET: Unauthenticate
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Logout()
        {
            Session.RemoveAll();
            Session.Abandon();
            return RedirectToAction("Login", "Unauthenticate");
        }
        public ActionResult Register()
        {
            return View();
        }
        public ActionResult RegisterCheck(string UserAccount, string UserPassword, string  UserFullName, string UserAddress, string UserPhoneNumber)
        {
            var result = new DataAccess.DAOImpl.UserDAOImpl()
                .User_Register(UserAccount, UserPassword, UserFullName, UserAddress, UserPhoneNumber,2);
            if(result > 0)
            {
                Session[DataAccess.Libs.UserSessionConfig.SessionAccount] = new DataAccess.DAOImpl
                        .UserDAOImpl()
                        .Users_GetList()
                        .FirstOrDefault(u => u.UserAccount == UserAccount);

                return Content("<script language='javascript' type='text/javascript'>" +
                      "alert('Register Succesfully !!!');" +
                      "window.location.href='/Product/Index';</script>");
            }
            else
            {
                if(result == -100)
                {
                    return Content("<script language='javascript' type='text/javascript'>" +
                     "alert('Account already exist !!! please try another account');" +
                     "window.location.href='/Unauthenticate/Register';</script>");
                }
                return Content("<script language='javascript' type='text/javascript'>" +
                     "alert('Something went wrong !!! please try again');" +
                     "window.location.href='/Product/Index';</script>");
            }

          
        }
        public ActionResult LoginCheck(string UserAccount, string UserPassword)
        {
            var returnData = new ReturnData();

            var result = new DataAccess.DAOImpl.UserDAOImpl().User_Login(UserAccount, UserPassword);

            try
            {
                if (result > 0)
                {

                    returnData.ResponseCode = 1;

                    Session[DataAccess.Libs.UserSessionConfig.SessionAccount] = new DataAccess.DAOImpl
                        .UserDAOImpl()
                        .Users_GetList()
                        .FirstOrDefault(u => u.UserAccount == UserAccount);

                    //returnData.ResponseCode = 1;
                    //returnData.Description = "Login successfully !!!"; 
                    //return Json(returnData, JsonRequestBehavior.AllowGet);

                    return RedirectToAction("Index", "Product");
                }
                else if (result == -1)
                {

                    //returnData.ResponseCode = -997;
                    //returnData.Description = "User Account or Password was wrong. Login Fail !!!";
                    //return Json(returnData, JsonRequestBehavior.AllowGet);
                    return Content("<script language='javascript' type='text/javascript'>" +
                        "alert('User Account not exist. Login Fail !!!');" +
                        "window.location.href='/Unauthenticate/Login';</script>");
                }

                else
                {
                    return Content("<script language='javascript' type='text/javascript'>" +
                       "alert('User Account not exist. Login Fail !!!');" +
                       "window.location.href='/Unauthenticate/Login';</script>");
                    //returnData.ResponseCode = -997;
                    //returnData.Description = "User Account or Password was wrong. Login Fail !!!";
                    //return Json(returnData, JsonRequestBehavior.AllowGet);
                    //return Content("<script language='javascript'" +
                    //    " type='text/javascript'>alert('User Account or Password was wrong. Login Fail !!!');" +
                    //    "window.location.href='/Unauthenticate/Login';</script>");
                }
            }


            catch (System.Exception)
            {

                throw;
                //returnData.ResponseCode = -999;
                //returnData.Description = "System Bussy. please  f5";
                //return Json(returnData, JsonRequestBehavior.AllowGet);
            }
        }



    }
}