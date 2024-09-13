namespace M_TV.Models
{
	public class Actor: BaseEntity
	{
		[MaxLength(500)]
		public string Image { get; set; } = string.Empty;
		public ICollection<MovieActor> Movies { get; set; } = new List<MovieActor>();
    }
}
