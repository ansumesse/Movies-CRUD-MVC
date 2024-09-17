namespace M_TV.Settings
{
    public static class FileSettings
    {
        public const string MoviesImagePath = "/assets/images/movies";
        public const string ActorsImagePath = "/assets/images/Actors";
        public const string AllowedImageExtensions = ".jpg,.jpeg,.png";
        public const int MaxFileSizeInMB = 1;
        public const int MaxFileSizeInBytes = MaxFileSizeInMB * 1024 * 1024;
    }
}
