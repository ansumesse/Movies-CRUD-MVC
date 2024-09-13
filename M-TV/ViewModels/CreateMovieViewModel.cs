using Microsoft.AspNetCore.Mvc.Rendering;

namespace M_TV.ViewModels
{
    public class CreateMovieViewModel
    {
        [MaxLength(250)]
        public string Name { get; set; } = string.Empty;
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; } = Enumerable.Empty<SelectListItem>();
        [MaxLength(2500)]
        public string Discription { get; set; } = string.Empty;
        [Display(Name = "Actors")]
        public List<int> SelectedActors { get; set; } = new List<int>();
        public IEnumerable<SelectListItem> Actors { get; set; } = Enumerable.Empty<SelectListItem>();
        [Range(0, 10)]
        public float Rate { get; set; }

        public IFormFile Cover { get; set; } = default!;



    }
}
