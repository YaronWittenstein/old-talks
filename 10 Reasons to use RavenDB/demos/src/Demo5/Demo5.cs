using System;
using System.Linq;
using System.ComponentModel.Composition.Hosting;

using Raven.Client;
using Raven.Client.Linq;
using Raven.Client.Indexes;


namespace Demo5
{
	public class Demo5
	{
		private readonly IDocumentStore documentStore;

		public Demo5(IDocumentStore documentStore)
		{
			this.documentStore = documentStore;
		}

		public void BuildDatabase()
		{
            using (var session = documentStore.OpenSession("Geeker5"))
            {
                var telAvivParty = new Activity
                                    {
                                        Id = "activities/1",
                                        Name = "Tel-Aviv Linux Party",
                                        Location = new ActivityLocation { City = "Tel-Aviv", Street = "Hertzel", StreetNumber = 12 },
                                        When = new DateTime(2013, 5, 1)
                                    };

                var yoyoTelAviv = new Activity
                                    {
                                        Id = "activities/2",
                                        Name = "YoYo Tournament",
                                        Location = new ActivityLocation { City = "Tel-Aviv", Street = "Moshe-Dayan", StreetNumber = 43 },
                                        When = new DateTime(2013, 5, 2)
                                    };

                var crosswordsHaifa = new Activity
                                        {
                                            Id = "activities/3",
                                            Name = "Extreme Crosswords",
                                            Location = new ActivityLocation { City = "Haifa", Street = "Ben-Gurion", StreetNumber = 11 },
                                            When = new DateTime(2013, 5, 7)
                                        };

                session.Store(telAvivParty);
                session.Store(yoyoTelAviv);
                session.Store(crosswordsHaifa);

                session.SaveChanges();
            }

			for (int i = 0; i < 100; i++)
			{
				using (var session = documentStore.OpenSession("Geeker5"))
				{
					for (int j = 1; j <= 100; j++)
					{
						int k = 100 * i + j;
						var user = new GeekerUser
									  {
										  Id = "users/" + k,
										  Age = k,
										  Nickname = "@user" + k,
										  FirstName = "User" + k,
										  LastName = "LastName" + k,
									  };

						session.Store(user);

						if (k % 3 == 0)
						{
                            session.Store(new GeekerUserActivity { UserId = user.Id, ActivityId = "activities/1" });
						}

						if (k % 5 == 0)
						{
                            session.Store(new GeekerUserActivity { UserId = user.Id, ActivityId = "activities/2" });
						}

						if (k % 7 == 0)
						{
                            session.Store(new GeekerUserActivity { UserId = user.Id, ActivityId = "activities/3" });
						}
					}
					session.SaveChanges();
				}
			}
		}
	}
}
