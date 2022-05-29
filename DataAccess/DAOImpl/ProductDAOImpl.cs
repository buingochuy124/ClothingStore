using DataAccess.DAO;
using DataAccess.DTO;
using DataAccess.Libs;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DataAccess.DAOImpl
{
    public class ProductDAOImpl : IProductDAO
    {
        public List<ProductDTO> Products_GetList()
        {
            var result = new List<ProductDTO>();
            try
            {
                var sqlconn = ConnectDB.GetSqlConnection();

                SqlCommand cmd = new SqlCommand("SP_GetListProduct", sqlconn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;


                var read = cmd.ExecuteReader();
                while (read.Read())
                {
                    result.Add(new ProductDTO
                    {

                        ProductID = int.Parse(read["ProductID"].ToString()),
                        ProductName = read["ProductName"].ToString(),
                        Cost = double.Parse(read["Cost"].ToString()),
                        ProductDescription = read["ProductDescription"].ToString(),
                        ProductImageURL = read["ProductImageURL"].ToString(),
                        CategoryID = int.Parse(read["CategoryID"].ToString()),
                    });
                }

                return result;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<ProductDTO> Products_GetListByPage(int? PageNumber, int? NumberPerPage)
        {
            var result = new List<ProductDTO>();
            try
            {
                var sqlconn = ConnectDB.GetSqlConnection();

                SqlCommand cmd = new SqlCommand("SP_GetProductPagination", sqlconn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_PageNumber", PageNumber);
                cmd.Parameters.AddWithValue("@_NumberPerPage", NumberPerPage);


                var read = cmd.ExecuteReader();
                while (read.Read())
                {
                    result.Add(new ProductDTO
                    {
                        ProductID = int.Parse(read["ProductID"].ToString()),
                        ProductName = read["ProductName"].ToString(),
                        Cost = double.Parse(read["Cost"].ToString()),
                        ProductDescription = read["ProductDescription"].ToString(),
                        ProductImageURL = read["ProductImageURL"].ToString(),
                        CategoryID = int.Parse(read["CategoryID"].ToString()),
                    });
                }

                return result;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<ProductDTO> Products_SearchByCategory(int CategoryID)
        {
            var result = new List<ProductDTO>();
            try
            {
                var sqlconn = ConnectDB.GetSqlConnection();

                SqlCommand cmd = new SqlCommand("SP_SearchProductByCategory", sqlconn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_CategoryID", CategoryID);


                var read = cmd.ExecuteReader();
                while (read.Read())
                {
                    result.Add(new ProductDTO
                    {
                        ProductID = int.Parse(read["ProductID"].ToString()),
                        ProductName = read["ProductName"].ToString(),
                        Cost = double.Parse(read["Cost"].ToString()),
                        ProductDescription = read["ProductDescription"].ToString(),
                        ProductImageURL = read["ProductImageURL"].ToString(),
                        CategoryID = int.Parse(read["CategoryID"].ToString()),
                    });
                }

                return result;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<ProductDTO> Products_SearchByCategoryAndPage(int? PageNumber, int? NumberPerPage, int CategoryID)
        {
            var result = new List<ProductDTO>();
            try
            {
                var sqlconn = ConnectDB.GetSqlConnection();

                SqlCommand cmd = new SqlCommand("SP_GetProductByCategoryAndPagination", sqlconn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_PageNumber", PageNumber);
                cmd.Parameters.AddWithValue("@_NumberPerPage", NumberPerPage);
                cmd.Parameters.AddWithValue("@_CategoryID", CategoryID);


                var read = cmd.ExecuteReader();
                while (read.Read())
                {
                    result.Add(new ProductDTO
                    {
                        ProductID = int.Parse(read["ProductID"].ToString()),
                        ProductName = read["ProductName"].ToString(),
                        Cost = double.Parse(read["Cost"].ToString()),
                        ProductDescription = read["ProductDescription"].ToString(),
                        ProductImageURL = read["ProductImageURL"].ToString(),
                        CategoryID = int.Parse(read["CategoryID"].ToString()),
                    });
                }

                return result;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public int Product_Create(string ProductName, double Cost, string ProductDescription, string ProductImage, int CategoryID)
        {
            var result = 0;


            try
            {
                var sqlconn = ConnectDB.GetSqlConnection();

                SqlCommand cmd = new SqlCommand("SP_CreateProduct", sqlconn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@_ProductName", ProductName);
                cmd.Parameters.AddWithValue("@_Cost", Cost);
                cmd.Parameters.AddWithValue("@_ProductDescription", ProductDescription);
                cmd.Parameters.AddWithValue("@_ProductImage", ProductImage);
                cmd.Parameters.AddWithValue("@_CategoryID", CategoryID);


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

        public int Product_Delete(int ProductID)
        {
            var result = 0;


            try
            {
                var sqlconn = ConnectDB.GetSqlConnection();

                SqlCommand cmd = new SqlCommand("SP_DeleteProduct", sqlconn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@_ProductID", ProductID);
             


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

        public ProductDTO Product_GetDetail(int ProductID)
        {
            var result = new ProductDTO();
            try
            {
                var sqlconn = ConnectDB.GetSqlConnection();

                SqlCommand cmd = new SqlCommand("SP_GetProductDetail", sqlconn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_ProductID", ProductID);


                var read = cmd.ExecuteReader();
                while (read.Read())
                {
                    result = new ProductDTO
                    {
                        ProductID = int.Parse(read["ProductID"].ToString()),
                        ProductName = read["ProductName"].ToString(),
                        Cost = double.Parse(read["Cost"].ToString()),
                        ProductDescription = read["ProductDescription"].ToString(),
                        ProductImageURL = read["ProductImageURL"].ToString(),
                        CategoryID = int.Parse(read["CategoryID"].ToString()),
                    };
                }

                return result;

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
