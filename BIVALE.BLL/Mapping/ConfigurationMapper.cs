using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIVALE.BLL.Mapping
{
	public class ConfigurationMapper
	{
		public IMapper Configuration()
		{
			var config = new MapperConfiguration(cfg =>
			{
				cfg.AllowNullCollections = true;
				cfg.AddProfile(new MappingProfile());
			});

			var mapper = config.CreateMapper();
			return mapper;
		}
	}
}
