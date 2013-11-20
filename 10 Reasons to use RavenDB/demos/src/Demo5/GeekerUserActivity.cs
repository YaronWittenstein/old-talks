using System;

namespace Demo5
{
	public class GeekerUserActivity
	{
		public GeekerUserActivity()
		{
			Id = Guid.NewGuid().ToString();
		}

		public string Id { get; set; }

		public string UserId { get; set; }

        public string ActivityId { get; set; }
	}
}