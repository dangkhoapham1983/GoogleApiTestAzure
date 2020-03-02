using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIVALE.DTO
{
	public class NodePermissionDTO
	{
		public int Id { get; set; }
		public int PERMISSION_OWNER_ID { get; set; }
		public int PERMISSION_OWNER_TYPE { get; set; }
		public string NODE_PATH { get; set; }
		public int PERMISSION_LEVEL { get; set; }

		public UserDTO User { get; set; }
	}
}
