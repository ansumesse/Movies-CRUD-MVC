namespace M_TV.Attributes
{
    public class AllowedExtensionAttribute : ValidationAttribute
    {
        private readonly string allowedExtenstion;

        public AllowedExtensionAttribute(string allowedExtenstion)
        {
            this.allowedExtenstion = allowedExtenstion;
        }
      
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            IFormFile file = value as IFormFile;

            if (file is not null)
            {
                string extension = Path.GetExtension(file.FileName);

                bool isAllowed = allowedExtenstion.Split(',').Contains(extension, StringComparer.OrdinalIgnoreCase);
                if (isAllowed)
                {
                    return ValidationResult.Success;
                }
                return new ValidationResult($"only {allowedExtenstion} allowed");
            }
            return ValidationResult.Success;
        }
    }
}
