using Domain.Model;
using Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTests
{
	class ProductTests
	{
		public static Guid TestId = new Guid("11111111-1111-1111-1111-111111111111");

		public static void TestAll()
		{
			Program.WriteMessage("ProductTests: Started");
			TestGetAll();
			TestGet();
			TestGetByName();
			TestCreateAndDelete();
			TestUpdateAndDelete();
			Program.WriteMessage("ProductTests: Complete");
		}

		static private void WriteProduct(Product product)
		{
			Program.WriteMessage($"Product {product.Id}, {product.Name}, {product.Description}, {product.Price}, {product.DeliveryPrice}");
		}

		static private Product GetTestProduct()
		{
			return new Product()
			{
				Id = TestId,
				Name = "TestName",
				Description = "TestDescription",
				DeliveryPrice = 1,
				Price = 2
			};
		}

		static void TestGetAll()
		{
			IProductRepository repository = new ProductRepository(Program.GetDbConnection());
			ICollection<Product> products = repository.GetAll();
			if (products.Count < 2)
			{
				throw new InvalidOperationException("No data returned");
			}

			foreach (Product product in products)
			{
				WriteProduct(product);
			}
			Program.WriteMessage("ProductTests: TestGetAll OK");
		}

		static void TestGet()
		{
			IProductRepository repository = new ProductRepository(Program.GetDbConnection());
			ICollection<Product> products = repository.GetAll();
			if (products.Count < 2)
			{
				throw new InvalidOperationException("No data returned");
			}

			Product product = repository.GetById(products.First().Id);
			if (product == null)
			{
				throw new InvalidOperationException("Cannot find product by id");
			}
			WriteProduct(product);
			Program.WriteMessage("ProductTests: TestGet OK");
		}

		static void TestGetByName()
		{
			IProductRepository repository = new ProductRepository(Program.GetDbConnection());
			ICollection<Product> products = repository.GetAll();
			if (products.Count < 2)
			{
				throw new InvalidOperationException("No data returned");
			}

			ICollection<Product> productsByName = repository.GetByName(products.First().Name);
			if (productsByName.Count < 1)
			{
				throw new InvalidOperationException("Cannot find product by name");
			}
			foreach (Product product in productsByName)
			{
				WriteProduct(product);
			}
			Program.WriteMessage("ProductTests: TestGetByName OK");
		}

		static void TestCreateAndDelete()
		{
			IProductRepository repository = new ProductRepository(Program.GetDbConnection());
			Product productFromDb = repository.GetById(TestId);
			if (productFromDb != null)
			{
				// lets remove it so the tests will run next time
				repository.Delete(TestId);
				throw new InvalidOperationException("Test id already present");
			}

			repository.Create(GetTestProduct());
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

			Program.WriteMessage("ProductTests: TestCreateAndDelete OK");
		}

		static void TestUpdateAndDelete()
		{
			IProductRepository repository = new ProductRepository(Program.GetDbConnection());
			repository.Create(GetTestProduct());
			Product productFromDb = repository.GetById(TestId);
			if (productFromDb == null)
			{
				throw new InvalidOperationException("Test id not created");
			}

			productFromDb.Name = "TestUpdatedName";
			repository.Update(productFromDb);

			Product updatedProductFromDb = repository.GetById(TestId);
			if (updatedProductFromDb == null)
			{
				throw new InvalidOperationException("updated product not found");
			}

			// TODO - test the other properties
			if (updatedProductFromDb.Name != "TestUpdatedName")
			{
				throw new InvalidOperationException("name not updated");
			}

			repository.Delete(TestId);
			productFromDb = repository.GetById(TestId);
			if (productFromDb != null)
			{
				throw new InvalidOperationException("Test id not deleted");
			}

			Program.WriteMessage("ProductTests: TestUpdateAndDelete OK");
		}
	}
}
