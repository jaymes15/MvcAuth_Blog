using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcAuthNBlog.Data;
using MvcAuthNBlog.Models;

namespace MvcAuthNBlog.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            return View(await _context.Category.ToListAsync());
        }

        // GET: Categories/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Category category = await _context.Category
                .SingleOrDefaultAsync(m => m.ID == id);
            if (category == null)
            {
                return NotFound();
            }
            CategoryBlogViewModel viewModel = await GetCategoryBlogViewModelFromCategory(category);
            //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); will give the user's userId
            var userName = User.FindFirstValue(ClaimTypes.Name); 
            ViewData["userid"] = userName;
            return View(viewModel);
        }

        

        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Details([Bind("CategoryID,AuthorID,ArticleTitle,ArticlePost,PublishDate")] CategoryBlogViewModel viewModel) 
        {
            if (ModelState.IsValid)
            {
                Blog blog = new Blog();
                blog.AuthorID = viewModel.AuthorID;
                blog.ArticleTitle = viewModel.ArticleTitle;
                blog.ArticlePost = viewModel.ArticlePost;
                blog.PublishDate = viewModel.PublishDate;


                Category category = await _context.Category
               .SingleOrDefaultAsync(m => m.ID == viewModel.CategoryID);

                if (category == null)
                {
                    return NotFound();
                }

                blog.Category = category;
                _context.Blog.Add(blog);
                await _context.SaveChangesAsync();

               viewModel = await GetCategoryBlogViewModelFromCategory(category);

            }
            return View(viewModel);
        }


        private async Task<CategoryBlogViewModel> GetCategoryBlogViewModelFromCategory(Category category)
        {
            CategoryBlogViewModel viewModel = new CategoryBlogViewModel();
            viewModel.Category = category;
            List<Blog> blogs = await _context.Blog
                .Where(m => m.Category == category).ToListAsync();
            viewModel.Blogs = blogs;
            return viewModel;

        }



        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("ID,CategoryName")] Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Categories/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Category.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("ID,CategoryName")] Category category)
        {
            if (id != category.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Categories/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Category
                .FirstOrDefaultAsync(m => m.ID == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _context.Category.FindAsync(id);
            _context.Category.Remove(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            return _context.Category.Any(e => e.ID == id);
        }
    }
}
