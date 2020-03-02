using AutoMapper;
using BIVALE.DAL.Models;
using BIVALE.DTO;

namespace BIVALE.BLL.Mapping
{
    public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<User, UserDTO>();
			CreateMap<UserDTO, User>();
            CreateMap<History, HistoryDTO>();
            CreateMap<HistoryDTO, History>();
            CreateMap<NodePermission, NodePermissionDTO>();
            CreateMap<NodePermissionDTO, NodePermission>();
        }
	}
}
