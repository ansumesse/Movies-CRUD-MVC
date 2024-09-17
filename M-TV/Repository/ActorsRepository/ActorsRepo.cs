namespace M_TV.Repository.ActorsRepository
{
    public class ActorsRepo : IActorsRepo
    {
        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly string actorsImagesPath;

        public ActorsRepo(ApplicationDbContext context,
            IWebHostEnvironment webHostEnvironment)
        {
            this.context = context;
            this.webHostEnvironment = webHostEnvironment;
            actorsImagesPath =$"{webHostEnvironment.WebRootPath}{FileSettings.ActorsImagePath}" ;
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
        public async Task Create(CreateActorViewModel newActorVM)
        {
            Actor actor = new()
            {
                Name = newActorVM.Name,
                Image = await ImageServices.Save(actorsImagesPath, newActorVM.Image)
            };
            context.Actors.Add(actor);
            context.SaveChanges();
        }
    }
}
