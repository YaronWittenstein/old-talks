using System;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Extensions;

namespace Demo2
{
	public class Program
	{
		public static void Main(string[] args)
		{
			using (var documentStore = InitializeDocumentStore())
			{
				documentStore.DatabaseCommands.EnsureDatabaseExists("Geeker2");
                new Demo2().BuildDatabaseWithoutAge(documentStore);
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
