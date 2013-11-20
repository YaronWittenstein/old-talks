using System;
using Raven.Client;

namespace Demo2
{
	public class Demo2
	{
		public void BuildDatabaseWithAge(IDocumentStore documentStore)
		{
			using (var session = documentStore.OpenSession("Geeker2"))
			{
				int userAge = 1;
				foreach (var user in session.Query<GeekerUser>())
				{
					if (user.Age <= 0)
					{
						Console.WriteLine("user {0} has no age defined.... his new age will be {1}", user.Id, userAge);
						user.Age = userAge;
						session.Store(user);
						userAge++;

						// In real case we would send email to the user asking for his age
						// SendEmailToTheUserAskingForHisAge(user);
						// we would add server-side Task that will spin in the background the email requests


						// No Down-Time, NO Damage to the speed of the application
					}
				}
				session.SaveChanges();
			}
		}
	}
}