using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RelaxifyEventRentWeb.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }        
        [Required(ErrorMessage = "To pole jest wymagane")]
        [RegularExpression("[A-Za-zżźćńółęąśŻŹĆĄŚĘŁÓŃ\\s]*", ErrorMessage = "To pole nie może zawierać liczb")]
        [MaxLength(30)]
        [DisplayName("Nazwa kategorii")]     
        public string? Name { get; set; }
        [DisplayName("Kolejność wyświetlania kategorii")]        
        [Range(1,100, ErrorMessage = "To pole musi być z zakresu od 1-100")]
        public int DisplayOrder { get; set; }
    }
}
