using Microsoft.EntityFrameworkCore;
using StoreApi.Data;
using StoreApi.Models;

namespace StoreApi.Repositories
{
    public class GiftRepository : IGiftRepository
    {
        private readonly StoreContextDB _context;
        public GiftRepository(StoreContextDB context)
        {
            _context = context;
        }
        //קבל כל המתנות
        public async Task<List<Gift>> GetAllGiftsAsync()
        {
            return await _context.Gifts.ToListAsync();
        }
        //קבל מתנה לפי מזהה
        public async Task<Gift?> GetGiftByIdAsync(int id)
        {
            return await _context.Gifts.FindAsync(id);
        }
        //צור מתנה
        public async Task<Gift> CreateGiftAsync(Gift gift)
        {
            _context.Gifts.Add(gift);
            await _context.SaveChangesAsync();
            return gift;
        }
        //עדכן מתנה
        public async Task<Gift?> UpdateGiftAsync(int id, Gift gift)
        {
            var existingGift = await _context.Gifts.FindAsync(id);
            if (existingGift == null)
            {
                return null;
            }
            existingGift.Name = gift.Name;
            existingGift.Description = gift.Description;
            existingGift.DonorId = gift.DonorId;
            existingGift.Price = gift.Price;
            existingGift.CategoryId = gift.CategoryId;
            existingGift.Picture = gift.Picture;
            await _context.SaveChangesAsync();
            return existingGift;
        }
        //מחק מתנה
        public async Task<bool> DeleteGiftAsync(int id)
        {
            var gift = await _context.Gifts.FindAsync(id);
            if (gift == null)
            {
                return false;
            }
            var purchases = await _context.ProductPurchaseds
                .Where(p => p.GiftId == id)
                .ToListAsync();
            if(purchases.Count > 0)
                return false;
            _context.Gifts.Remove(gift);
            var cartItems = await _context.ProductCarts
                .Where(c => c.GiftId == id)
                .ToListAsync();
            cartItems.ForEach(c => _context.ProductCarts.Remove(c));
            await _context.SaveChangesAsync();
            return true;
        }
        //============================================
        //קבלת מתנות לפי שם התורם
        public async Task<List<Gift>> GetGiftsByDonorNameAsync(string donorName)
        {
            return await _context.Gifts
                .Include(g => g.Donor)
                .Where(g => g.Donor.Name == donorName)
                .ToListAsync();
        }
        //קבלת מתנות לפי מזהה קטגוריה
        public async Task<List<Gift>> GetGiftsByCategoryIdAsync(int categoryId)
        {
            return await _context.Gifts
                .Where(g => g.CategoryId == categoryId)
                .ToListAsync();
        }
        //חיפוש מתנות לפי שם
        public async Task<List<Gift>> SearchGiftsByNameAsync(string name)
        {
            return await _context.Gifts
                .Where(g => EF.Functions.Like(g.Name, $"%{name}%"))
                .ToListAsync();

        }
        //קבלת תורם לפי מזהה מתנה
        public async Task<Donor?> GetDonorByGiftIdAsync(int giftId)
        {
            var gift = await _context.Gifts
                .Include(g => g.Donor)
                .FirstOrDefaultAsync(g => g.Id == giftId);
            return gift?.Donor;

        }
        //קבלת מתנות לפי מספר רכישות
        public async Task<List<Gift>> GetGiftsByPurchaseCountAsync(int purchaseCount)
        {
            return await _context.Gifts
                .Where(g => g.Purchaseds.Count == purchaseCount)
                .ToListAsync();
        }
        //הגרלת זוכה למתנה
        public async Task<User?> LotteryForGiftAsync(int giftId)
        {
            var gift = await _context.Gifts
                .Include(g => g.Purchaseds)
                .ThenInclude(p => p.User)
                .FirstOrDefaultAsync(g => g.Id == giftId);
            if (gift == null || gift.Purchaseds.Count == 0)
            {
                return null;
            }
            var ticketPool = new List<User>();
            foreach (var purchase in gift.Purchaseds)
            {
                for (int i = 0; i < purchase.Count; i++)
                {
                    ticketPool.Add(purchase.User);
                }
            }
            var random = new Random();
            int winnerIndex = random.Next(ticketPool.Count);
            return ticketPool[winnerIndex];
        }
    }
}
