using System.Linq;
using System.Web.Mvc;
using Webbanhang.Context;

namespace Webbanhang.Controllers
{
    public class CategoryController : Controller
    {
        trung_webEntities objtrung_webEntities = new trung_webEntities();
        // GET: Category
        public ActionResult Index()
        {
            var lstCategory = objtrung_webEntities.Categories.ToList();
            return View(lstCategory);
        }
        public ActionResult ProductCategory(int Id)
        {
            var lstProduct = objtrung_webEntities.Products.Where(n => n.CategoryId == Id).ToList();
            return View(lstProduct);
        }
    }
}