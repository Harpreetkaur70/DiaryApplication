using DiaryApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiaryApplication.Web.Services
{
    public interface IDiaryService
    {
        Task<List<DiaryPostEntity>> GetAllPostsAsync(string userId);
        Task<DiaryPostEntity> GetPostById(int id, string userId);
        Task<bool> CreatePostAsync(DiaryPostEntity diaryPost);
        Task<bool> UpdatePostAsync(DiaryPostEntity diaryPost);
        Task<bool> DeletePostAsync(DiaryPostEntity diaryPost);
    }
}
