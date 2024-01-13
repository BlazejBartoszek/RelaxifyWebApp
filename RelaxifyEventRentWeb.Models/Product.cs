using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RelaxifyEventRentWeb.Models
{
    public class Product
    {

        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane")]
        [RegularExpression("[A-Za-zżźćńółęąśŻŹĆĄŚĘŁÓŃ\\s]*", ErrorMessage = "To pole nie może zawierać liczb")]
        [MaxLength(30)]
        [DisplayName("Nazwa przedmiotu")]
        public string? ProductName { get; set; }
        public string? Description { get; set; }
        public int Price { get; set; }
    }
}
