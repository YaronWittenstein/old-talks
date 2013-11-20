using System;
using System.Linq;

using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Extensions;

namespace Demo5
{
	public class Program
	{
		public static void Main(string[] args)
		{
			using (var documentStore = InitializeDocumentStore())
			{
				documentStore.DatabaseCommands.EnsureDatabaseExists("Geeker5");
				var demo5 = new Demo5(documentStore);

				// First Run
                //demo5.BuildDatabase();

				// Second Run
                int pageSize = 5;
                int pageNumber = 2;
                RavenQueryStatistics stats;
                using (var session = documentStore.OpenSession("Geeker5"))
                {
                    var activityUsers = session.Query<GeekerUserActivity>()
                                                    .Statistics(out stats)
                                                    .Where(x => x.ActivityId == "activities/2")
                                                    .Skip(pageNumber * pageSize)
                                                    .Take(pageSize)
                                                    .ToArray();
                    
                    string[] attendeesIds = activityUsers.Select(x => x.UserId).ToArray();
                    GeekerUser[] attendees = session.Load<GeekerUser>(attendeesIds);

                    int firstIndex = stats.SkippedResults + 1;
                    int lastIndex = firstIndex + activityUsers.Length - 1;

                    Console.WriteLine("Attendees of Tel-Aviv Linux Party");
                    Console.WriteLine("{0} - {1} results out of (showing {2} entries per page)", firstIndex, lastIndex, pageSize);
                    foreach (var attendee in attendees)
                    {
                        Console.WriteLine(attendee.Nickname);
                    }
                }
			}
			Console.WriteLine();
			Console.WriteLine("done.");
		}

		private static IDocumentStore InitializeDocumentStore()
		{
			return new DocumentStore { Url = "http://localhost:1234" }.Initialize();
		}
	}
}
