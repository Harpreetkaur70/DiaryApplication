using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DiaryApplication.Models;
using DiaryApplication.Web.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using DiaryApplication.Web.Services;

namespace DiaryApplication.Web.Controllers
{
    [Authorize]
    public class DiaryController : Controller
    {
        private readonly UserManager<DiaryApplicationUser> _userManager;
        private readonly IDiaryService _diaryService;
        public DiaryController(UserManager<DiaryApplicationUser> userManager, IDiaryService diaryService)
        {
            _userManager = userManager;
            _diaryService = diaryService;
        }

        // GET: Diaryontroller
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var posts =  await _diaryService.GetAllPostsAsync(user.Id);
            return View(posts);

        }

        // GET: Diaryontroller/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);
            var diaryPostEntity = await _diaryService.GetPostById(Convert.ToInt32(id),user.Id);
            if (diaryPostEntity == null)
            {
                return NotFound();
            }

            return View(diaryPostEntity);
        }

        // GET: Diaryontroller/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Diaryontroller/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Content,ImageUrl")] DiaryPostEntity diaryPostEntity)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                diaryPostEntity.UserId = user.Id;
                diaryPostEntity.CreatedDate = DateTime.UtcNow;
                await _diaryService.CreatePostAsync(diaryPostEntity);
                return RedirectToAction(nameof(Index));
            }
            return View(diaryPostEntity);
        }

        // GET: Diaryontroller/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = await _userManager.GetUserAsync(User);
            var diaryPostEntity = await _diaryService.GetPostById(Convert.ToInt32(id), user.Id);
            if (diaryPostEntity == null)
            {
                return NotFound();
            }
            return View(diaryPostEntity);
        }

        // POST: Diaryontroller/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Content,ImageUrl,CreateDate,UserId")] DiaryPostEntity diaryPostEntity)
        {
            if (id != diaryPostEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _diaryService.UpdatePostAsync(diaryPostEntity);
                }
                catch (Exception)
                {
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(diaryPostEntity);
        }

        // GET: Diaryontroller/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = await _userManager.GetUserAsync(User);
            var diaryPostEntity = await _diaryService.GetPostById(Convert.ToInt32(id), user.Id);
            if (diaryPostEntity == null)
            {
                return NotFound();
            }

            return View(diaryPostEntity);
        }

        // POST: Diaryontroller/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var diaryPostEntity = await _diaryService.GetPostById(Convert.ToInt32(id), user.Id);
            await _diaryService.DeletePostAsync(diaryPostEntity);
            return RedirectToAction(nameof(Index));
        }
    }
}
