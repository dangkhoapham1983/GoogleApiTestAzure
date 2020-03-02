using AutoMapper;
using BIVALE.BLL.Interfaces;
using BIVALE.DAL.Models;
using BIVALE.DTO;
using BIVALE.Extensions.Services;
using System.Collections.Generic;

namespace BIVALE.BLL.Mapping
{
    public class UserMapper : IMapper<UserDTO, User>
	{
		public User Map(UserDTO obj)
		{
			IMapper mapper = DependencyInjector.Retrieve<ConfigurationMapper>().Configuration();
			return mapper.Map<User>(obj);
		}

		public UserDTO Map(User obj)
		{
			IMapper mapper = DependencyInjector.Retrieve<ConfigurationMapper>().Configuration();
			return mapper.Map<UserDTO>(obj);
		}

		public IEnumerable<User> MapList(IEnumerable<UserDTO> obj)
		{
			IMapper mapper = DependencyInjector.Retrieve<ConfigurationMapper>().Configuration();
			return mapper.Map<IEnumerable<User>>(obj);
		}

		public IEnumerable<UserDTO> MapList(IEnumerable<User> obj)
		{
			IMapper mapper = DependencyInjector.Retrieve<ConfigurationMapper>().Configuration();
			return mapper.Map<IEnumerable<UserDTO>>(obj);
		}

		public IList<User> MapList(IList<UserDTO> obj)
		{
			IMapper mapper = DependencyInjector.Retrieve<ConfigurationMapper>().Configuration();
			return mapper.Map<IList<User>>(obj);
		}

		public IList<UserDTO> MapList(IList<User> obj)
		{
			IMapper mapper = DependencyInjector.Retrieve<ConfigurationMapper>().Configuration();
			var result = mapper.Map<IList<UserDTO>>(obj);
			return result;
		}

		public ICollection<User> MapList(ICollection<UserDTO> obj)
		{
			IMapper mapper = DependencyInjector.Retrieve<ConfigurationMapper>().Configuration();
			return mapper.Map<ICollection<User>>(obj);
		}

		public ICollection<UserDTO> MapList(ICollection<User> obj)
		{
			IMapper mapper = DependencyInjector.Retrieve<ConfigurationMapper>().Configuration();
			return mapper.Map<ICollection<UserDTO>>(obj);
		}
	}
}
