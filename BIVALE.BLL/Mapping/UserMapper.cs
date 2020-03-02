using AutoMapper;
using BIVALE.BLL.Interfaces;
using BIVALE.DAL.Models;
using BIVALE.DTO;
using System.Collections.Generic;

namespace BIVALE.BLL.Mapping
{
    public class UserMapper : IMapper<UserDTO, User>
	{
		public User Map(UserDTO obj)
		{
			var config = new MapperConfiguration(cfg => {
				cfg.AllowNullCollections = true;
				cfg.AddProfile(new MappingProfile());
			});
			var mapper = config.CreateMapper();
			User target = mapper.Map<User>(obj);
			return target;
		}

		public UserDTO Map(User obj)
		{
			var config = new MapperConfiguration(cfg => {
				cfg.AllowNullCollections = true;
				cfg.AddProfile(new MappingProfile());
			});
			var mapper = config.CreateMapper();
			var result = mapper.Map<UserDTO>(obj);
			return result;
		}

		public IEnumerable<User> MapList(IEnumerable<UserDTO> obj)
		{
			var config = new MapperConfiguration(cfg => {
				cfg.AllowNullCollections = true;
				cfg.AddProfile(new MappingProfile());
			});
			var mapper = config.CreateMapper();
			var result = mapper.Map<IEnumerable<User>>(obj);
			return result;
		}

		public IEnumerable<UserDTO> MapList(IEnumerable<User> obj)
		{
			var config = new MapperConfiguration(cfg => {
				cfg.AllowNullCollections = true;
				cfg.AddProfile(new MappingProfile());
			});

			var mapper = config.CreateMapper();
			var result = mapper.Map<IEnumerable<UserDTO>>(obj);
			return result;
		}

		public IList<User> MapList(IList<UserDTO> obj)
		{
			var config = new MapperConfiguration(cfg => {
				cfg.AllowNullCollections = true;
				cfg.AddProfile(new MappingProfile());
			});
			var mapper = config.CreateMapper();
			var result = mapper.Map<IList<User>>(obj);
			return result;
		}

		public IList<UserDTO> MapList(IList<User> obj)
		{
			var config = new MapperConfiguration(cfg => {
				cfg.AllowNullCollections = true;
				cfg.AddProfile(new MappingProfile());
			});
			var mapper = config.CreateMapper();
			var result = mapper.Map<IList<UserDTO>>(obj);
			return result;
		}

		public ICollection<User> MapList(ICollection<UserDTO> obj)
		{
			var config = new MapperConfiguration(cfg => {
				cfg.AllowNullCollections = true;
				cfg.AddProfile(new MappingProfile());
			});
			var mapper = config.CreateMapper();
			var result = mapper.Map<ICollection<User>>(obj);
			return result;
		}

		public ICollection<UserDTO> MapList(ICollection<User> obj)
		{
			var config = new MapperConfiguration(cfg => {
				cfg.AllowNullCollections = true;
				cfg.AddProfile(new MappingProfile());
			});
			var mapper = config.CreateMapper();
			var result = mapper.Map<ICollection<UserDTO>>(obj);
			return result;
		}
	}
}
