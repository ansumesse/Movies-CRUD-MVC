namespace M_TV.Repository
{
    public static class ImageServices
    {
        public static async Task<string> Save(string folderPath, IFormFile cover)
        {
            var coverName = $"{Guid.NewGuid()}{Path.GetExtension(cover.FileName)}";
            var path = Path.Combine(folderPath, coverName);

            using var stream = File.Create(path);
            await cover.CopyToAsync(stream);
            return coverName;
        }
    }
}
