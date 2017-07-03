using Dapper;
using Domain.Data;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
	public class ProductRepository : IProductRepository
	{
		private const string SQL_SELECT = "SELECT * FROM product ";
		private const string SQL_WHERE_ID_SUFFIX = " WHERE id = @Id ";
		private const string SQL_WHERE_NAME_SUFFIX = " WHERE lower(name) LIKE @Name ";

		private const string SQL_SELECT_ALL = SQL_SELECT;
		private const string SQL_SELECT_BY_ID = SQL_SELECT + SQL_WHERE_ID_SUFFIX;
		private const string SQL_SELECT_BY_NAME = SQL_SELECT + SQL_WHERE_NAME_SUFFIX;

		private IConnectionFactory _connectionFactory;

		public ProductRepository(IConnectionFactory factory)
		{
			_connectionFactory = factory;
		}

		public IList<Product> GetAll()
		{
			using (IDbConnection connection = _connectionFactory.GetOpenConnection())
			{
				var products = connection.Query<Product>(SQL_SELECT_ALL).ToList();
				return products;
			}
		}
		public Product GetById(Guid id)
		{
			using (IDbConnection connection = _connectionFactory.GetOpenConnection())
			{
				var products = connection.Query<Product>(SQL_SELECT_BY_ID, new { Id = id }).ToList();
				return products.FirstOrDefault();
			}
		}

		public IList<Product> GetByName(string name)
		{
			using (IDbConnection connection = _connectionFactory.GetOpenConnection())
			{
				var products = connection.Query<Product>(SQL_SELECT_BY_NAME, new { Name = name.ToLower() }).ToList();
				return products;
			}
		}

		private const string SQL_INSERT = "insert into product (id, name, description, price, deliveryprice) values (@Id, @Name, @Description, @Price, @DeliveryPrice)";

		public void Create(Product product)
		{
			using (IDbConnection connection = _connectionFactory.GetOpenConnection())
			{
				var command = new CommandDefinition(SQL_INSERT, product);
				connection.Execute(command);
			}
		}

		private const string SQL_UPDATE = "update product set name = @Name, description = @Description, price = @Price, deliveryprice = @DeliveryPrice where id = @Id";

		public void Update(Product product)
		{
			using (IDbConnection connection = _connectionFactory.GetOpenConnection())
			{
				connection.Execute(SQL_UPDATE, product);
			}
		}

		private const string SQL_DELETE = "delete from product where id = @Id";

		public void Delete(Product product)
		{
			Delete(product.Id);
		}

		public void Delete(Guid idToDelete)
		{
			using (IDbConnection connection = _connectionFactory.GetOpenConnection())
			{
				// TODO - delete the product options
				connection.Execute(SQL_DELETE, new { Id = idToDelete });
			}
		}
	}
}
