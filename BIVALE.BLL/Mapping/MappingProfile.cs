using AutoMapper;
using BIVALE.DAL.Models;
using BIVALE.DTO;
using System.Collections.Generic;

namespace BIVALE.BLL.Mapping
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<User, UserDTO>();
			CreateMap<UserDTO, User>();
		}
	}
}
