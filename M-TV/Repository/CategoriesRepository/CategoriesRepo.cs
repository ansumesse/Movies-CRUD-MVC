namespace M_TV.Repository.CategoriesRepository
{
    public class CategoriesRepo : ICategoriesRepo
    {
        private readonly ApplicationDbContext context;

        public CategoriesRepo(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IEnumerable<SelectListItem> GetSelectListsCatigories()
        {
            return context.Categories
                .Select(c => new SelectListItem()
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                })
                .AsNoTracking()
                .OrderBy(c => c.Text);
        }
    }
}
