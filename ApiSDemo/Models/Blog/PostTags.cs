namespace ApiSDemo.Models.Blog
{
	public class PostTags
	{
		public int Id { get; set; }

		public int PostId { get; set; }
		public Post Post { get; set; }
		public int TagsId{ get; set; }
		public Tags Tags { get; set; }
			
	}
}
