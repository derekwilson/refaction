using System.Data;

namespace Domain.Data
{
	public interface IConnectionFactory
	{
		IDbConnection GetOpenConnection();
	}
}
