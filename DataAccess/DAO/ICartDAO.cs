using DataAccess.DTO;
using System.Collections.Generic;

namespace DataAccess.DAO
{
    public interface ICartDAO
    {
        int Cart_AddProductToCart(int ProductID, int UserID);

        List<CartDTO> Carts_GetCartByUser(int UserID);

        int Cart_CheckOut(int UserID);

        int Cart_Update(int ProductID, int Quantity);

        int Cart_Delete(int ProductID);
    }
}
