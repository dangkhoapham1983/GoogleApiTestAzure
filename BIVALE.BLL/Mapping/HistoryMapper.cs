using AutoMapper;
using BIVALE.BLL.Interfaces;
using BIVALE.DAL.Models;
using BIVALE.DTO;
using BIVALE.Extensions.Services;
using System.Collections.Generic;
using static BIVALE.BLL.Mapping.ConfigurationMapper;

namespace BIVALE.BLL.Mapping
{
    public class HistoryMapper : IMapper<HistoryDTO, History>
	{
		public History Map(HistoryDTO obj)
		{
			IMapper mapper = DependencyInjector.Retrieve<ConfigurationMapper>().Configuration();
			return mapper.Map<History>(obj);
		}

		public HistoryDTO Map(History obj)
		{
			IMapper mapper = DependencyInjector.Retrieve<ConfigurationMapper>().Configuration();
			return mapper.Map<HistoryDTO>(obj);
		}

		public IEnumerable<History> MapList(IEnumerable<HistoryDTO> obj)
		{
			IMapper mapper = DependencyInjector.Retrieve<ConfigurationMapper>().Configuration();
			return mapper.Map<IEnumerable<History>>(obj);
		}

		public IEnumerable<HistoryDTO> MapList(IEnumerable<History> obj)
		{
			IMapper mapper = DependencyInjector.Retrieve<ConfigurationMapper>().Configuration();
			return mapper.Map<IEnumerable<HistoryDTO>>(obj);
		}

		public IList<History> MapList(IList<HistoryDTO> obj)
		{
			IMapper mapper = DependencyInjector.Retrieve<ConfigurationMapper>().Configuration();
			return mapper.Map<IList<History>>(obj);
		}

		public IList<HistoryDTO> MapList(IList<History> obj)
		{
			IMapper mapper = DependencyInjector.Retrieve<ConfigurationMapper>().Configuration();
			var result = mapper.Map<IList<HistoryDTO>>(obj);
			return result;
		}

		public ICollection<History> MapList(ICollection<HistoryDTO> obj)
		{
			IMapper mapper = DependencyInjector.Retrieve<ConfigurationMapper>().Configuration();
			return mapper.Map<ICollection<History>>(obj);
		}

		public ICollection<HistoryDTO> MapList(ICollection<History> obj)
		{
			IMapper mapper = DependencyInjector.Retrieve<ConfigurationMapper>().Configuration();
			return mapper.Map<ICollection<HistoryDTO>>(obj);
		}
	}
}
