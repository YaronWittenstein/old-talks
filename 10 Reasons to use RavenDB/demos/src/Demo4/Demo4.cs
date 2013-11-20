using System;
using System.Linq;
using System.ComponentModel.Composition.Hosting;

using Raven.Client;
using Raven.Client.Indexes;


namespace Demo5
{
    public class Demo4
    {
        private readonly IDocumentStore documentStore;

        public Demo4(IDocumentStore documentStore)
        {
            this.documentStore = documentStore;
            var catalog = new CompositionContainer(new AssemblyCatalog(typeof(GeekerUsersActivities_ByUserId).Assembly));
            IndexCreation.CreateIndexes(catalog, documentStore.DatabaseCommands.ForDatabase("Geeker4"), documentStore.Conventions);
        }

        public void BuildDatabase()
        {
            using (var session = documentStore.OpenSession("Geeker4"))
            {
                session.Store(new GeekerUser { Id = "users/1", Age = 25, Nickname = "@geeky", FirstName = "Geeky", LastName = "Geek" });
                session.Store(new GeekerUser { Id = "users/2", Age = 23, Nickname = "@smarty", FirstName = "Smarty", LastName = "Smart" });
                session.Store(new GeekerUser { Id = "users/3", Age = 28, Nickname = "@nerdy", FirstName = "Nerdy", LastName = "Nerd" });
                session.Store(new GeekerUser { Id = "users/4", Age = 21, Nickname = "@dorky", FirstName = "Dorky", LastName = "Dork" });
                session.Store(new GeekerUser { Id = "users/5", Age = 32, Nickname = "@weirdo", FirstName = "Weirdo", LastName = "weird" });
                session.Store(new GeekerUser { Id = "users/6", Age = 24, Nickname = "@smarterthanU", FirstName = "smarter", LastName = "thanU" });

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

            using (var session = documentStore.OpenSession("Geeker4"))
            {
                session.Query<GeekerUser>().Customize(x => x.WaitForNonStaleResults());
                session.Query<Activity>().Customize(x => x.WaitForNonStaleResults());

                var geekerUserActivity1 = new GeekerUserActivity { ActivityId = "activities/1", UserId = "users/1" };
                var geekerUserActivity2 = new GeekerUserActivity { ActivityId = "activities/2", UserId = "users/1" };
                var geekerUserActivity3 = new GeekerUserActivity { ActivityId = "activities/3", UserId = "users/1" };

                session.Store(geekerUserActivity1);
                session.Store(geekerUserActivity2);
                session.Store(geekerUserActivity3);

                var smartyUserActivity1 = new GeekerUserActivity { ActivityId = "activities/1", UserId = "users/2" };
                var smartyUserActivity2 = new GeekerUserActivity { ActivityId = "activities/2", UserId = "users/2" };
                var nerdyUserActivity1 = new GeekerUserActivity { ActivityId = "activities/2", UserId = "users/3" };
                var dorkyUserActivity1 = new GeekerUserActivity { ActivityId = "activities/3", UserId = "users/4" };

                session.Store(smartyUserActivity1);
                session.Store(smartyUserActivity2);
                session.Store(nerdyUserActivity1);
                session.Store(dorkyUserActivity1);

                session.SaveChanges();
            }
        }


        public void ShowUserActivitiesAtCity(string userId, string city)
        {
            using (var session = documentStore.OpenSession("Geeker4"))
            {
                var geekyTelAvivActivities = session.Query<GeekerUserActivity, GeekerUsersActivities_ByUserId>()
                                                   .Where(x => x.UserId == userId)
                                                   .As<GeekerUsersActivitiesView>()
                                                   .FirstOrDefault();

                if (geekyTelAvivActivities == null)
                    return;

                GeekerUser user = session.Load<GeekerUser>(userId);

                Console.WriteLine("{0} activities in {1}", user.Nickname, city);
                foreach (var activity in geekyTelAvivActivities.Activities)
                {
                    var location = activity.Location;
                    if (location.City == city)
                    {
                        Console.WriteLine("{0} ({1} {2}) at {3}", activity.Name, location.Street, location.StreetNumber, activity.When);
                    }
                }
            }
        }
    }
}
