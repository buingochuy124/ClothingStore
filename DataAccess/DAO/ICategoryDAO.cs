using DataAccess.DTO;
using System.Collections.Generic;

namespace DataAccess.DAO
{
    public interface ICategoryDAO
    {
        List<CategoryDTO> Categories_GetList();
        CategoryDTO Category_GetDetailByID(int CategoryID);
        int Category_CreateCategory(string CategoryName);
    }
}
