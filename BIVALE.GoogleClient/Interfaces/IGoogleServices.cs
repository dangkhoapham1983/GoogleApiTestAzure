using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIVALE.GoogleClient.Interfaces
{
	public interface IGoogleServices
	{
		UserGoogle Validate();

		Task<UserGoogle> CodeValidate(string code);
	}
}
