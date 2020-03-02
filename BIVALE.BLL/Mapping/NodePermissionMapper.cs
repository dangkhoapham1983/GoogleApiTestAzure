using AutoMapper;
using BIVALE.BLL.Interfaces;
using BIVALE.DAL.Models;
using BIVALE.DTO;
using BIVALE.Extensions.Services;
using System.Collections.Generic;

namespace BIVALE.BLL.Mapping
{
    public class NodePermissionMapper : IMapper<NodePermissionDTO, NodePermission>
    {
		public NodePermission Map(NodePermissionDTO obj)
		{
			IMapper mapper = DependencyInjector.Retrieve<ConfigurationMapper>().Configuration();
			return mapper.Map<NodePermission>(obj);
		}

		public NodePermissionDTO Map(NodePermission obj)
		{
			IMapper mapper = DependencyInjector.Retrieve<ConfigurationMapper>().Configuration();
			return mapper.Map<NodePermissionDTO>(obj);
		}

		public IEnumerable<NodePermission> MapList(IEnumerable<NodePermissionDTO> obj)
		{
			IMapper mapper = DependencyInjector.Retrieve<ConfigurationMapper>().Configuration();
			return mapper.Map<IEnumerable<NodePermission>>(obj);
		}

		public IEnumerable<NodePermissionDTO> MapList(IEnumerable<NodePermission> obj)
		{
			IMapper mapper = DependencyInjector.Retrieve<ConfigurationMapper>().Configuration();
			return mapper.Map<IEnumerable<NodePermissionDTO>>(obj);
		}

		public IList<NodePermission> MapList(IList<NodePermissionDTO> obj)
		{
			IMapper mapper = DependencyInjector.Retrieve<ConfigurationMapper>().Configuration();
			return mapper.Map<IList<NodePermission>>(obj);
		}

		public IList<NodePermissionDTO> MapList(IList<NodePermission> obj)
		{
			IMapper mapper = DependencyInjector.Retrieve<ConfigurationMapper>().Configuration();
			var result = mapper.Map<IList<NodePermissionDTO>>(obj);
			return result;
		}

		public ICollection<NodePermission> MapList(ICollection<NodePermissionDTO> obj)
		{
			IMapper mapper = DependencyInjector.Retrieve<ConfigurationMapper>().Configuration();
			return mapper.Map<ICollection<NodePermission>>(obj);
		}

		public ICollection<NodePermissionDTO> MapList(ICollection<NodePermission> obj)
		{
			IMapper mapper = DependencyInjector.Retrieve<ConfigurationMapper>().Configuration();
			return mapper.Map<ICollection<NodePermissionDTO>>(obj);
		}
	}
}
