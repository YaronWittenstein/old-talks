using System;
using System.Linq;
using Raven.Client;

namespace Demo3
{
	public class Demo3
	{
		private readonly IDocumentStore documentStore;

		public Demo3(IDocumentStore documentStore)
		{
			this.documentStore = documentStore;
		}

		public void BuildDatabase()
		{
			using (var session = documentStore.OpenSession("Geeker3"))
			{
				for (int i = 1; i <= 100; i++)
				{
					var user = new GeekerUser
								   {
									   Id = "users/" + i,
									   Age = i,
									   FirstName = "User" + i,
									   LastName = "Userly" + i,
									   Nickname = "@user" + i,
								   };
					session.Store(user);
				}
				session.SaveChanges();
			}
		}

		public void ShowUsersOlderThan(int age)
		{
			using (var session = documentStore.OpenSession("Geeker3"))
			{
				var users = session.Query<GeekerUser>().Where(u => u.Age > age).ToArray();
				foreach (var user in users)
				{
					Console.WriteLine(user);
				}
			}
		}

		public void ShowNumberOfUsers()
		{
			using (var session = documentStore.OpenSession("Geeker3"))
			{
				int numberOfUsers = session.Query<GeekerUser>().Count();
				Console.WriteLine("Geeker system has {0} users", numberOfUsers);
			}
		}
	}
}