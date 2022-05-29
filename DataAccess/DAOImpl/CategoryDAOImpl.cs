using DataAccess.DAO;
using DataAccess.DTO;
using DataAccess.Libs;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DataAccess.DAOImpl
{
    public class CategoryDAOImpl : ICategoryDAO
    {
        public List<CategoryDTO> Categories_GetList()
        {
            var result = new List<CategoryDTO>();
            try
            {
                var sqlconn = ConnectDB.GetSqlConnection();

                SqlCommand cmd = new SqlCommand("SP_GetListCategory", sqlconn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                var read = cmd.ExecuteReader();
                while (read.Read())
                {
                    result.Add(new CategoryDTO
                    {

                        CategoryID = int.Parse(read["CategoryID"].ToString()),
                        CategoryName = read["CategoryName"].ToString(),

                    });
                }
                return result;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public int Category_CreateCategory(string CategoryName)
        {
            var result = 0;

            try
            {
                var sqlconn = ConnectDB.GetSqlConnection();

                SqlCommand cmd = new SqlCommand("SP_CreateCategory", sqlconn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@_CategoryName", CategoryName);
                



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

        public CategoryDTO Category_GetDetailByID(int CategoryID)
        {
            var result = new CategoryDTO();
            try
            {
                var sqlconn = ConnectDB.GetSqlConnection();
                SqlCommand cmd = new SqlCommand("SP_CategoryGetDetailByID", sqlconn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_CategoryID", CategoryID);


                var read = cmd.ExecuteReader();
                while (read.Read())
                {
                    result = (new CategoryDTO
                    {
                        CategoryID = int.Parse(read["CategoryID"].ToString()),
                        CategoryName = read["CategoryName"].ToString()
                    });
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
