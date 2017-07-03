using System;
using System.Net;
using System.Web.Http;
using refactor_me.Models;
using Domain.Repository;
using System.Collections.Generic;
using Domain.Model;

namespace refactor_me.Controllers
{
    [RoutePrefix("products")]
    public class ProductsController : ApiController
    {
		private IProductRepository _productRepository;
		private IProductOptionRepository _productOptionRepository;

		public ProductsController(IProductRepository productRepository, IProductOptionRepository productOptionRepository)
		{
			_productRepository = productRepository;
			_productOptionRepository = productOptionRepository;
		}

        [Route]
        [HttpGet]
        public IHttpActionResult GetAll()
        {
			IList<Product> items = _productRepository.GetAll();
			ItemCollectionDto<Product> dto = new ItemCollectionDto<Product>(items);
			return Ok(dto);
        }

        [Route]
        [HttpGet]
        public IHttpActionResult SearchByName(string name)
        {
			IList<Product> items = _productRepository.GetByName(name);
			ItemCollectionDto<Product> dto = new ItemCollectionDto<Product>(items);
			return Ok(dto);
		}

		[Route("{id}")]
        [HttpGet]
        public IHttpActionResult GetProduct(Guid id)
        {
			Product product = _productRepository.GetById(id);
            if (product == null)
			{
				return StatusCode(HttpStatusCode.NotFound);
			}

            return Ok(product);
        }

        [Route]
        [HttpPost]
        public IHttpActionResult Create(Product product)
        {
			_productRepository.Create(product);
			return StatusCode(HttpStatusCode.NoContent);
        }

        [Route("{id}")]
        [HttpPut]
        public IHttpActionResult Update(Guid id, Product product)
        {
			Product original = _productRepository.GetById(id);
			if (original == null)
			{
				return StatusCode(HttpStatusCode.NotFound);
			}
			original.Name = product.Name;
			original.Description = product.Description;
			original.Price = product.Price;
			original.DeliveryPrice = product.DeliveryPrice;
			_productRepository.Update(original);
			return StatusCode(HttpStatusCode.NoContent);
		}

		[Route("{id}")]
        [HttpDelete]
        public IHttpActionResult Delete(Guid id)
        {
			// TODO - consider if its an error if the ID does not exist
			_productRepository.Delete(id);
			return StatusCode(HttpStatusCode.NoContent);
		}

		[Route("{productId}/options")]
        [HttpGet]
        public IHttpActionResult GetOptions(Guid productId)
        {
			IList<ProductOption> items = _productOptionRepository.GetByProductId(productId);
			ItemCollectionDto<ProductOption> dto = new ItemCollectionDto<ProductOption>(items);
			return Ok(dto);
        }

        [Route("{productId}/options/{id}")]
        [HttpGet]
        public IHttpActionResult GetOption(Guid productId, Guid id)
        {
			ProductOption option = _productOptionRepository.GetById(id);
			if (option == null)
			{
				return StatusCode(HttpStatusCode.NotFound);
			}

			return Ok(option);
        }

        [Route("{productId}/options")]
        [HttpPost]
        public IHttpActionResult CreateOption(Guid productId, ProductOption option)
        {
            option.ProductId = productId;
			_productOptionRepository.Create(option);
			return StatusCode(HttpStatusCode.NoContent);
        }

        [Route("{productId}/options/{id}")]
        [HttpPut]
        public IHttpActionResult UpdateOption(Guid id, ProductOption option)
        {
			ProductOption original = _productOptionRepository.GetById(id);
			if (original == null)
			{
				return StatusCode(HttpStatusCode.NotFound);
			}
			original.Name = option.Name;
			original.Description = option.Description;
			_productOptionRepository.Update(original);
			return StatusCode(HttpStatusCode.NoContent);
        }

        [Route("{productId}/options/{id}")]
        [HttpDelete]
        public IHttpActionResult DeleteOption(Guid id)
        {
			// TODO - consider if its an error if the ID does not exist
			_productOptionRepository.Delete(id);
			return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
