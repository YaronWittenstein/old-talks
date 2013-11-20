using System;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Extensions;

namespace Demo3
{
	public class Program
	{
		public static void Main(string[] args)
		{
			using (var documentStore = InitializeDocumentStore())
			{
				documentStore.DatabaseCommands.EnsureDatabaseExists("Geeker3");
				var demo3 = new Demo3(documentStore);

				// First Run
				demo3.BuildDatabase();

				// Second Run (so that the index will be updated)
				demo3.ShowUsersOlderThan(40);
				demo3.ShowNumberOfUsers();
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
