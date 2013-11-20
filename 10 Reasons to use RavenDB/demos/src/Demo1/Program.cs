using System;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Extensions;

namespace Demo1
{
	public class Program
	{
		public static void Main(string[] args)
		{
			using (var documentStore = InitializeDocumentStore())
			{
				documentStore.DatabaseCommands.EnsureDatabaseExists("Geeker1");
				new Demo1().Execute(documentStore);
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
