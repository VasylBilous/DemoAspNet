using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Game
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public int Year { get; set; }
        [Required]
        public double Price { get; set; }
        public string Image { get; set; }
        public int? DeveloperId { get; set; }
        public int? GenreId { get; set; }
        public int? Available { get; set; }
        public virtual Genre Genre { get; set; }
        public virtual Developer Developer { get; set; }
    }
}
