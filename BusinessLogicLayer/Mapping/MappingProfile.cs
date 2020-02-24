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
		}
	}
}
