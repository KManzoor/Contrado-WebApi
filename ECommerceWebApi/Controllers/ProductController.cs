using DataAccessLayer;
using Entites;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ECommerceWebApi.Controllers
{
    [RoutePrefix("Api/Product")]
    [EnableCors(origins: "http://localhost:59640", headers: "Content-Type", methods: "*")]

    public class ProductController : ApiController
    {
        [HttpGet]
        [Route("GetProducts")]
        public IEnumerable<ProductManagement> GetProducts()
        {
            List<ProductManagement> products = new List<ProductManagement>();

            try
            {
                ProductDAL dal = new ProductDAL();

                products=dal.GetProducts();
                
                
                return products;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("GetProductByID")]
        public ProductManagement GetProductByID(int ID)
        {
            ProductManagement product = new ProductManagement();

            try
            {
                ProductDAL dal = new ProductDAL();

                product= dal.GetProductByID(ID);
               

                return product;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]      
[Route("SaveProduct")]
        public IHttpActionResult SaveProduct(ProductManagement product)
        {
            
            if (ModelState.IsValid)
            {
                ProductDAL dal = new ProductDAL();
               product= dal.SaveProduct(product);
            }
            else
            {
               
            }
            return Ok(product);
        }

        [HttpGet]
        [Route("GetProductCategoryAttributeofProduct")]
        public List<ProductAttribute> GetProductCategoryAttributeofProduct(string product)
        {
            List<ProductAttribute> attributes = new List<ProductAttribute>();

            try
            {
                ProductDAL dal = new ProductDAL();

                attributes = dal.GetProductAttributes(JsonConvert.DeserializeObject<ProductManagement>(product));


                return attributes;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpDelete]
        [Route("DeleteProductWithDetails")]
        public IHttpActionResult DeleteProductWithDetails(int productId)
        {


            ProductDAL dal = new ProductDAL();
            return Ok(dal.DeleteProductWithAttributes(productId));

        }

        [HttpPost]
        [Route("SaveProductAttributes")]
        public IHttpActionResult SaveProductAttributes(List<ProductAttribute> productAttributes)
        {

           
                ProductDAL dal = new ProductDAL();
                return Ok(dal.SaveProductAttributes(productAttributes));
           
        }
        
    }
}
