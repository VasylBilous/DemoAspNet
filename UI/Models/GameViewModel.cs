using System.ComponentModel.DataAnnotations;

namespace UI.Models
{
    public class GameViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Введіть, будь ласка, назву гри")]
        [MinLength(2, ErrorMessage = "Закоротка назва")]
        [MaxLength(100)]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "Введіть, будь ласка, рік")]
        public int Year { get; set; }
        [Range(1, 1000, ErrorMessage = "Ціна має бути в діапазоні 1-1000")]
        public double Price { get; set; }
        public string Image { get; set; }
        [Required(ErrorMessage = "Виберіть розробника з списку")]
        public string Genre { get; set; }
        public string Developer { get; set; }
        public int Available { get; set; }
    }
}