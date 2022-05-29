using DataAccess.DAO;
using DataAccess.DTO;
using DataAccess.Libs;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DataAccess.DAOImpl
{
    public class OrderDetailDAOImpl : IOrderDetailDAO
    {
        public int OrderDetail_Create(int ProductID, int Quantity, int OderID)
        {
            var result = 0;
            try
            {
                var sqlconn = ConnectDB.GetSqlConnection();

                SqlCommand cmd = new SqlCommand("SP_CreateOrderDetail", sqlconn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@_ProductID", ProductID);
                cmd.Parameters.AddWithValue("@_Quantity", Quantity);
                cmd.Parameters.AddWithValue("@_OderID", OderID);


                cmd.Parameters.Add("@_ResponseCode", System.Data.SqlDbType.Int).Direction = System.Data.ParameterDirection.Output;


                cmd.ExecuteNonQuery();

                result = cmd.Parameters["@_ResponseCode"].Value != null ? Convert.ToInt32(cmd.Parameters["@_ResponseCode"].Value) : 0;

                return result;


            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public List<OrderDetailDTO> OrderDetail_GetOrderDetail(int OrderID)
        {
            var result = new List<DataAccess.DTO.OrderDetailDTO>();


            try
            {
                var sqlconn = ConnectDB.GetSqlConnection();

                SqlCommand cmd = new SqlCommand("SP_GetUserOrderDetail", sqlconn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@_OrderID", OrderID);



                var read = cmd.ExecuteReader();
                while (read.Read())
                {
                    result.Add(new OrderDetailDTO
                    {
                        ProductID = int.Parse(read["ProductID"].ToString()),
                        OrderID = int.Parse(read["OrderID"].ToString()),
                        Quantity = int.Parse(read["Quantity"].ToString()),

                    });
                }

                return result;


            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
