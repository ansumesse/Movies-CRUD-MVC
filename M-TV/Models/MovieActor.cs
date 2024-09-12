namespace M_TV.Models
{
	public class MovieActor
	{
        [ForeignKey("Movie")]
        public int MovieId { get; set; }
        public Movie Movie { get; set; } = default!;
		[ForeignKey("Actor")]
		public int ActorId { get; set; }
        public Actor Actor { get; set; } = default!;
    }
}
