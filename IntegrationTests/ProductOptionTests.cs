using Domain.Model;
using Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTests
{
	class ProductOptionTests
	{
		public static Guid TestId = new Guid("22222222-1111-1111-1111-111111111111");

		public static void TestAll()
		{
			Program.WriteMessage("ProductOptionTests: Started");
			TestGetAll();
			TestGet();
			TestCreateAndDelete();
			//TestUpdateAndDelete();
			Program.WriteMessage("ProductOptionTests: Complete");
		}

		static private void WriteProduct(ProductOption productOption)
		{
			Program.WriteMessage($"ProductOption {productOption.Id}, {productOption.ProductId} {productOption.Name}, {productOption.Description}");
		}

		static private ProductOption GetTestProductOption()
		{
			return new ProductOption()
			{
				Id = TestId,
				ProductId = ProductTests.TestId,
				Name = "TestOptionName",
				Description = "TestOptionDescription"
			};
		}

		static void TestGetAll()
		{
			IProductOptionRepository repository = new ProductOptionRepository(Program.GetDbConnection());
			ICollection<ProductOption> productOptions = repository.GetAll();
			if (productOptions.Count < 2)
			{
				throw new InvalidOperationException("No data returned");
			}

			foreach (ProductOption productOption in productOptions)
			{
				WriteProduct(productOption);
			}
			Program.WriteMessage("ProductOptionTests: TestGetAll OK");
		}

		static void TestGet()
		{
			IProductOptionRepository repository = new ProductOptionRepository(Program.GetDbConnection());
			ICollection<ProductOption> productOptions = repository.GetAll();
			if (productOptions.Count < 2)
			{
				throw new InvalidOperationException("No data returned");
			}

			ProductOption productOption = repository.GetById(productOptions.First().Id);
			if (productOption == null)
			{
				throw new InvalidOperationException("Cannot find product by id");
			}
			WriteProduct(productOption);
			Program.WriteMessage("ProductOptionTests: TestGet OK");
		}

		static void TestCreateAndDelete()
		{
			IProductOptionRepository repository = new ProductOptionRepository(Program.GetDbConnection());
			ProductOption productFromDb = repository.GetById(TestId);
			if (productFromDb != null)
			{
				// lets remove it so the tests will run next time
				repository.Delete(TestId);
				throw new InvalidOperationException("Test id already present");
			}

			repository.Create(GetTestProductOption());
			productFromDb = repository.GetById(TestId);
			if (productFromDb == null)
			{
				throw new InvalidOperationException("Test id not created");
			}

			repository.Delete(TestId);
			productFromDb = repository.GetById(TestId);
			if (productFromDb != null)
			{
				throw new InvalidOperationException("Test id not deleted");
			}

			Program.WriteMessage("ProductOptionTests: TestCreateAndDelete OK");
		}
	}
}
