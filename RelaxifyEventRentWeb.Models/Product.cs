using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RelaxifyEventRentWeb.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane")]        
        [MaxLength(30)]
        [DisplayName("Nazwa przedmiotu")]
        public string? ProductName { get; set; }
        [DisplayName("Opis przedmiotu")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane")]
        [Range(1,40000)]
        [DisplayName("Cena przedmiotu")]
        public double Price { get; set; }
        [DisplayName("Dodaj zdjęcie przedmiotu")]
        public string? ImageUrl { get; set; }
        [DisplayName("Kategoria")]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }
    }
}
