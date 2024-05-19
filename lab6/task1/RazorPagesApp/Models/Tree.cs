using System.ComponentModel.DataAnnotations;

namespace RazorPagesApp.Models
{
    public class Tree : Plant
    {
        [Required(ErrorMessage = "Поле 'Возраст' является обязательным.")]
        public int Age { get; set; }
    }
}
