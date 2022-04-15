using DiaryApplication.Models;
using DiaryApplication.Web.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiaryApplication.Web.Services
{
    public class DiaryService : IDiaryService
    {
        private readonly ApplicationDbContext _context;
        public DiaryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreatePostAsync(DiaryPostEntity diaryPost)
        {
            try
            {
                _context.Add(diaryPost);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeletePostAsync(DiaryPostEntity diaryPost)
        {
            try
            {
                _context.DiaryPosts.Remove(diaryPost);
                await _context.SaveChangesAsync();
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

        public async Task<List<DiaryPostEntity>> GetAllPostsAsync(string userId)
        {
            return await _context.DiaryPosts.Include(d => d.DiaryApplicationUser).Where(m => m.UserId == userId).ToListAsync();
        }
      

        public async Task<DiaryPostEntity> GetPostById(int id,string userId)
        {
            return await _context.DiaryPosts
                .Include(d => d.DiaryApplicationUser)
                .FirstOrDefaultAsync(m => m.Id == id && m.UserId == userId);
        }

        public async Task<bool> UpdatePostAsync(DiaryPostEntity diaryPost)
        {
            try
            {
                _context.Update(diaryPost);
                await _context.SaveChangesAsync();
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

    }
}
