using System;
using Raven.Client;

namespace Demo1
{
	public class Demo1
	{
		public void Execute(IDocumentStore documentStore)
		{
			using (var session = documentStore.OpenSession("Geeker1"))
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