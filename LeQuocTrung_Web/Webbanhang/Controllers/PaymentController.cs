using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Webbanhang.Context;
using Webbanhang.Models;

namespace Webbanhang.Controllers
{
    public class PaymentController : Controller
    {
        trung_webEntities objtrung_webEntities = new trung_webEntities();
        // GET: Payment
        public ActionResult Index()
        {
            if (Session["idUser"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                var lstCart = (List<CartModel>)Session["Cart"];
                Order objOrder = new Order();
                objOrder.Name = "DonHang" + DateTime.Now.ToString("yyyyMMddHHmmss");
                objOrder.UserId = int.Parse(Session["IdUser"].ToString());
                objOrder.CreatedOnUtc = DateTime.Now;
                objOrder.Status = 1;
                objtrung_webEntities.Orders.Add(objOrder);
                objtrung_webEntities.SaveChanges();


                int intOrderId = objOrder.Id;
                List<OrderDetail> lstOrderDetail = new List<OrderDetail>();
                foreach (var item in lstCart)
                {
                    OrderDetail obj = new OrderDetail();
                    obj.Quantity = item.Quantity;
                    obj.OrderId = intOrderId;
                    obj.ProductId = item.Product.Id;
                    lstOrderDetail.Add(obj);
                }
                objtrung_webEntities.OrderDetails.AddRange(lstOrderDetail);
                objtrung_webEntities.SaveChanges();
            }
            
            return View();
        }
    }
}
