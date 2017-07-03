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

		private const string SQL_INSERT = "insert into productoption (id, productid, name, description) values (@Id, @ProductId, @Name, @Description)";

		public void Create(ProductOption productOption)
		{
			using (IDbConnection connection = _connectionFactory.GetOpenConnection())
			{
				// TODO - add foreign key constraint on the ProductId
				var command = new CommandDefinition(SQL_INSERT, productOption);
				connection.Execute(command);
			}
		}

		private const string SQL_UPDATE = "update productoption set name = @Name, description = @Description where id = @Id";

		public void Update(ProductOption productOption)
		{
			using (IDbConnection connection = _connectionFactory.GetOpenConnection())
			{
				connection.Execute(SQL_UPDATE, productOption);
			}
		}

		private const string SQL_DELETE = "delete from productoption where id = @Id";

		public void Delete(ProductOption productOption)
		{
			Delete(productOption.Id);
		}

		public void Delete(Guid idToDelete)
		{
			using (IDbConnection connection = _connectionFactory.GetOpenConnection())
			{
				connection.Execute(SQL_DELETE, new { Id = idToDelete });
			}
		}
	}
}
