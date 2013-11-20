using System;
using Raven.Client;

namespace Demo2
{
	public class Demo2
	{
		public void BuildDatabaseWithoutAge(IDocumentStore documentStore)
		{
			using (var session = documentStore.OpenSession("Geeker2"))
			{
				for (int i = 1; i <= 100; i++)
				{
					var user = new GeekerUser
								   {
									   Id = "users/" + i,
									   FirstName = "User" + i,
									   LastName = "Userly" + i,
									   Nickname = "@user" + i,
								   };
					Console.WriteLine("storing user: {0}", user.Id);
					session.Store(user);
				}
				session.SaveChanges();
			}
		}
	}
}