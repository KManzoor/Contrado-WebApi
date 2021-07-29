using Entites;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class ProductDAL
    {
        public ProductManagement GetProductByID(int? ID)
        {
            ProductManagement product = new ProductManagement();
            string ConString = System.Configuration.ConfigurationManager.ConnectionStrings["DBECommerce"].ConnectionString; ;
            using (SqlConnection connection = new SqlConnection(ConString))
            {
                // Creating SqlCommand objcet   
                SqlCommand cm = connection.CreateCommand();
                cm.CommandType = System.Data.CommandType.StoredProcedure;
                cm.Parameters.Add("@ProductID", SqlDbType.Int).Value = ID;
                cm.CommandText = "usp_GetProductByID";
                // Opening Connection  
                connection.Open();
                // Executing the SQL query  
                SqlDataReader sdr = cm.ExecuteReader();
                while (sdr.Read())
                {
                    product.CategoryID = Convert.ToInt32(sdr["CategoryID"]);
                    product.ProductID = Convert.ToInt32(sdr["ProductID"]);
                    product.PCategoryName = Convert.ToString(sdr["PCategoryName"]);
                    product.PName= Convert.ToString(sdr["PName"]);
                    product.PDesc = Convert.ToString(sdr["PDesc"]);


                }
                return product;
            }
        }
        public List<ProductManagement> GetProducts()
        {
            List<ProductManagement> products = new List<ProductManagement>();
            string ConString = System.Configuration.ConfigurationManager.ConnectionStrings["DBECommerce"].ConnectionString; ;
            using (SqlConnection connection = new SqlConnection(ConString))
            {
                // Creating SqlCommand objcet   
                SqlCommand cm = connection.CreateCommand();
                cm.CommandType = System.Data.CommandType.StoredProcedure;
                cm.Parameters.Add("@ProductID", SqlDbType.Int).Value = null;

                cm.CommandText = "usp_GetProductByID";
                // Opening Connection  
                connection.Open();
                // Executing the SQL query  
                SqlDataReader sdr = cm.ExecuteReader();
                ProductManagement product = null;
                while (sdr.Read())
                {
                    product = new ProductManagement();
                    product.CategoryID = Convert.ToInt32(sdr["CategoryID"]);
                    product.ProductID = Convert.ToInt32(sdr["ProductID"]);
                    product.PCategoryName = Convert.ToString(sdr["PCategoryName"]);
                    product.PName = Convert.ToString(sdr["PName"]);
                                        product.PName = Convert.ToString(sdr["PName"]);

                    products.Add(product);

                }
                return products;
            }
        }
        public ProductManagement SaveProduct(ProductManagement product)
        {
            string ConString = System.Configuration.ConfigurationManager.ConnectionStrings["DBECommerce"].ConnectionString; ;
            using (SqlConnection connection = new SqlConnection(ConString))
            {
                // Creating SqlCommand objcet   
                SqlCommand cm = connection.CreateCommand();
                cm.CommandType = System.Data.CommandType.StoredProcedure;
                cm.Parameters.Add("@ProductID", SqlDbType.Int).Value = product.ProductID;
                cm.Parameters["@ProductID"].Direction = ParameterDirection.InputOutput;
                cm.Parameters.Add("@CategoryID", SqlDbType.Int).Value = product.CategoryID;
                cm.Parameters.Add("@PName", SqlDbType.VarChar).Value = product.PName;
                cm.Parameters.Add("@PDesc", SqlDbType.VarChar).Value = product.PDesc;
                cm.CommandText = "usp_InsertUpdateProduct";
                // Opening Connection  
                connection.Open();
                cm.ExecuteNonQuery();
                product.ProductID = Convert.ToInt32(cm.Parameters["@ProductID"].Value);

                
                return product;
            }
        }

        public bool DeleteProductWithAttributes(int productId)
        {
            string ConString = System.Configuration.ConfigurationManager.ConnectionStrings["DBECommerce"].ConnectionString; ;
            using (SqlConnection connection = new SqlConnection(ConString))
            {
                // Creating SqlCommand objcet   
                SqlCommand cm = connection.CreateCommand();
                cm.CommandType = System.Data.CommandType.StoredProcedure;
                cm.Parameters.Add("@ProductID", SqlDbType.Int).Value = productId;
                cm.CommandText = "usp_DeleteProductWithAttributes";
                // Opening Connection  
                connection.Open();
                cm.ExecuteNonQuery();


                return true;
            }
        }


        public List<ProductAttribute> GetProductAttributes(ProductManagement product)
        {
            List<ProductAttribute> attributes = new List<ProductAttribute>();
            string ConString = System.Configuration.ConfigurationManager.ConnectionStrings["DBECommerce"].ConnectionString; ;
            using (SqlConnection connection = new SqlConnection(ConString))
            {
                // Creating SqlCommand objcet   
                SqlCommand cm = connection.CreateCommand();
                cm.CommandType = System.Data.CommandType.StoredProcedure;
                cm.Parameters.Add("@ProductID", SqlDbType.Int).Value = product.ProductID;
                cm.Parameters.Add("@CategoryID", SqlDbType.Int).Value = product.CategoryID;

                cm.CommandText = "usp_GetProductCategoryAttributeofProduct";
                // Opening Connection  
                connection.Open();
                // Executing the SQL query  
                SqlDataReader sdr = cm.ExecuteReader();
                ProductAttribute attribute = null;
                while (sdr.Read())
                {
                    attribute = new ProductAttribute();
                    attribute.AttributeId  = Convert.ToInt32(sdr["AttributeId"]);
                    attribute.AttributeName = Convert.ToString(sdr["AttributeName"]);
                    attribute.AttributeValue = Convert.ToString(sdr["AttributeValue"]);
                   

                    attributes.Add(attribute);

                }
                return attributes;
            }
        }

        public bool SaveProductAttributes(List<ProductAttribute> productAttributes)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ProductID", typeof(int));
            dt.Columns.Add("AttributeID", typeof(int));
            dt.Columns.Add("AttributeValue", typeof(string));
            foreach (ProductAttribute pa in productAttributes)
            {
                if (pa.AttributeId > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr["ProductID"] = pa.ProductID;
                    dr["AttributeID"] = pa.AttributeId;
                    dr["AttributeValue"] = pa.AttributeValue;
                    dt.Rows.Add(dr);
                }

            }
            string ConString = System.Configuration.ConfigurationManager.ConnectionStrings["DBECommerce"].ConnectionString; ;
            using (SqlConnection connection = new SqlConnection(ConString))
            {
                // Creating SqlCommand objcet   
                SqlCommand cm = connection.CreateCommand();
                cm.CommandType = System.Data.CommandType.StoredProcedure;
                cm.Parameters.Add("@productAttributes", SqlDbType.Structured).Value = dt;
                cm.CommandText = "usp_InsertUpdateProductAttributes";
                // Opening Connection  
                connection.Open();
                cm.ExecuteNonQuery();
                connection.Close();



            }
            return true;
        }
    }
}
