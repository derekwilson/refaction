using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
	public interface IProductRepository
	{
		IList<Product> GetAll();

		Product GetById(Guid id);

		IList<Product> GetByName(string name);

		int Create(Product product);

		int Update(Product product);

		int Delete(Guid id);

		int Delete(Product product);
	}
}
