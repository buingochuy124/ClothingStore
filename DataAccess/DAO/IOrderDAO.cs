using DataAccess.DTO;
using System;
using System.Collections.Generic;

namespace DataAccess.DAO
{
    public interface IOrderDAO
    {
        int CreateOrder(int UserID, double Total, DateTime Date);

        OrderDTO Order_GetOrderID(int UserID, DateTime Date);

        List<OrderDTO> Order_GetUserOrder(int UserID);

        List<OrderDTO> Orders_GetList();
    }
}
