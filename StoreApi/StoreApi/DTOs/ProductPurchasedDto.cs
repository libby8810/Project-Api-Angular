using StoreApi.Models;
using System.ComponentModel.DataAnnotations;

namespace StoreApi.DTOs
{
    public class ProductPurchasedDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The gift is required.")]
        public int GiftId { get; set; }

        [Required(ErrorMessage = "The user is required.")]
        public int UserId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "The count must be a positive integer.")]
        public int Count { get; set; }
    }
}
