using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
	public interface IProductOptionRepository
	{
		IList<ProductOption> GetAll();

		IList<ProductOption> GetByProductId(Guid productId);

		ProductOption GetById(Guid id);

		void Create(ProductOption productOption);

		void Update(ProductOption productOption);

		void Delete(Guid id);

		void Delete(ProductOption productOption);
	}
}
