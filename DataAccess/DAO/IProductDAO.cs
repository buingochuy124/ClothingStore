using DataAccess.DTO;
using System.Collections.Generic;

namespace DataAccess.DAO
{
    public interface IProductDAO
    {
        List<ProductDTO> Products_GetList();
        List<ProductDTO> Products_GetListByPage(int? PageNumber, int? NumberPerPage);
        List<ProductDTO> Products_SearchByCategory(int CategoryID);
        List<ProductDTO> Products_SearchByCategoryAndPage(int? PageNumber, int? NumberPerPage, int CategoryID);
        ProductDTO Product_GetDetail(int ProductID);
        int Product_Create(string ProductName, double Cost, string ProductDescription, string ProductImage, int CategoryID);
        int Product_Delete(int ProductID);

    }
}
