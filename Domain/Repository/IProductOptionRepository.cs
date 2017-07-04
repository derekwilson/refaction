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

		int Create(ProductOption productOption);

		int Update(ProductOption productOption);

		int Delete(Guid id);

		int Delete(ProductOption productOption);
	}
}
