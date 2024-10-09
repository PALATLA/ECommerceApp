using Microsoft.AspNetCore.Mvc;
using ECommerceApp.Data;
using ECommerceApp.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PagedList;

namespace ECommerceApp.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        //search
        //public IActionResult Index(string search)
        //{
        //    var products = string.IsNullOrEmpty(search) ?
        //        _context.Products.ToList() :
        //        _context.Products.Where(p => p.Name.Contains(search)).ToList();
        //    return View(products);
        //}



        

public IActionResult Index(string searchName, decimal? searchPrice, int? page)
    {
        var userEmail = User.Identity.Name; // Assuming you have user authentication set up
        var user = _context.Users.FirstOrDefault(u => u.Email == userEmail);
        ViewBag.UserName = user?.Email ?? "User"; // Use email as a placeholder for the name

        int pageSize = 10; // Number of items per page
        int pageNumber = (page ?? 1); // Default to page 1 if no page number is specified

        var products = _context.Products.AsQueryable();

        if (!string.IsNullOrEmpty(searchName))
        {
            products = products.Where(p => p.Name.Contains(searchName));
        }

        if (searchPrice.HasValue)
        {
            products = products.Where(p => p.Price == searchPrice.Value);
        }

        var pagedProducts = products.OrderBy(p => p.ProductId).ToPagedList(pageNumber, pageSize);

        ViewBag.SearchName = searchName;
        ViewBag.SearchPrice = searchPrice;

        return View(pagedProducts);
    }




    //main index befor adding pageination
    //public IActionResult Index()
    //{
    //    var userEmail = User.Identity.Name; 
    //    var user = _context.Users.FirstOrDefault(u => u.Email == userEmail);
    //    ViewBag.UserName = user?.Email ?? "User"; 
    //    var products = _context.Products.ToList();
    //    return View(products);
    //}


    [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Product added successfully!";
                return RedirectToAction("Index");
            }
            return View(product);
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Update(product);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Product updated successfully!";
                return RedirectToAction("Index");
            }
            return View(product);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }


        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = _context.Products.Find(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        //this is delete with alert message code 
        //[HttpPost, ActionName("Delete")]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var product = _context.Products.Find(id);
        //    _context.Products.Remove(product);
        //    await _context.SaveChangesAsync();
        //    TempData["SuccessMessage"] = "Product deleted successfully!";
        //    return RedirectToAction("Index");
        //}
    }
}









//2nd version
//using Microsoft.AspNetCore.Mvc;
//using ECommerceApp.Data;
//using System.Linq;

//namespace ECommerceApp.Controllers
//{
//    public class DashboardController : Controller
//    {
//        private readonly ApplicationDbContext _context;

//        public DashboardController(ApplicationDbContext context)
//        {
//            _context = context;
//        }

//        public IActionResult Index()
//        {
//            var products = _context.Products.ToList();
//            return View(products);
//        }
//    }
//}
