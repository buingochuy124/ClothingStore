using System.Collections.Generic;

namespace DataAccess.DAO
{
    public interface IOrderDetailDAO
    {
        int OrderDetail_Create(int ProductID, int Quantity, int OderID);

        List<DataAccess.DTO.OrderDetailDTO> OrderDetail_GetOrderDetail(int OrderID);

        //List<DataAccess.DTO.OrderDetailDTO> OrderDetail_GetList();
    }
}
