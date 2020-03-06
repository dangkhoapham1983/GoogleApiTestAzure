using BIVALE.BLL.Enum;
using BIVALE.BLL.Generic;
using BIVALE.BLL.Interfaces;
using BIVALE.BLL.Mapping;
using BIVALE.DAL.Models;
using BIVALE.DTO;
using BIVALE.Extensions.Services;
using System.Collections.Generic;

namespace BIVALE.BLL.Services
{
    public class NodePermissionService : UnitOfWork, INodePermissionService
    {
        private IRepository<NodePermission> userRepository;
        public IRepository<NodePermission> NodePermissionRepository
        {
            get
            {
                if (userRepository == null)
                {
                    userRepository = GetRepository<NodePermission>();
                }
                return userRepository;
            }
        }
        IEnumerable<NodePermissionDTO> INodePermissionService.GetNodePermissionsByUser(int userId, PermissionOwnerType ownerType)
        {
            var objUserMapper = DependencyInjector.Retrieve<NodePermissionMapper>();
            var target = NodePermissionRepository.Get(p => p.PERMISSION_OWNER_ID == userId && p.PERMISSION_OWNER_TYPE == (int)ownerType);
            var result = objUserMapper.MapList(target);
            return result;
        }
    }
}
