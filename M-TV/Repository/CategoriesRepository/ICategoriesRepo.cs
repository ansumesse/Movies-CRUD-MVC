namespace M_TV.Repository.CategoriesRepository
{
    public interface ICategoriesRepo
    {
        public IEnumerable<SelectListItem> GetSelectListsCatigories();
    }
}
