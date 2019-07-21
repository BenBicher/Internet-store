using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyStore.Models;
using MyStore.ViewModels;
using Microsoft.AspNet.Identity;
using System.IO;

namespace MyStore.Controllers
{
    public class ProductsController : Controller
    {
        private StoreDbModel db = new StoreDbModel();

        // GET: Products
        public ActionResult Index()
        {
            return View(db.Product.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            if (LoginViewModel.isLoged)
            {
                return View();
            }
            return RedirectToAction("index");
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,OwnerId,UserId,Title,ShortDescription,LongDescription,Date,Price,Picture1,Picture2,Picture3,State")] Product product, HttpPostedFileBase image1, HttpPostedFileBase image2, HttpPostedFileBase image3)
        {
            if (image1 != null && image1.ContentLength > 0)
            {
                // get the name of the file 
                var filename = Path.GetFileName(image1.FileName);
                //update the path to save files 
                var path = Path.Combine(Server.MapPath("~/images"), filename);
                // save the path of file in the "post.Image"
                product.Picture1 = "~/images/" + filename;

                //the files saved in the /images directory 
                image1.SaveAs(path);
            }
            if (image2 != null && image1.ContentLength > 0)
            {
                // get the name of the file 
                var filename = Path.GetFileName(image2.FileName);
                //update the path to save files 
                var path = Path.Combine(Server.MapPath("~/images"), filename);
                // save the path of file in the "post.Image"
                product.Picture2 = "~/images/" + filename;

                //the files saved in the /images directory 
                image2.SaveAs(path);
            }
            if (image3 != null && image3.ContentLength > 0)
            {
                // get the name of the file 
                var filename = Path.GetFileName(image3.FileName);
                //update the path to save files 
                var path = Path.Combine(Server.MapPath("~/images"), filename);
                // save the path of file in the "post.Image"
                product.Picture3 = "~/images/" + filename;

                //the files saved in the /images directory 
                image3.SaveAs(path);
            }
            product.OwnerId = User.Identity.GetUserId();
            product.Date = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Product.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,OwnerId,UserId,Title,ShortDescription,LongDescription,Date,Price,Picture1,Picture2,Picture3,State")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        public ActionResult AddToCart(int id)
        {
            int idToCheck = 0;
            string userId = User.Identity.GetUserId();
            var productIds = db.UserProducts.Where(P => P.UserRefId == userId).ToList();
            foreach (var _id in productIds)
            {
                var product = db.UserProducts.Where(p => p.ProductRefId == id).FirstOrDefault();
                if (product != null)
                {
                    var productss = int.TryParse(product.ProductRefId.ToString(), out idToCheck);
                    if (idToCheck == id)
                    {
                        return View("Index", db.Product.ToList());
                    }
                }
            }

            var userID = User.Identity.GetUserId();
            UserProduct mappingUserToProduct = new UserProduct()
            {
                ProductRefId = id,
                UserRefId = userID
            };
            db.UserProducts.Add(mappingUserToProduct);
            db.SaveChanges();
            return View("Index", db.Product.ToList());
        }

        public ActionResult PressToBuy()
        {
            string userId = User.Identity.GetUserId();
            var productIds = db.UserProducts.Where(P => P.UserRefId == userId).ToList();
            List<UserProduct> ids = productIds.ToList();
            foreach (var _id in ids)
            {
                var product = db.Product.Find(_id.ProductRefId);
                db.Product.Remove(product);
                db.UserProducts.Remove(_id);
                db.SaveChanges();
            }
            return RedirectToAction("index");
        }

        public ActionResult GetUserProducts()
        {
            List<Product> products = new List<Product>();
            string userId = User.Identity.GetUserId();
            var productIds = db.UserProducts.Where(P => P.UserRefId == userId).ToList();
            List<UserProduct> ids = productIds.ToList();
            var TotalPrice = 0;
            foreach (UserProduct id in ids)
            {
                var product = db.Product.Where(p => p.Id == id.ProductRefId).First();
                products.Add(product);
                TotalPrice += (int)products[products.Count - 1].Price;
            }
            if (User.Identity.Name != null && User.Identity.Name != "")
            {
                var UserLogedPrice = (TotalPrice) * 10 / 100;
                TotalPrice -= UserLogedPrice;
            }
            UserProductsViewModel model = new UserProductsViewModel()
            {
                products = products,
                TotalPrice = TotalPrice
            };
            return View(model);
        }

        public ActionResult DeleteProduct(int id)
        {
            var product = db.UserProducts.Where(i => i.Product.Id == id).FirstOrDefault();
            db.UserProducts.Remove(product);
            db.SaveChanges();

            return RedirectToAction("GetUserProducts");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
