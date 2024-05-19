using System.ComponentModel.DataAnnotations;

namespace RazorPagesApp.Models
{
    public class Rose : Flower
    {
        [Required(ErrorMessage = "Поле 'Цвет' является обязательным.")]
        public string? Color { get; set; }
    }
}
