using Domain.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.SQLServer
{
	public class WebConnectionStringConnectionFactory : IConnectionFactory
	{
		private string _connectionString;

		public WebConnectionStringConnectionFactory(string connectionString)
		{
			_connectionString = connectionString;
		}

		IDbConnection IConnectionFactory.GetOpenConnection()
		{
			IDbConnection connection = new System.Data.SqlClient.SqlConnection();
			connection.ConnectionString = _connectionString;
			connection.Open();
			return connection;
		}
	}
}
