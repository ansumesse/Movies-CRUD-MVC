namespace M_TV.ViewModels
{
    public class MovieViewModel
    {
        [MaxLength(250)]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; } = Enumerable.Empty<SelectListItem>();
        [MaxLength(2500)]
        public string Discription { get; set; } = string.Empty;
        [Display(Name = "Actors")]
        public List<int> SelectedActors { get; set; } = default!;
        public IEnumerable<SelectListItem> Actors { get; set; } = Enumerable.Empty<SelectListItem>();
        [Range(0, 10)]
        public int Rate { get; set; }
    }
}
