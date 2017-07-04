using Domain.Model;
using Domain.Repository;
using System;

namespace IntegrationTests
{
	class ProductAndProductOptionTests
	{
		public static Guid TestProductId = new Guid("33333333-1111-1111-1111-111111111111");
		public static Guid TestProductOptionId = new Guid("44444444-1111-1111-1111-111111111111");

		public static void TestAll()
		{
			Program.WriteMessage("ProductAndProductOptionTests: Started");
			TestDelete();
			Program.WriteMessage("ProductAndProductOptionTests: Complete");
		}

		static private ProductOption GetTestProductOption()
		{
			return new ProductOption()
			{
				Id = TestProductOptionId,
				ProductId = TestProductId,
				Name = "TestOptionName2",
				Description = "TestOptionDescription2"
			};
		}

		static private Product GetTestProduct()
		{
			return new Product()
			{
				Id = TestProductId,
				Name = "TestName2",
				Description = "TestDescription2",
				DeliveryPrice = 1,
				Price = 2
			};
		}

		static void TestDelete()
		{
			IProductRepository productRepository = new ProductRepository(Program.GetDbConnection());
			IProductOptionRepository productOptionRepository = new ProductOptionRepository(Program.GetDbConnection());
			Product productFromDb = productRepository.GetById(TestProductId);
			if (productFromDb != null)
			{
				// lets remove it so the tests will run next time
				productRepository.Delete(TestProductId);
				throw new InvalidOperationException("Test product already present");
			}
			ProductOption productOptionFromDb = productOptionRepository.GetById(TestProductOptionId);
			if (productOptionFromDb != null)
			{
				// lets remove it so the tests will run next time
				productOptionRepository.Delete(TestProductOptionId);
				throw new InvalidOperationException("Test product option already present");
			}

			productRepository.Create(GetTestProduct());
			productFromDb = productRepository.GetById(TestProductId);
			if (productFromDb == null)
			{
				throw new InvalidOperationException("Test product not created");
			}
			productOptionRepository.Create(GetTestProductOption());
			productOptionFromDb = productOptionRepository.GetById(TestProductOptionId);
			if (productOptionFromDb == null)
			{
				throw new InvalidOperationException("Test product option not created");
			}

			// right so now we know we have created a product and an option for it
			// lets delete the product and check that both the product and its option is deleted

			productRepository.Delete(TestProductId);
			productFromDb = productRepository.GetById(TestProductId);
			if (productFromDb != null)
			{
				throw new InvalidOperationException("Test product not deleted");
			}
			productOptionFromDb = productOptionRepository.GetById(TestProductOptionId);
			if (productOptionFromDb != null)
			{
				throw new InvalidOperationException("Test product option not deleted");
			}

			Program.WriteMessage("ProductAndProductOptionTests: TestDelete OK");
		}

	}
}
