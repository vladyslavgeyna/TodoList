namespace TodoList.Extensions
{
	public static class ConfigurationExtensions
	{
		public static string? GetDefaultConnectionString(this IConfiguration configuration)
		{
			try
			{
				return configuration.GetConnectionString("DefaultConnection");
			}
			catch
			{
				throw new Exception("Wrong connection string name");
			}
		}
	}
}
