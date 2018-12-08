using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Spice.Data;
using Spice.Models.ViewModels;

namespace Spice.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SubCategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public SubCategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        //Get INDEX
        public async Task<IActionResult> Index()
        {
            var subCategories = await _db.SubCategory.Include(s=>s.Category).ToListAsync();
            return View(subCategories);
        }

        //GET - CREATE
        public async Task<IActionResult> Create()
        {
            SubCategoryAndCategoryViewModel model = new SubCategoryAndCategoryViewModel()
            {
                CategoryList = await _db.Category.ToListAsync(),
                SubCategory = new Models.SubCategory(),
                SubCategoryList = await _db.SubCategory.OrderBy(p => p.Name).Select(p => p.Name).Distinct().ToListAsync()
            };

            return View(model);
        }

    }
}