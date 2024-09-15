

namespace M_TV.Models
{
	public class Movie: BaseEntity
	{
		[MaxLength(2500)]
		public string Discription { get; set; } = string.Empty;
		[MaxLength(500)]
		public string Cover { get; set; } = string.Empty;
		[Range(0, 10)]
        public int Rate { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
		public ICollection<MovieActor> Actors { get; set; } = new List<MovieActor>();
    }
}
