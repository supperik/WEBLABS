using System.ComponentModel.DataAnnotations;

namespace RazorPagesApp.Models
{
    public class Plant
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле 'Название' является обязательным.")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "Поле 'Вид' является обязательным.")]
        public string? Species { get; set; }
    }
}
