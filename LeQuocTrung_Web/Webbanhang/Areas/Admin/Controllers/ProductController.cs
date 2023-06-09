﻿using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Webbanhang.Context;
using static Webbanhang.Common;

namespace Webbanhang.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        trung_webEntities objtrung_webEntities = new trung_webEntities();

        // GET: Admin/Product
        public ActionResult Index(string currentFilter, string SearchString, int? page)
        {
            var lstProduct = new List<Product>();
            if (SearchString != null)
            {
                page = 1;
            }
            else
            {
                SearchString = currentFilter;
            }
            if (!string.IsNullOrEmpty(SearchString))
            {
                lstProduct = objtrung_webEntities.Products.Where(n => n.Name.Contains(SearchString)).ToList();
            }
            else
            {
                lstProduct = objtrung_webEntities.Products.ToList();
            }
            ViewBag.CurrentFilter = SearchString;
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            lstProduct = lstProduct.OrderByDescending(n => n.Id).ToList();
            return View(lstProduct.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public ActionResult Create()
        {

            this.LoadData();
            return View();
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(Product objProduct)
        {
            this.LoadData();
            if (ModelState.IsValid)

            {
                try
                {
                    if (objProduct.ImageUpload != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(objProduct.ImageUpload.FileName);
                        string extension = Path.GetExtension(objProduct.ImageUpload.FileName);
                        fileName = fileName + extension;
                        objProduct.Avartar = fileName;
                        objProduct.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), fileName));
                    }
                    objProduct.CreatedOnUtc = DateTime.Now;
                    objtrung_webEntities.Products.Add(objProduct);
                    objtrung_webEntities.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(objProduct);
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            var objProduct = objtrung_webEntities.Products.Where(n => n.Id == id).FirstOrDefault();
            return View(objProduct);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var objProduct = objtrung_webEntities.Products.Where(n => n.Id == id).FirstOrDefault();
            return View(objProduct);
        }
        [HttpPost]
        public ActionResult Delete(Product objPro)
        {
            var objProduct = objtrung_webEntities.Products.Where(n => n.Id == objPro.Id).FirstOrDefault();
            objtrung_webEntities.Products.Remove(objProduct);
            objtrung_webEntities.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            this.LoadData();

            var objProduct = objtrung_webEntities.Products.Where(n => n.Id == id).FirstOrDefault();
            return View(objProduct);
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(int id, Product objProduct)
       {
          if (objProduct.ImageUpload != null)
        {
          string fileName = Path.GetFileNameWithoutExtension(objProduct.ImageUpload.FileName);
          string extension = Path.GetExtension(objProduct.ImageUpload.FileName);
          fileName = fileName + extension;
          objProduct.Avartar = fileName;
          objProduct.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), fileName));
         }
          objtrung_webEntities.Entry(objProduct).State = EntityState.Modified;
          objtrung_webEntities.SaveChanges();
          return RedirectToAction("Index");
         }
        void LoadData()
        {
            Common objCommon = new Common();
            var lstCat = objtrung_webEntities.Categories.ToList();
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dtCategory = converter.ToDataTable(lstCat);
            ViewBag.ListCategory = objCommon.ToSelectList(dtCategory, "Id", "Name");


            var lstBrand = objtrung_webEntities.Brands.ToList();
            DataTable dtBrand = converter.ToDataTable(lstBrand);
            ViewBag.ListBrand = objCommon.ToSelectList(dtBrand, "Id", "Name");


            List<ProductType> lstProductType = new List<ProductType>();
            ProductType objproductType = new ProductType();
            objproductType.Id = 01;
            objproductType.Name = "Giảm giá sốc";
            lstProductType.Add(objproductType);


            objproductType = new ProductType();
            objproductType.Id = 02;
            objproductType.Name = "Đề Xuất";
            lstProductType.Add(objproductType);

            


            DataTable dtProductType = converter.ToDataTable(lstProductType);
            ViewBag.ProductType = objCommon.ToSelectList(dtProductType, "Id", "Name");
        }
    }
}