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
	public class ProductOptionRepository : IProductOptionRepository
	{
		private const string SQL_SELECT = "SELECT * FROM productoption ";
		private const string SQL_WHERE_ID_SUFFIX = " WHERE id = @Id ";
		private const string SQL_WHERE_PRODUCT_ID_SUFFIX = " WHERE productid = @ProductId ";

		private const string SQL_SELECT_ALL = SQL_SELECT;
		private const string SQL_SELECT_BY_ID = SQL_SELECT + SQL_WHERE_ID_SUFFIX;
		private const string SQL_SELECT_BY_PRODUCT_ID = SQL_SELECT + SQL_WHERE_PRODUCT_ID_SUFFIX;

		private const string SQL_INSERT = "insert into productoption (id, productid, name, description) values (@Id, @ProductId, @Name, @Description)";
		private const string SQL_UPDATE = "update productoption set name = @Name, description = @Description where id = @Id";
		private const string SQL_DELETE = "delete from productoption where id = @Id";
		// for the moment we need to access this statement from the ProductRepository
		// its not used in this repo but we want to keep all the SQL for this table in one place
		internal const string SQL_DELETE_BY_PRODUCT_ID = "delete from productoption where productid = @Id";

		private IConnectionFactory _connectionFactory;

		public ProductOptionRepository(IConnectionFactory factory)
		{
			_connectionFactory = factory;
		}

		public IList<ProductOption> GetAll()
		{
			using (IDbConnection connection = _connectionFactory.GetOpenConnection())
			{
				var products = connection.Query<ProductOption>(SQL_SELECT_ALL).ToList();
				return products;
			}
		}
		public ProductOption GetById(Guid id)
		{
			using (IDbConnection connection = _connectionFactory.GetOpenConnection())
			{
				var products = connection.Query<ProductOption>(SQL_SELECT_BY_ID, new { Id = id }).ToList();
				return products.FirstOrDefault();
			}
		}

		public IList<ProductOption> GetByProductId(Guid productId)
		{
			using (IDbConnection connection = _connectionFactory.GetOpenConnection())
			{
				var products = connection.Query<ProductOption>(SQL_SELECT_BY_PRODUCT_ID, new { ProductId = productId }).ToList();
				return products;
			}
		}

		public int Create(ProductOption productOption)
		{
			int numberOfRowsAffected = 0;
			using (IDbConnection connection = _connectionFactory.GetOpenConnection())
			{
				// TODO - add foreign key constraint on the ProductId
				var command = new CommandDefinition(SQL_INSERT, productOption);
				numberOfRowsAffected = connection.Execute(command);
			}
			return numberOfRowsAffected;
		}

		public int Update(ProductOption productOption)
		{
			int numberOfRowsAffected = 0;
			using (IDbConnection connection = _connectionFactory.GetOpenConnection())
			{
				numberOfRowsAffected = connection.Execute(SQL_UPDATE, productOption);
			}
			return numberOfRowsAffected;
		}

		public int Delete(ProductOption productOption)
		{
			return Delete(productOption.Id);
		}

		public int Delete(Guid idToDelete)
		{
			int numberOfRowsAffected = 0;
			using (IDbConnection connection = _connectionFactory.GetOpenConnection())
			{
				numberOfRowsAffected = connection.Execute(SQL_DELETE, new { Id = idToDelete });
			}
			return numberOfRowsAffected;
		}
	}
}
