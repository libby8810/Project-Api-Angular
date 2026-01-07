using StoreApi.Models;
using System.ComponentModel.DataAnnotations;

namespace StoreApi.DTOs
{
    public class GiftDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The gift name is required.")]
        [MaxLength(100, ErrorMessage = "The gift name can't be longer than 100 characters.")]
        public string Name { get; set; }

        [MaxLength(500, ErrorMessage = "The description can't be longer than 500 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "The donor is required.")]
        public int DonorId { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Price must be a positive number.")]
        public int Price { get; set; }

        [Required(ErrorMessage = "The category is required.")]
        public int CategoryId { get; set; }

        [Url(ErrorMessage = "The picture URL is invalid.")]
        public string Picture { get; set; }
    }
    public class CreateGiftDto
    {
        [Required(ErrorMessage = "The gift name is required.")]
        [MaxLength(100, ErrorMessage = "The gift name can't be longer than 100 characters.")]
        public string Name { get; set; }

        [MaxLength(500, ErrorMessage = "The description can't be longer than 500 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "The donor is required.")]
        public int DonorId { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Price must be a positive number.")]
        public int Price { get; set; }

        [Required(ErrorMessage = "The category is required.")]
        public int CategoryId { get; set; }

        [Url(ErrorMessage = "The picture URL is invalid.")]
        public string Picture { get; set; }
    }

}
