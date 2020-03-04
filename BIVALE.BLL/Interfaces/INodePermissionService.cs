using BIVALE.DTO;
using System.Collections.Generic;

namespace BIVALE.BLL.Interfaces
{
    public interface INodePermissionService
    {
        IEnumerable<NodePermissionDTO> GetNodePermissionsByUser(int userId);
    }
}
