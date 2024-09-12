namespace M_TV.Models
{
	public class Category: BaseEntity
	{
        public ICollection<Movie> Movies { get; set; }
    }
}
