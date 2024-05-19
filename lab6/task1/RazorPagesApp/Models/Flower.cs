using System.ComponentModel.DataAnnotations;

namespace RazorPagesApp.Models
{
    public class Flower : Plant
    {
        [Required(ErrorMessage = "Поле 'Длина стебля' является обязательным.")]
        public double StemLength { get; set; }
    }
}
