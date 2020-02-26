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
    public class BlogsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BlogsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Blogs
        public async Task<IActionResult> Index()
        {
            return View(await _context.Blog.Include( c => c.Category).ToListAsync());
        }

        // GET: Blogs/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Blog blog = await _context.Blog
                .FirstOrDefaultAsync(m => m.ID == id);
            if (blog == null)
            {
                return NotFound();
            }
            BlogCommentViewModel viewModel = await GetBlogCommentViewModelFromBlog(blog);
            //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); will give the user's userId
            var userName = User.FindFirstValue(ClaimTypes.Name);
            ViewData["userid"] = userName;
            return View(viewModel);
        }


        private async Task<BlogCommentViewModel> GetBlogCommentViewModelFromBlog(Blog blog)
        {
            BlogCommentViewModel viewModel = new BlogCommentViewModel();
            viewModel.Blog = blog;
            List<Comment> comments = await _context.Comment
                .Where(m => m.BlogComment == blog).ToListAsync();
            viewModel.Comments = comments;
            return viewModel;

        }


        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Details([Bind("BlogID,CommentAuthorID,CommentPost,PublishDate")] BlogCommentViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Comment comment = new Comment();
                comment.CommentAuthorID = viewModel.CommentAuthorID;
                comment.CommentPost = viewModel.CommentPost;
                comment.PublishDate = viewModel.PublishDate;


               Blog blog= await _context.Blog
               .SingleOrDefaultAsync(m => m.ID == viewModel.BlogID);

                if (blog == null)
                {
                    return NotFound();
                }

                comment.BlogComment = blog;
                _context.Comment.Add(comment);
                await _context.SaveChangesAsync();

                viewModel = await GetBlogCommentViewModelFromBlog(blog);

            }
            return View(viewModel);
        }






        // GET: Blogs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Blogs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,AuthorID,ArticleTitle,ArticlePost,PublishDate")] Blog blog)
        {
            if (ModelState.IsValid)
            {
                _context.Add(blog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(blog);
        }

        // GET: Blogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = await _context.Blog.FindAsync(id);
            if (blog == null)
            {
                return NotFound();
            }
            return View(blog);
        }

        // POST: Blogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,AuthorID,ArticleTitle,ArticlePost,PublishDate")] Blog blog)
        {
            if (id != blog.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(blog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogExists(blog.ID))
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
            return View(blog);
        }

        // GET: Blogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = await _context.Blog
                .FirstOrDefaultAsync(m => m.ID == id);
            if (blog == null)
            {
                return NotFound();
            }

            return View(blog);
        }

        // POST: Blogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var blog = await _context.Blog.FindAsync(id);
            _context.Blog.Remove(blog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlogExists(int id)
        {
            return _context.Blog.Any(e => e.ID == id);
        }
    }
}
