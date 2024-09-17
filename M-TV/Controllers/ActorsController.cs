namespace M_TV.Controllers
{
    public class ActorsController : Controller
    {
        private readonly IActorsRepo actorsRepo;

        public ActorsController(IActorsRepo actorsRepo)
        {
            this.actorsRepo = actorsRepo;
        }
        [HttpGet]
       public IActionResult Create()
        {
            return View();
        }
        public async Task<IActionResult> Create(CreateActorViewModel actorVM)
        {
            if (ModelState.IsValid)
            {
                await actorsRepo.Create(actorVM);
                return RedirectToAction("Create", "Movies");
            }
            return View(actorVM);
        }
    }
}
