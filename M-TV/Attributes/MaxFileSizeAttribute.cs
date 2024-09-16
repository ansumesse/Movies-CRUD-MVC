namespace M_TV.Attributes
{
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int maxFileSize;

        public MaxFileSizeAttribute(int maxFileSize)
        {
            this.maxFileSize = maxFileSize;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            IFormFile file = value as IFormFile;
            if(file is not null)
            {
                if(file.Length < maxFileSize)
                {
                    return ValidationResult.Success;
                }
                return new ValidationResult($"File size must be less than {maxFileSize}");
            }
            return ValidationResult.Success;
        }
    }
}
