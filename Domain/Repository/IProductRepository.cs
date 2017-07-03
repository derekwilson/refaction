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

		void Create(Product product);

		void Update(Product product);

		void Delete(Guid id);

		void Delete(Product product);
	}
}
