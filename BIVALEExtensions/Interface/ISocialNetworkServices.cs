using DataTransferObjectLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIVALEExtensions.Interface
{
	public interface ISocialNetworkServices
	{
		List<UserDTO> GetUserDTO();
	}
}
