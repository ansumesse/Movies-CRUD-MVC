

namespace M_TV.Models
{
	public class Movie: BaseEntity
	{
		[MaxLength(2500)]
		public string Discription { get; set; } = string.Empty;
		[MaxLength(500)]
		public string Cover { get; set; } = string.Empty;
		[ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
		public ICollection<MovieActor> MyProperty { get; set; } = new List<MovieActor>();
    }
}
