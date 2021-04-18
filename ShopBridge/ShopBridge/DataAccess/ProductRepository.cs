using ShopBridge.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridge.DataAccess
{
    public class ProductRepository : IProductRepository
    {
        private string conn;
        public ProductRepository()
        {
            conn = System.Configuration.ConfigurationManager.AppSettings["DBConn"];
        }
        int IProductRepository.AddProduct(Product product)
        {
            int productId = 0;
            DataTable productDb = new DataTable();
            productDb.Columns.AddRange(new DataColumn[] {
                        new DataColumn("Id", typeof(int)),
                        new DataColumn("Name", typeof(string)),
                        new DataColumn("Description", typeof(string)),
                        new DataColumn("Price", typeof(decimal)),
                        new DataColumn("Count", typeof(int))
            });
            using (SqlConnection con = new SqlConnection(conn))
            {
                using (SqlCommand cmd = new SqlCommand("USPAddProduct"))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@Product", productDb);                  
                        cmd.CommandTimeout = 300;
                        if (con.State != ConnectionState.Open)
                            con.Open();
                        productId = cmd.ExecuteNonQuery();

                    }
                    finally
                    {
                        if (con.State != ConnectionState.Closed)
                        {
                            con.Close();
                        }
                    }
                }
            }

            return productId;
        }

        int IProductRepository.UpdateProduct(Product product)
        {
            int isUpdated = 0;
            DataTable productDb = new DataTable();
            productDb.Columns.AddRange(new DataColumn[] {
                        new DataColumn("Id", typeof(int)),
                        new DataColumn("Name", typeof(string)),
                        new DataColumn("Description", typeof(string)),
                        new DataColumn("Price", typeof(decimal)),
                        new DataColumn("Count", typeof(int))
            });
            using (SqlConnection con = new SqlConnection(conn)) 
            {
                using (SqlCommand cmd = new SqlCommand("USPUpdateProduct"))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@Product", productDb);
                        cmd.CommandTimeout = 300;
                        if (con.State != ConnectionState.Open)
                            con.Open();
                        cmd.ExecuteNonQuery();
                        isUpdated = 1;

                    }
                    finally
                    {
                        if (con.State != ConnectionState.Closed)
                        {
                            con.Close();
                        }
                    }
                }
            }

            return isUpdated;
        }
        int IProductRepository.DeleteProduct(Product product)
        {
            int isDeleted = 0;           
            using (SqlConnection con = new SqlConnection(conn))
            {
                using (SqlCommand cmd = new SqlCommand("USPDeleteProduct"))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@ProductId", product.Id);
                        cmd.CommandTimeout = 300;
                        if (con.State != ConnectionState.Open)
                            con.Open();
                        cmd.ExecuteNonQuery();
                        isDeleted = 1;

                    }
                    finally
                    {
                        if (con.State != ConnectionState.Closed)
                        {
                            con.Close();
                        }
                    }
                }
            }

            return isDeleted;
        }

        List<Product> IProductRepository.GetAll()
        {
            List<Product> results = new List<Product>();
            using (SqlConnection con = new SqlConnection(conn))
            {
                using (SqlCommand cmd = new SqlCommand("USPGetAllProduct"))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con;
                        cmd.CommandTimeout = 300;
                        if (con.State != ConnectionState.Open)
                            con.Open();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            foreach(DataRow row in ds.Tables[0].Rows)
                            {
                                results.Add(new Product
                                {
                                    Id = Convert.ToInt32(row["Id"]),
                                    Name = row["Name"].ToString(),
                                    Description = row["Description"].ToString(),
                                    Price = Convert.ToDouble(row["Price"]),
                                    Count = Convert.ToInt32(row["Count"])
                                });
                            }

                        }

                    }
                    finally
                    {
                        if (con.State != ConnectionState.Closed)
                        {
                            con.Close();
                        }
                    }
                }
            }
            return results;
        }

    }
}
