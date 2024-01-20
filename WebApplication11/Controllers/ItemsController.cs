using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Identity.Data;
using Identity.Models;
using Identity.Repositry.Base;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace TestCoreApp.Controllers
{
    public class ItemsController : Controller
    {
        private readonly IRepositry<Item> _mainReposirty;
        private readonly IdentityContext _db;
        private readonly IHostingEnvironment _host;
        public ItemsController(IdentityContext db, IRepositry<Item> mainReposirty,
            IHostingEnvironment host
            )
        {
            _host = host;   
            _db = db;
            _mainReposirty =mainReposirty;
        }   
  
        public async Task<IActionResult> Index()
        {

            IEnumerable<Item> itemsList = await _mainReposirty.GetAllAsync(new string[] { "Category" });
            return View(itemsList);
        }

        //GET
        public IActionResult New()
        {
            createSelectList();
            return View(); 
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult New(Item item)
        {
            if (item.Name == "100")
            {
                ModelState.AddModelError("Name", "Name can't equal 100");
            }
            if (ModelState.IsValid)
            {
                string fileName = "";
                if (item.ClientFile != null)
                {
                    string upload = Path.Combine(_host.WebRootPath, "Images");
                    fileName = item.ClientFile.FileName;
                    string fullpath=Path.Combine(upload, fileName);
                    item.ClientFile.CopyTo(new FileStream(fullpath, FileMode.Create));
                }
                item.ImagePath = fileName;
                _mainReposirty.Add(item);
                TempData["successData"] = "Item has been added successfully";
                return RedirectToAction("Index");
            }
            else
            {
                return View(item);
            }
        }

        public void createSelectList(int selectId = 1)
        {
            //List<Category> categories = new List<Category> {
            //  new Category() {Id = 0, Name = "Select Category"},
            //  new Category() {Id = 1, Name = "Computers"},
            //  new Category() {Id = 2, Name = "Mobiles"},
            //  new Category() {Id = 3, Name = "Electric machines"},
            //};
            List<Category> categories = _db.Categories.ToList();
            SelectList listItems = new SelectList(categories, "Id", "Name", selectId);
            ViewBag.CategoryList = listItems;
        }

        //GET
        public IActionResult Edit(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            var item =_mainReposirty.FindById(Id);
            if (item == null)
            {
                return NotFound();
            }
            createSelectList(item.CategoryId);
            return View(item);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Item item)
        {
            if (item.Name == "100")
            {
                ModelState.AddModelError("Name", "Name can't equal 100");
            }
            if (ModelState.IsValid)
            {
                _mainReposirty.UpdateOne(item);
                TempData["successData"] = "Item has been updated successfully";
                return RedirectToAction("Index");
            }
            else
            {
                return View(item);
            }
        }

        //GET
        public IActionResult Delete(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            var item = _mainReposirty.FindById(Id);
            if (item == null)
            {
                return NotFound();
            }
            createSelectList(item.CategoryId);
            return View(item);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteItem(int? Id)
        {
            var item =_mainReposirty.FindById(Id);
            if (item == null)
            {
                return NotFound();
            }
            _mainReposirty.Delete(item);
            TempData["successData"] = "Item has been deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
