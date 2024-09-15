
using System.CodeDom;

namespace M_TV.Repository.ActorsRepository
{
    public class ActorsRepo : IActorsRepo
    {
        private readonly ApplicationDbContext context;

        public ActorsRepo(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IEnumerable<SelectListItem> GetSelectListsActors()
        {
            return context.Actors
                .Select(a => new SelectListItem()
                {
                    Text = a.Name,
                    Value = a.Id.ToString()
                })
                .AsNoTracking()
                .OrderBy(a => a.Text);
        }
    }
}
