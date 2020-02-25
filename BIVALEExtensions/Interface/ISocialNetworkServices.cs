using BIVALE.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIVALE.Extensions.Interface
{
	public interface ISocialNetworkServices
	{
		List<UserDTO> GetUserDTO();
	}
}
