using System;
using System.Collections.Generic;
using AutoMapper;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Services;
using DataAccessLayer.Models;
using DataTransferObjectLayer;

namespace BusinessLogicLayer.Mapping
{
	public class UserMapper : IMapper<UserDTO, User>
	{
		public User Map(UserDTO obj)
		{
			//var config = DependencyInjector.Retrieve<MapperConfiguration>(cfg => cfg.CreateMap<UserDTO, User>());
			//var config = new MapperConfiguration(cfg => cfg.CreateMap<UserDTO, User>());
			var config = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
			var mapper = config.CreateMapper();
			User target = mapper.Map<User>(obj);
			return target;
		}

		public UserDTO Map(User obj)
		{
			//var config = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDTO>());
			var config = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
			var mapper = config.CreateMapper();
			var result = mapper.Map<UserDTO>(obj);
			return result;
		}

		public IEnumerable<User> MapList(IEnumerable<UserDTO> obj)
		{
			var config = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
			var mapper = config.CreateMapper();
			var result = mapper.Map<IEnumerable<User>>(obj);
			return result;
		}

		public IEnumerable<UserDTO> MapList(IEnumerable<User> obj)
		{
			var config = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
			var mapper = config.CreateMapper();
			var result = mapper.Map<IEnumerable<UserDTO>>(obj);
			return result;
		}

		public IList<User> MapList(IList<UserDTO> obj)
		{
			var config = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
			var mapper = config.CreateMapper();
			var result = mapper.Map<IList<User>>(obj);
			return result;
		}

		public IList<UserDTO> MapList(IList<User> obj)
		{
			var config = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
			var mapper = config.CreateMapper();
			var result = mapper.Map<IList<UserDTO>>(obj);
			return result;
		}

		public ICollection<User> MapList(ICollection<UserDTO> obj)
		{
			var config = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
			var mapper = config.CreateMapper();
			var result = mapper.Map<ICollection<User>>(obj);
			return result;
		}

		public ICollection<UserDTO> MapList(ICollection<User> obj)
		{
			var config = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
			var mapper = config.CreateMapper();
			var result = mapper.Map<ICollection<UserDTO>>(obj);
			return result;
		}
	}
}
