using Domain.Model;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;

namespace UnitTests.ProductControllerTests
{
	[TestFixture]
	class GetProductTests : BaseProductControllerTests
	{
		[Test]
		public void ShouldReturnDataWhenFound()
		{
			// Arrange
			_mockProductRepository.Setup(productRepo => productRepo.GetById(It.IsAny<Guid>())).Returns(_testProductData.First());

			// Act
			var httpActionResult = _controller.GetProduct(TestProductId1);

			// Assert
			OkNegotiatedContentResult<Product> contentResult = httpActionResult as OkNegotiatedContentResult<Product>;
			Assert.IsNotNull(contentResult);
			Assert.AreEqual(contentResult.Content.Name, "TestName1");
		}

		[Test]
		public void ShouldReturnErrorWhenNotFound()
		{
			// Arrange

			// Act
			var httpActionResult = _controller.GetProduct(TestProductInvalidId);

			// Assert
			StatusCodeResult contentResult = httpActionResult as StatusCodeResult;
			Assert.IsNotNull(contentResult);
			Assert.AreEqual(HttpStatusCode.NotFound, contentResult.StatusCode);
		}
	}
}
