namespace M_TV.ViewModels
{
    public class CreateMovieViewModel : MovieViewModel
    {
        // [Remote(action: "AllowedExtensionsAction", controller: "Movies", ErrorMessage = "Invalid file.")]

        [MaxFileSize(FileSettings.MaxFileSizeInBytes)]
        [AllowedExtension(FileSettings.AllowedImageExtensions)]
        public IFormFile Cover { get; set; } = default!;



    }
}
