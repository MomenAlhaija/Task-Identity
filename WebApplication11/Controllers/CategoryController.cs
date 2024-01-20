using Microsoft.AspNetCore.Mvc;
using System.Net.Sockets;
using System.Net.WebSockets;
using Identity.Models;
using Identity.Repositry;
using Identity.Repositry.Base;

namespace Identity.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IRepositry<Category> _mainRepositry;
        private readonly ILogger<CategoryController> _logger;   
        public CategoryController(IRepositry<Category> mainRepositry, ILogger<CategoryController> logger)
        {
            _mainRepositry = mainRepositry;
            _logger = logger;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                return View(await _mainRepositry.GetAllAsync());
            }
            catch {
                _logger.LogError("Error");
                return View();
            }
        }
        [HttpGet("Category/Create")]
        public  IActionResult Create()
        {
            return View();
        }
        [HttpPost("Category/Create")]
        public async Task<IActionResult> Create(Category category) {

            if (category == null)
                return Problem(detail: "The Category Can not be Empty or Null", statusCode: 400);

            else
            {
                if (ModelState.IsValid)
                {
                    if (_mainRepositry.selectOne(C => C.Name == category.Name) != null)
                        return Problem(detail: "The Category is already found", statusCode: 400);
                    _mainRepositry.AddAsync(category);
                    return RedirectToAction("Index");
                }
                return View(category);
            }
        }

        [HttpGet("Category/Edit/{Id}")]
        public IActionResult Edit(int ? Id)
        {
            if (Id == null) return null;
            else
            {
                var category =  _mainRepositry.FindById(Id);
                if (category == null)
                    return null;
                return View(category);
            }
        }
        [HttpPost("Category/Edit/{Id}")]
        public IActionResult Edit(int? id,Category? category)
        {
            if (id == null|| category==null) return null;
            else if (id != category.Id) return NotFound();
           
            _mainRepositry.UpdateOne(category);
            return RedirectToAction("Index");
        }


        [HttpGet("Category/Delete/{Id}")]
        public IActionResult Delete(int? Id)
        {
            if (Id == null) return null;
            var cat = _mainRepositry.FindById(Id);
            if(cat == null) return NotFound();
            return View(cat);
        }
        [HttpPost("Category/Delete/{Id}")]
        public IActionResult DeleteItem(int? Id)
        {
            if (Id == null) return null;
            var cat = _mainRepositry.FindById(Id);
            if (cat == null) return NotFound();
            _mainRepositry.Delete(cat);
            return RedirectToAction("Index");
        }

    }
}
