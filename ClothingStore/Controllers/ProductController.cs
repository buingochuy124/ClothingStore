using ClothingStore.Models.ViewModel;
using DataAccess.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ClothingStore.Controllers
{
    public class ProductController : Controller
    {
        public ActionResult Index()
        {
            //DataAccess.Lib.SendMail.SendMailToAccount("test", "huybngcd191347@fpt.edu.vn");

            return View();
        }

        public ActionResult ProductPartialView(int? PageNumber, int? NumberPerPage)
        {
            var userSession = (UserDTO)Session[DataAccess.Libs.UserSessionConfig.SessionAccount];
            ViewBag.user = userSession;
            try
            {
                if (PageNumber == null && NumberPerPage == null)
                {
                    PageNumber = 1;
                    NumberPerPage = 12;
                }
                ViewBag.CurrentPage = PageNumber;
                ViewBag.NumberPerPage = NumberPerPage;
                ViewBag.EndPage = (new DataAccess.DAOImpl.ProductDAOImpl()
                    .Products_GetList().Count) / NumberPerPage + 1;

                var result = new List<ProductViewModel>();
                var listProduct = new DataAccess.DAOImpl.ProductDAOImpl()
                    .Products_GetListByPage(PageNumber, NumberPerPage);
                foreach (var item in listProduct)
                {
                    result.Add(new ProductViewModel
                    {
                        ProductID = item.ProductID,
                        ProductName = item.ProductName,
                        Cost = item.Cost,
                        ProductDescription = item.ProductDescription,
                        ProductImageURL = item.ProductImageURL,
                        CategoryID = item.CategoryID,
                        CategoryName = new DataAccess.DAOImpl.CategoryDAOImpl()
                        .Category_GetDetailByID(item.CategoryID).CategoryName,

                    });
                }
                return PartialView(result);
            }
            catch (System.Exception)
            {
                return RedirectToAction("Index", "Product");
            }


        }

        public ActionResult CategoryInProductPagePartialView()
        {
            var userSession = (UserDTO)Session[DataAccess.Libs.UserSessionConfig.SessionAccount];
            ViewBag.user = userSession;
            var result = new DataAccess.DAOImpl
                .CategoryDAOImpl().Categories_GetList();
            return PartialView(result);
        }
        public ActionResult CreateProductCategory()
        {
            return View();
        }
        public ActionResult InsertProductCategory(string CategoryName)
        {
            var userSession = (UserDTO)Session[DataAccess.Libs.UserSessionConfig.SessionAccount];
            if (userSession.RoleID != 1)
            {
                return Content("<script language='javascript' type='text/javascript'>" +
                    "alert('You Dont have permission !!!');" +
                    "window.location.href='/Product/Index';</script>");
            }
            else
            {
                var result = new DataAccess.DAOImpl.CategoryDAOImpl().Category_CreateCategory(CategoryName);
                if (result > 0)
                {
                    return Content("<script language='javascript' type='text/javascript'>" +
                     "alert('Create category succesfully!!!');" +
                     "window.location.href='/Product/Index';</script>");
                }
                else
                {
                    if (result == -100)
                    {
                        return Content("<script language='javascript' type='text/javascript'>" +
                  "alert('Category already exist !!! please try another category');" +
                  "window.location.href='/Product/Index';</script>");
                    }
                    else
                    {
                        return Content("<script language='javascript' type='text/javascript'>" +
                  "alert('Create category Fail !!! please try again');" +
                  "window.location.href='/Product/Index';</script>");
                    }

                }

            }
        }

        public ActionResult ProductCategory(int? PageNumber, int? NumberPerPage, string CategoryName)
        {
            if (PageNumber == null && NumberPerPage == null)
            {
                PageNumber = 1;
                NumberPerPage = 12;
            }
            var categoryID = new DataAccess.DAOImpl.CategoryDAOImpl().Categories_GetList()
               .FirstOrDefault(c => c.CategoryName == CategoryName).CategoryID;

            ViewBag.CurrentPage = PageNumber;
            ViewBag.NumberPerPage = NumberPerPage;
            ViewBag.EndPage = (new DataAccess.DAOImpl.ProductDAOImpl()
                .Products_SearchByCategory(categoryID).Count) / NumberPerPage + 1;

            var listProduct = new DataAccess.DAOImpl
                .ProductDAOImpl().Products_SearchByCategory(categoryID);
            var result = new List<ProductViewModel>();
            foreach (var item in listProduct)
            {
                result.Add(new ProductViewModel
                {
                    ProductID = item.ProductID,
                    ProductName = item.ProductName,
                    Cost = item.Cost,
                    ProductDescription = item.ProductDescription,
                    ProductImageURL = item.ProductImageURL,
                    CategoryID = item.CategoryID,
                    CategoryName = new DataAccess.DAOImpl.CategoryDAOImpl()
                    .Category_GetDetailByID(item.CategoryID).CategoryName,

                });
            }
            if(result.Count != 0)
            {
                ViewBag.Title = result[0].CategoryName;

            }
            return View(result);
        }
        public ActionResult AddToCart(int ProductID)
        {
            var userSession = (UserDTO)Session[DataAccess.Libs.UserSessionConfig.SessionAccount];
            if (userSession == null)
            {
                return Content("<script language='javascript' type='text/javascript'>" +
                    "alert('You Need To Login First !!!');" +
                    "window.location.href='/Unauthenticate/Login';</script>");
            }
            else
            {
                var result = new DataAccess.DAOImpl.CartDAOImpl()
                    .Cart_AddProductToCart(ProductID, userSession.UserID);
                if (result > 0)
                {
                    return Content("<script language='javascript' type='text/javascript'>" +
                        "alert('Added Product To Cart !!!');" +
                        "window.location.href='/Product/Index';</script>");

                }
                else
                {
                    return Content("<script language='javascript' type='text/javascript'>" +
                        "alert('Product Already In Cart !!! Please Try Another Product');" +
                        "window.location.href='/Product/Index';</script>");
                }
            }
        }
        public ActionResult CreateProduct()
        {
            var result = new DataAccess.DAOImpl.CategoryDAOImpl().Categories_GetList();
            return View(result);
        }
        public ActionResult InsertProduct(string ProductName, double Cost, string ProductDescription, string ProductImage, string CategoryName)
        {
            var categoryID = new DataAccess.DAOImpl.CategoryDAOImpl().Categories_GetList()
                .FirstOrDefault(c => c.CategoryName == CategoryName).CategoryID;
            var result = new DataAccess.DAOImpl.ProductDAOImpl()
                .Product_Create(ProductName, Cost, ProductDescription, ProductImage, categoryID);
            if (result > 0)
            {
                return Content("<script language='javascript' type='text/javascript'>" +
                       "alert('Product Added To Store !!!');" +
                       "window.location.href='/Product/Index';</script>");
            }
            else
            {
                return Content("<script language='javascript' type='text/javascript'>" +
                        "alert('some thing went wrong !!!');" +
                        "window.location.href='/Product/Index';</script>");
            }

        }
        //public ActionResult RemoveProduct(int ProductID)
        //{
        //    var result = new DataAccess.DAOImpl.ProductDAOImpl().Product_Delete(ProductID);
        //    if (result > 0)
        //    {
        //        return Content("<script language='javascript' type='text/javascript'>" +
        //               "alert('Delete Successfully !!!');" +
        //               "window.location.href='/Product/Index';</script>");
        //    }
        //    else
        //    {
        //        return Content("<script language='javascript' type='text/javascript'>" +
        //                "alert('some thing went wrong !!!');" +
        //                "window.location.href='/Product/Index';</script>");
        //    }
        //}
    }
}