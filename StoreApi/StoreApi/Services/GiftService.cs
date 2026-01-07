using StoreApi.DTOs;
using StoreApi.Models;
using StoreApi.Repositories;

namespace StoreApi.Services
{
    public class GiftService : IGiftService
    {
        private readonly IGiftRepository _repository;
        public GiftService(IGiftRepository repository)
        {
            this._repository = repository;
        }
        //קבלת כל המתנות
        public async Task<List<GiftDto>> GetAllGiftsAsync()
        {
            var gifts = await _repository.GetAllGiftsAsync();
            return gifts.Select(g => new GiftDto
            {
                Id = g.Id,
                Name = g.Name,
                Description = g.Description,
                DonorId = g.DonorId,
                Price = g.Price,
                CategoryId = g.CategoryId,
                Picture = g.Picture
            }).ToList();
        }
        //קבלת מתנה לפי מזהה
        public async Task<GiftDto?> GetGiftByIdAsync(int id)
        {
            var gift = await _repository.GetGiftByIdAsync(id);
            if (gift == null) return null;
            return new GiftDto
            {
                Id = gift.Id,
                Name = gift.Name,
                Description = gift.Description,
                DonorId = gift.DonorId,
                Price = gift.Price,
                CategoryId = gift.CategoryId,
                Picture = gift.Picture
            };
        }
        //יצירת מתנה חדשה
        public async Task<GiftDto> CreateGiftAsync(CreateGiftDto createGiftDto)
        {
            var gift = new Gift
            {
                Name = createGiftDto.Name,
                Description = createGiftDto.Description,
                DonorId = createGiftDto.DonorId,
                Price = createGiftDto.Price,
                CategoryId = createGiftDto.CategoryId,
                Picture = createGiftDto.Picture
            };
            var createdGift = await _repository.CreateGiftAsync(gift);
            return new GiftDto
            {
                Id = createdGift.Id,
                Name = createdGift.Name,
                Description = createdGift.Description,
                DonorId = createdGift.DonorId,
                Price = createdGift.Price,
                CategoryId = createdGift.CategoryId,
                Picture = createdGift.Picture
            };
        }
        //עדכון מתנה קיימת
        public async Task<CreateGiftDto?> UpdateGiftAsync(int id, CreateGiftDto giftDto)
        {
            var gift = new Gift
            {
                Name = giftDto.Name,
                Description = giftDto.Description,
                DonorId = giftDto.DonorId,
                Price = giftDto.Price,
                CategoryId = giftDto.CategoryId,
                Picture = giftDto.Picture
            };
            var updatedGift = await _repository.UpdateGiftAsync(id, gift);
            if (updatedGift == null)
                return null;
            return new CreateGiftDto
            {
                Name = updatedGift.Name,
                Description = updatedGift.Description,
                DonorId = updatedGift.DonorId,
                Price = updatedGift.Price,
                CategoryId = updatedGift.CategoryId,
                Picture = updatedGift.Picture
            };
        }
        public async Task<bool> DeleteGiftAsync(int id)
        {
            return await _repository.DeleteGiftAsync(id);
        }

        //============================================
        //מיון מתנות לפי שם התורם
        public async Task<List<GiftDto>> GetGiftsByDonorNameAsync(string donorName)
        {
            var gifts = await _repository.GetGiftsByDonorNameAsync(donorName);
            return gifts.Select(g => new GiftDto
            {
                Id = g.Id,
                Name = g.Name,
                Description = g.Description,
                DonorId = g.DonorId,
                Price = g.Price,
                CategoryId = g.CategoryId,
                Picture = g.Picture
            }).ToList();
        }
        //מיון מתנות לפי קטגוריה
        public async Task<List<GiftDto>> GetGiftsByCategoryIdAsync(int categoryId)
        {
            var gifts = await _repository.GetGiftsByCategoryIdAsync(categoryId);
            return gifts.Select(g => new GiftDto
            {
                Id = g.Id,
                Name = g.Name,
                Description = g.Description,
                DonorId = g.DonorId,
                Price = g.Price,
                CategoryId = g.CategoryId,
                Picture = g.Picture
            }).ToList();
        }
        //מיון מתנות לפי שם
        public async Task<List<GiftDto>> SearchGiftsByNameAsync(string name)
        {
            var gifts = await _repository.SearchGiftsByNameAsync(name);
            return gifts.Select(g => new GiftDto
            {
                Id = g.Id,
                Name = g.Name,
                Description = g.Description,
                DonorId = g.DonorId,
                Price = g.Price,
                CategoryId = g.CategoryId,
                Picture = g.Picture
            }).ToList();
        }
        //קבלת תורם לפי מתנה
        public async Task<DonorDto?> GetDonorByGiftIdAsync(int giftId)
        {
            var d = await _repository.GetDonorByGiftIdAsync(giftId);
            return new DonorDto
            {
                Id = d.Id,
                Name = d.Name,
                Email = d.Email,
                Phone = d.Phone,
                Address = d.Address
            };
        }

        //מיון מתנות לפי כמות רכישות
        public async Task<List<GiftDto>> GetGiftsByPurchaseCountAsync(int purchaseCount)
        {
            var gifts = await _repository.GetGiftsByPurchaseCountAsync(purchaseCount);
            return gifts.Select(g => new GiftDto
            {
                Id = g.Id,
                Name = g.Name,
                Description = g.Description,
                DonorId = g.DonorId,
                Price = g.Price,
                CategoryId = g.CategoryId,
                Picture = g.Picture
            }).ToList();
        }
        public async Task<UserWinerDTO?> LotteryForGiftAsync(int giftId)
        {
            var win= await _repository.LotteryForGiftAsync(giftId);
            if (win == null) return null;
            return new UserWinerDTO
            {
                Id = win.Id,
                Name = win.Name,
                Email = win.Email
            };
        }
    }

}
