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
				documentStore.DatabaseCommands.EnsureDatabaseExists("Geeker4");
				var demo4 = new Demo4(documentStore);

				// First Run
                demo4.BuildDatabase();

				// Second Run
                demo4.ShowUserActivitiesAtCity("users/1", "Tel-Aviv");
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
