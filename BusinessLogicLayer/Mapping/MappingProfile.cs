using AutoMapper;
using DataAccessLayer.Models;
using DataTransferObjectLayer;
using System.Collections.Generic;

namespace BusinessLogicLayer.Mapping
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<User, UserDTO>();
			CreateMap<UserDTO, User>();
			CreateMap<IList<User>, IList<UserDTO>>();
			CreateMap<IList<UserDTO>, IList<User>>();
			CreateMap<IEnumerable<User>, IEnumerable<UserDTO>>();
			CreateMap<IEnumerable<UserDTO>, IEnumerable<User>>();
			CreateMap<ICollection<User>, ICollection<UserDTO>>();
			CreateMap<ICollection<UserDTO>, ICollection<User>>();
		}
	}
}
