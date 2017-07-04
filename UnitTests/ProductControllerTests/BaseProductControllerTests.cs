using Domain.Model;
using Domain.Repository;
using Moq;
using NUnit.Framework;
using refactor_me.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.ProductControllerTests
{
	class BaseProductControllerTests
	{
		protected ProductsController _controller;
		protected Mock<IProductRepository> _mockProductRepository;
		protected Mock<IProductOptionRepository> _mockProductOptionRepository;

		protected static Guid TestProductId1 = new Guid("11111111-1111-1111-2222-111111111111");
		protected static Guid TestProductId2 = new Guid("22222222-1111-1111-2222-111111111111");
		protected static Guid TestProductInvalidId = new Guid("33333333-1111-1111-2222-111111111111");

		protected IList<Product> _testProductData;

		private IList<Product> GetProductTestsData()
		{
			return new List<Product>(10)
			{
				new Product {
					Id = TestProductId1,
					Name = "TestName1",
					Description = "TestDescription",
					DeliveryPrice = 1,
					Price = 2
				},
				new Product {
					Id = TestProductId2,
					Name = "TestName2",
					Description = "TestDescription",
					DeliveryPrice = 1,
					Price = 2
				}
			};
		}

		[SetUp]
		public void TestSetup()
		{
			_testProductData = GetProductTestsData();
			_mockProductRepository = new Mock<IProductRepository>();
			_mockProductOptionRepository = new Mock<IProductOptionRepository>();

			_mockProductRepository.Setup(productRepo => productRepo.GetAll()).Returns(_testProductData);

			_controller = new ProductsController(_mockProductRepository.Object, _mockProductOptionRepository.Object);
		}

		[TearDown]
		public void TestTearDown()
		{
			_controller = null;
		}
	}
}
