using GreenSale.Dtos.Dtos.Storages;
using GreenSale.ViewModels.Models;
using GreenSale.ViewModels.Models.Storages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenSale.Integrated.Interfaces.Storages
{
    public interface IStorageService
    {
        public Task<List<Storage>> GetAllAsync();
        public Task<long> CountAsync();
        public Task<bool>CreateAsync(StorageDto dto);
        public Task<List<Storage>> GetAllUserId(long userId);
        public Task<bool> DeleteAsync(long storageId);
        public Task<StorageGetById> GetByIdAsync(long storageId);
        public Task<bool> UpdateAsync(long storageId, StorageUpdateDto dto);
        public Task<StorageSearchViewModel> SearchAsync(string info);

        public Task<bool> UpdateImageStorageAsync(StorageImageDto dto);
        public Task<List<PostCreatedAt>> StorageDaylilyCreatedAsync(int day);
        public Task<List<PostCreatedAt>> StorageMonthlyCreatedAsync(int month);
        public Task<bool> UpdateStartAsync(long postId, int start);
    }
}
