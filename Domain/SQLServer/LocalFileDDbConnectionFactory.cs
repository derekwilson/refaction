using Domain.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.SQLServer
{
	public class LocalFileDbConnectionFactory : IConnectionFactory
	{
		string _dbFilePath;

		public LocalFileDbConnectionFactory(string filepath)
		{
			_dbFilePath = filepath;
		}

		public IDbConnection GetOpenConnection()
		{
			if (!File.Exists(_dbFilePath))
			{
				throw new FileNotFoundException("Database cannot be found", _dbFilePath);
			}
			IDbConnection connection = new System.Data.SqlClient.SqlConnection();
			connection.ConnectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\{_dbFilePath};Integrated Security=True;Connect Timeout=30";
			connection.Open();
			return connection;
		}
	}
}
