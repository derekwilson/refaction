using Domain.Data;
using Domain.SQLServer;
using System;

namespace IntegrationTests
{
	class Program
	{
		public static void WriteMessage(string message)
		{
			Console.WriteLine(message);
		}

		public static IConnectionFactory GetDbConnection()
		{
			// we could use IoC or just manually swap to another provider for the tests here
			return new LocalFileDbConnectionFactory("Database.mdf");
		}

		static void Main(string[] args)
		{
			ProductTests.TestAll();
			ProductOptionTests.TestAll();
			ProductAndProductOptionTests.TestAll();
		}
	}
}
