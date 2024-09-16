namespace M_TV.ViewModels
{
    public class UpdateMovieViewModel : MovieViewModel
    {
        public int Id { get; set; }
        public string? CoverName { get; set; }
        [MaxFileSize(FileSettings.MaxFileSizeInBytes)]
        [AllowedExtension(FileSettings.AllowedImageExtensions)]
        public IFormFile? Cover { get; set; }
    }
}
