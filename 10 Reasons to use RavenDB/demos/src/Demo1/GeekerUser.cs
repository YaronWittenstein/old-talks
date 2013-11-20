namespace Demo1
{
	public class GeekerUser
	{
		public string Id { get; set; }

		public string Nickname { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public override string ToString()
		{
			return string.Format("Id: {0}	Nickname: {1}", Id, Nickname);
		}
	}
}