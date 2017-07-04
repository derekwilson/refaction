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

		private const string SQL_INSERT = "insert into product (id, name, description, price, deliveryprice) values (@Id, @Name, @Description, @Price, @DeliveryPrice)";
		private const string SQL_UPDATE = "update product set name = @Name, description = @Description, price = @Price, deliveryprice = @DeliveryPrice where id = @Id";
		private const string SQL_DELETE = "delete from product where id = @Id";

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

		public int Create(Product product)
		{
			int numberOfRowsAffected = 0;

			using (IDbConnection connection = _connectionFactory.GetOpenConnection())
			{
				var command = new CommandDefinition(SQL_INSERT, product);
				numberOfRowsAffected = connection.Execute(command);
			}

			return numberOfRowsAffected;
		}

		public int Update(Product product)
		{
			int numberOfRowsAffected = 0;

			using (IDbConnection connection = _connectionFactory.GetOpenConnection())
			{
				numberOfRowsAffected = connection.Execute(SQL_UPDATE, product);
			}

			return numberOfRowsAffected;
		}

		public int Delete(Product product)
		{
			return Delete(product.Id);
		}

		public int Delete(Guid idToDelete)
		{
			int numberOfRowsAffected = 0;

			using (IDbConnection connection = _connectionFactory.GetOpenConnection())
			{
				using (IDbTransaction transaction = connection.BeginTransaction())
				{
					try
					{
						// if we add a foreign key constraint we could use cascade delete
						connection.Execute(ProductOptionRepository.SQL_DELETE_BY_PRODUCT_ID, new { Id = idToDelete }, transaction);

						numberOfRowsAffected = connection.Execute(SQL_DELETE, new { Id = idToDelete }, transaction);

						transaction.Commit();
					}
					catch
					{
						transaction.Rollback();
						throw;
					}
				}
			}
			return numberOfRowsAffected;
		}
	}
}
