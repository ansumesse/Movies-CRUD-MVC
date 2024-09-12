namespace M_TV.Models
{
	public class Actor: BaseEntity
	{
		[MaxLength(500)]
        public string Image { get; set; }
		public ICollection<MovieActor> MyProperty { get; set; } = new List<MovieActor>();
    }
}
