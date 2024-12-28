using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Web_Api_6.DAL;
using Web_Api_6.Models;

namespace Web_Api_6.Controllers
{
    public class ProductController : ApiController
    {
        private readonly ProductContext context;
        public ProductController()
        {
            context = new ProductContext();
        }
        [HttpGet]
        public HttpResponseMessage GetAllProducts()
        {
            try
            {
                List<Product> plist = context.Products.ToList();
                if (plist.Count > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, plist);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NoContent, "No Product to Display");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpGet]
        public HttpResponseMessage GetProductById(int id)
        {
            try
            {
                Product p = context.Products.Find(id);
                if (p == null) 
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound,"With the given id "+id+"No Product Found");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Found, p);
                }
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpDelete]
        public HttpResponseMessage DeleteProduct(int id)
        {
            try
            {
                Product p = context.Products.Find(id);
                if (p == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "with the Given Id " + id + "No Product Found To Delete");
                }
                else
                {
                    context.Products.Remove(p);
                    context.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, p);
                }
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpPost]
        public HttpResponseMessage CreateProduct(Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    context.Products.Add(product);
                    context.SaveChanges();
                    Product x = context.Products.Find(product.ProductId);
                    return Request.CreateResponse(HttpStatusCode.Created, x);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid Input");
                }
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpPut]
        public HttpResponseMessage UpdateProduct(int id,Product product)
        {
            try
            {
                if (id == product.ProductId)
                {
                    if (ModelState.IsValid)
                    {
                        Product p = context.Products.Find(product.ProductId);
                        if (p != null)
                        {
                            p.ProductName = product.ProductName;
                            p.Quantity = product.Quantity;
                            p.Price = product.Price;
                            context.SaveChanges();
                            Product x = context.Products.Find(id);
                            return Request.CreateResponse(HttpStatusCode.OK, x);
                        }
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.NotFound, "With the given id " + id + "No Product Found to update");
                        }
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid Product Id");
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Id Mismatch");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpPatch]
        public HttpResponseMessage UpdateProductPrice(int id, Product product)
        {
            try
            {
                if (id == product.ProductId)
                {
                    if (ModelState.IsValid)
                    {
                        Product p = context.Products.Find(product.ProductId);
                        if (p != null)
                        {
                            p.Price = product.Price;
                            context.SaveChanges();
                            Product x = context.Products.Find(id);
                            return Request.CreateResponse(HttpStatusCode.OK, x);
                        }
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.NotFound, "With th give id " + id + " no Product Found to update");
                        }
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid Product Data");
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Id mismatch");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
