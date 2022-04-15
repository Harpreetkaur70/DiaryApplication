using DiaryApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiaryApplication.Web.Services
{
    public interface IDiaryService
    {
        public Task<List<DiaryPostEntity>> GetAllPostsAsync(string userId);
        public Task<DiaryPostEntity> GetPostById(int id, string userId);
        public Task<bool> CreatePostAsync(DiaryPostEntity diaryPost);
        public Task<bool> UpdatePostAsync(DiaryPostEntity diaryPost);
        public Task<bool> DeletePostAsync(DiaryPostEntity diaryPost);
    }
}
