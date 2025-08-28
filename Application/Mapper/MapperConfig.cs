
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application.Mapper
{
	public static class MapperConfig
	{
		public static IServiceCollection AddApplicationMappings(this IServiceCollection services)
		{
			services.AddAutoMapper(Assembly.GetExecutingAssembly());
			return services;
		}
	}


}
