using GreenSale.Dtos.Dtos.BuyerPost;
using GreenSale.ViewModels.Models;
using GreenSale.ViewModels.Models.BuyerPosts;
using GreenSale.ViewModels.Models.SellerPosts;
using GreenSale.ViewModels.Models.Storages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenSale.Integrated.Interfaces.BuyerPosts
{
    public interface IBuyerPostService
    {
        public Task<List<BuyerPost>> GetAllAsync();
        public Task<long> CountAsync();  
        public Task<BuyerPostGetById> GetByIdAsync(long postId);
        public Task<List<BuyerPost>> GetAllUserId(long userId);
        public Task<bool> DeleteAsync(long postId);
        public Task<bool> DeleteImageAsync(long imageId);
        public Task<bool> CreateAsync(BuyerPostCreateDto dto);
        public Task<bool> UpdateAsync(long postId, BuyerPostUpdateDto dto);
        public Task<bool> UpdateImageAsync(long imageId, string dto);
        public Task<BuyerPostSearch> SearchAsync(string title);
        public Task<long> CountAgreeAsync();
        public Task<long> CountNewAsync();
        public Task<List<PostCreatedAt>> BuyerDaylilyCreatedAsync(int day);
        public Task<List<PostCreatedAt>> BuyerMonthlyCreatedAsync(int month);
        public Task<bool> UpdateStatusAsync(long postId, int status);
        public Task<bool> UpdateStartAsync(long postId, int start);
    }
}
